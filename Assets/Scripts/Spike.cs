using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField]
    GameObject hitbox;

    ItemObject itemObject;

    bool isActivated;

    private void Start() {
        itemObject = GetComponent<ItemObject>();

        if(MapManager.instance)
        {
            hitbox.transform.position = MapManager.instance.colliderYVal.position;
            hitbox.transform.Translate(transform.position.x,0,transform.position.z);
        }
        
    }

    private void Update() {
        if(itemObject.activation.isActivated && isActivated == false)
        {
            SpikeActivated();
            isActivated = true;
        }
    }

    public void SpikeActivated()
    {
        itemObject.activationDirections.Add(itemObject.activation.directionFrom);
        Debug.Log("activating surroundings");
        itemObject.ActivateSurroundings();
    }
}
