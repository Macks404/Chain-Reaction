using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public ObjectProperties objectProperties;

    public void DestroySelf()
    {
        MapManager.instance.points += GetComponent<ItemObject>().objectProperties.cost;
        Destroy(this.gameObject);
    }
    
}
