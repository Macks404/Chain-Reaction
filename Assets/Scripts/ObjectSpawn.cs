using UnityEngine;

public class ObjectSpawn : MonoBehaviour
{
    public void SpawnObject(ObjectProperties objProps)
    {
        if(GetComponent<PlayerSelectionManager>().selectedGameobj != null)
        {
            if(GetComponent<PlayerSelectionManager>().selectedGameobj.CompareTag("tile"))
            {
                GameObject instance = Instantiate(objProps.obj,GetComponent<PlayerSelectionManager>().selectedGameobj.transform.position,Quaternion.identity);
                instance.transform.Translate(Vector3.up * instance.GetComponent<BoxCollider>().size.y*1.5f);
                MapManager.instance.points -= objProps.cost;
                instance.GetComponent<ItemObject>().objectProperties = objProps;
            }
        }
    }
}
