using UnityEngine;

public class Ball : MonoBehaviour
{
    ItemObject itemObject;
    public float moveAmount;
    bool hasMoved;
    [SerializeField]
    GameObject model;

    float t = 0;

    Vector3 startPosition;


    private void Start() {
        itemObject = GetComponent<ItemObject>();
        startPosition = transform.position;
    }

    private void Update() {
        if(itemObject.activation.isActivated)
        {
            if(!hasMoved)
            {
                OnActivated();
            }
        }
    }

    private void OnActivated(){
        t += Time.deltaTime;
        if(t > 1)
        {
            hasMoved = true;
            itemObject.activationDirections.Add(itemObject.activation.directionFrom);
            Debug.Log("Ball finished moving: "+itemObject.activation.directionFrom);
            itemObject.ActivateSurroundings();
        }
        Vector3 moveDir = itemObject.activation.directionFrom;
        Vector3 endPosition = startPosition + moveDir * moveAmount;

        model.transform.Rotate(new Vector3(itemObject.activation.directionFrom.z,0,itemObject.activation.directionFrom.x));
        transform.position = Vector3.Lerp(startPosition,endPosition,t);
    }
}
