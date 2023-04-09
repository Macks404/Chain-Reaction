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
                MapManager.instance.points -= objProps.cost;
                instance.GetComponent<ItemObject>().objectProperties = objProps;
                GetComponent<PlayerSelectionManager>().selectedGameobj = null;
            }
        }
    }
}
