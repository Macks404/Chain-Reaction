using UnityEngine;
using TMPro;

public class ObjectSpawn : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI errorMessage;

    [SerializeField]
    AudioClip clip;

    [SerializeField]
    AudioSource source;

    public void SpawnObject(ObjectProperties objProps)
    {
        if(GetComponent<PlayerSelectionManager>().selectedGameobj != null)
        {
            if(GetComponent<PlayerSelectionManager>().selectedGameobj.CompareTag("tile"))
            {
                if(!MapManager.instance.takenTiles.Contains(GetComponent<PlayerSelectionManager>().selectedGameobj) && !MapManager.instance.gameRunning)
                {
                    if(MapManager.instance.currentPoints - objProps.cost > 0)
                    {
                        GameObject instance = Instantiate(objProps.obj,GetComponent<PlayerSelectionManager>().selectedGameobj.transform.position,Quaternion.identity);
                        source.PlayOneShot(clip);
                        MapManager.instance.takenTiles.Add(GetComponent<PlayerSelectionManager>().selectedGameobj);
                        MapManager.instance.objectLayout.Add(instance);
                        instance.GetComponent<ItemObject>().corrospondingTile = GetComponent<PlayerSelectionManager>().selectedGameobj;

                        if(instance.GetComponent<BoxCollider>())
                        {
                            instance.transform.Translate(Vector3.up * instance.GetComponent<BoxCollider>().size.y*1.5f);
                        }
                        else
                        {
                            instance.transform.Translate(new Vector3(instance.GetComponent<ItemObject>().objectProperties.xDisplace, 
                            instance.GetComponent<ItemObject>().objectProperties.yDisplace,
                            instance.GetComponent<ItemObject>().objectProperties.zDisplace));
                        }
                        MapManager.instance.currentPoints -= objProps.cost;
                        instance.GetComponent<ItemObject>().objectProperties = objProps;
                        GetComponent<PlayerSelectionManager>().selectedGameobj = null;
                    }
                    else
                    {
                        errorMessage.text = "> Not enough points! <";
                        Invoke("ResetError",1);
                    }
                }
            }
        }
    }
    private void ResetError()
    {
        errorMessage.text = "";
    }
}
