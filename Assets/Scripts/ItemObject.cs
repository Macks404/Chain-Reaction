using UnityEngine;
using System.Collections.Generic;

public class ItemObject : MonoBehaviour
{
    public ObjectProperties objectProperties;
    public LayerMask itemActivateLayer;
    public GameObject hitbox;
    public List<Vector3> activationDirections;

    public bool isActivated;

    public struct Activation
    {
        public Vector3 directionFrom;
        public bool isActivated;
    }

    public Activation activation;

    private void Start() {
        hitbox.transform.position = MapManager.instance.colliderYVal.position;
        hitbox.transform.Translate(transform.position.x-objectProperties.xDisplace,0,transform.position.z-objectProperties.zDisplace);
        for(int i = 0; i < objectProperties.activationDirections.Length; i++)
        {
            activationDirections.Add(objectProperties.activationDirections[i]);
        }
    }

    private void Update() {
        ActivateSurroundings();
    }

    public void DestroySelf()
    {
        MapManager.instance.points += GetComponent<ItemObject>().objectProperties.cost;
        Destroy(this.gameObject);
    }

    public void ActivateSurroundings()
    {
        for(int i = 0; i < activationDirections.ToArray().Length; i++)
        {
            RaycastHit hit;

            Vector3 origin = hitbox.transform.position;

            Debug.Log(this.gameObject+ " "+activationDirections[i]);

            Debug.DrawRay(origin,activationDirections[i],Color.red);

            if(Physics.Raycast(origin,activationDirections[i], out hit, 2))
            {
                if(hit.collider.GetComponentInParent<ItemObject>())
                {
                    hit.collider.GetComponentInParent<ItemObject>().isActivated = true;
                    hit.collider.GetComponentInParent<ItemObject>().activation.isActivated = true;
                    hit.collider.GetComponentInParent<ItemObject>().activation.directionFrom = activationDirections[i];
                }
                if(hit.collider.GetComponentInParent<Spike>())
                {
                    if(GetComponent<Bomb>())
                    {
                        Destroy(hit.collider.GetComponentInParent<Spike>().gameObject);
                    }
                }
            }
        }
    }
}
