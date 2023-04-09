using UnityEngine;
using VoxelDestruction;
using TMPro;

public class Bomb : MonoBehaviour
{
    ItemObject itemObject;

    public float explodeRadius;
    public float explodeForce;
    public float overrideMax;
    public float countdown;
    public TextMeshProUGUI countdownText;

    [SerializeField]
    private GameObject particle;

    private GameObject particleInstance;

    public bool isExploded;

    private float current;

    private void Start() {
        itemObject = GetComponent<ItemObject>();
        current = countdown;
    }
    

    private void Update() {
        if(countdownText != null)
        {
            FaceTextMeshToCamera(); 
        }
        
        if(itemObject.activation.isActivated)
        {
            if(current < 0)
            {
                if(!isExploded)
                {
                    Explode();
                    isExploded = true;
                }
            }
            else
            {
                current -= Time.deltaTime;
                countdownText.text = $"{(int)current+1}";
            }
        }
    }

    VoxelFragment[] fragments;

    public void Explode() {
        itemObject.ActivateSurroundings();
        Destroy(countdownText.gameObject);
        Vector3 origin = new Vector3(transform.position.x-itemObject.objectProperties.xDisplace,transform.position.y+itemObject.objectProperties.yDisplace/1.5f,transform.position.z - itemObject.objectProperties.zDisplace);
        Collider[] colliders = Physics.OverlapSphere(origin, explodeRadius);
        particleInstance = Instantiate(particle,origin,Quaternion.identity);

        foreach(Collider collider in colliders)
        {
            if(collider.transform.root.GetComponent<VoxelObject>())
            {
                collider.transform.root.GetComponent<VoxelObject>().AddDestruction(explodeForce,
                origin,-(origin - collider.transform.position).normalized,overrideMax);
            }
            if (collider.transform.root.GetComponent<Rigidbody>())
            {
                collider.transform.root.GetComponent<Rigidbody>().AddForce(-explodeForce * 
                    (origin - collider.transform.position).normalized 
                    * (1 / Vector3.Distance(origin, collider.transform.position)), ForceMode.Impulse);
            }
        }
        Invoke("DestroyAll",1.25f);
    }
    void FaceTextMeshToCamera(){
        Vector3 origRot = countdownText.transform.eulerAngles;
        countdownText.transform.LookAt(Camera.main.gameObject.transform);
        Vector3 desiredRot = countdownText.transform.eulerAngles;
        origRot.y = desiredRot.y;
        countdownText.transform.eulerAngles = new Vector3(origRot.x,origRot.y+180,origRot.z);
    }
    void DestroyAll()
    {
        fragments = FindObjectsOfType<VoxelFragment>();
        Debug.Log(fragments.Length);
        for(int i = 0; i < fragments.Length; i++)
        {
            Destroy(fragments[i].gameObject);
        }
        Destroy(particleInstance);
        Destroy(this.gameObject,0.5f);
    }
}
