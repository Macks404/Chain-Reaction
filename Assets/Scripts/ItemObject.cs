using UnityEngine;
using System.Collections.Generic;

public class ItemObject : MonoBehaviour
{
    public ObjectProperties objectProperties;
    public LayerMask itemActivateLayer;
    public GameObject hitbox;
    public List<Vector3> activationDirections = new List<Vector3>();
    public GameObject corrospondingTile;
    public Transform originalPos;

    public bool isActivated;

    public GameObject cube;

    [SerializeField]
    AudioClip placeSfx;

    public struct Activation
    {
        public Vector3 directionFrom;
        public bool isActivated;
    }

    public Activation activation;

    private void Start() {
        hitbox.transform.position = MapManager.instance.colliderYVal.position;
        hitbox.transform.Translate(transform.position.x-objectProperties.xDisplace,0,transform.position.z-objectProperties.zDisplace);
        originalPos = transform;

        activationDirections.AddRange(objectProperties.activationDirections);
    }

    public void DestroySelf()
    {
        MapManager.instance.currentPoints += GetComponent<ItemObject>().objectProperties.cost;
        MapManager.instance.takenTiles.Remove(corrospondingTile);
        MapManager.instance.objectLayout.Remove(this.gameObject);
        FindObjectOfType<AudioSource>().PlayOneShot(placeSfx);
        Destroy(this.gameObject);
    }

    public void ActivateSurroundings()
    {
        if(!GetComponent<StartingForce>())
        {
            for(int i = 0; i < activationDirections.Count; i++)
            {
                RaycastHit hit;

                Vector3 origin = hitbox.transform.position;
                Debug.Log(hitbox.transform.position);

                Debug.DrawRay(origin,activationDirections[i]*Mathf.Infinity,Color.red,100);

                if(GetComponent<Spike>())
                {
                    Debug.Log("Spike active");
                    Instantiate(cube,origin,Quaternion.identity);
                }
                

                if(Physics.Raycast(origin,activationDirections[i], out hit, 2))
                {
                    Debug.Log("3"+this.gameObject);
                    if(hit.collider.GetComponentInParent<ItemObject>())
                    {
                        Debug.Log("4"+this.gameObject);
                        hit.collider.GetComponentInParent<ItemObject>().isActivated = true;
                        hit.collider.GetComponentInParent<ItemObject>().activation.isActivated = true;
                        hit.collider.GetComponentInParent<ItemObject>().activation.directionFrom = activationDirections[i];
                    }
                    if(hit.collider.GetComponentInParent<Spike>())
                    {
                        if(GetComponent<Bomb>())
                        {
                            hit.collider.GetComponentInParent<Spike>().SpikeActivated();
                            Destroy(hit.collider.GetComponentInParent<Spike>().gameObject);
                        }
                    }
                }
            }
        }
        else
        {
            for(int i = 0; i < objectProperties.activationDirections.Length; i++)
            {
                RaycastHit hit;

                Vector3 origin = new Vector3(-8,hitbox.transform.position.y,6);
                Debug.Log(hitbox.transform.position);

                Debug.DrawRay(origin,objectProperties.activationDirections[i]*Mathf.Infinity,Color.red,100);
                Debug.Log("Origin: "+origin+" direction: "+objectProperties.activationDirections[i]);

                if(Physics.Raycast(origin,objectProperties.activationDirections[i], out hit, 2))
                {
                    Debug.Log("3"+this.gameObject);
                    if(hit.collider.GetComponentInParent<ItemObject>())
                    {
                        Debug.Log("4"+this.gameObject);
                        hit.collider.GetComponentInParent<ItemObject>().isActivated = true;
                        hit.collider.GetComponentInParent<ItemObject>().activation.isActivated = true;
                        hit.collider.GetComponentInParent<ItemObject>().activation.directionFrom = objectProperties.activationDirections[i];
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
}
