using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    ItemObject itemObject;
    public float moveAmount;
    bool hasMoved;
    [SerializeField]
    GameObject model;
    [SerializeField]
    TextMeshProUGUI errorText;

    Vector3 endPosition;

    float t = 0;

    Vector3 startPosition;

    bool stop;
    bool movedByTrampoline;
    bool hitByTrampoline;

    Vector3 moveDir;

    private void Start() {
        itemObject = GetComponent<ItemObject>();
        startPosition = transform.position;

        errorText = FindObjectOfType<ErrorText>().GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        if(itemObject.activation.isActivated)
        {
            if(!hasMoved && !stop)
            {
                CheckForward();
                OnActivated();
            }
        }

        if(!MapManager.instance.gameRunning)
        {
            RaycastHit hit;

            Vector3 origin = GetComponent<ItemObject>().hitbox.transform.position;
            if(Physics.Raycast(origin,Vector3.forward, out hit, 4))
            {
                if(hit.collider.GetComponentInParent<Trampoline>())
                {
                    errorText.text = "Can't place ball within 2 blocks of trampoline";
                    Invoke("ResetText",1);
                    itemObject.DestroySelf();
                }
            }
            if(Physics.Raycast(origin,Vector3.forward*-1, out hit, 4))
            {
                if(hit.collider.GetComponentInParent<Trampoline>())
                {
                    errorText.text = "Can't place ball within 2 blocks of trampoline";
                    Invoke("ResetText",1);
                    itemObject.DestroySelf();
                }
            }
            if(Physics.Raycast(origin,Vector3.left, out hit, 4))
            {
                if(hit.collider.GetComponentInParent<Trampoline>())
                {
                    errorText.text = "Can't place ball within 2 blocks of trampoline";
                    itemObject.DestroySelf();
                }
            }
            if(Physics.Raycast(origin,Vector3.left*-1, out hit, 4))
            {
                if(hit.collider.GetComponentInParent<Trampoline>())
                {
                    errorText.text = "Can't place ball within 2 blocks of trampoline";
                    MapManager.instance.ResetErrorText();
                    itemObject.DestroySelf();
                }
            }
            
        }
    }
    bool startEndPosSet;

    private void ResetText()
    {
        errorText.text = "";
    }
    
    private void OnActivated(){
        t += Time.deltaTime;
        if(t > 1)
        {
            if(hitByTrampoline && !movedByTrampoline)
            {
                t=0;
                endPosition = startPosition - moveDir * (moveAmount * 2f);
                Debug.Log(endPosition);
                movedByTrampoline = true;
            }
            else
            {
                hasMoved = true;
                itemObject.activationDirections.Add(itemObject.activation.directionFrom);
                itemObject.ActivateSurroundings();
            }
        }
        moveDir = itemObject.activation.directionFrom;
        if(!startEndPosSet)
        {
            endPosition = startPosition + moveDir * moveAmount;
        }
        startEndPosSet = true;

        model.transform.Rotate(new Vector3(itemObject.activation.directionFrom.z,0,itemObject.activation.directionFrom.x));
        if(hitByTrampoline && !movedByTrampoline)
        {
            transform.position = Vector3.Lerp(endPosition,startPosition,t);
        }
        else
        {
            transform.position = Vector3.Lerp(startPosition,endPosition,t);
        }
    }

    public void StopBall()
    {
        itemObject.ActivateSurroundings();
        stop=true;
    }

    private void CheckForward()
    {
        RaycastHit hit;

        Vector3 origin = GetComponent<ItemObject>().hitbox.transform.position;

        if(Physics.Raycast(origin,itemObject.activation.directionFrom, out hit, 2))
        {
            if(hit.collider.GetComponentInParent<TileBarrier>())
            {
                if(GetComponent<Ball>())
                {
                    GetComponent<Ball>().StopBall();
                }
            }
            if(hit.collider.GetComponentInParent<Trampoline>())
            {
                hitByTrampoline = true;
            }
            if(hit.collider.GetComponentInParent<Bomb>())
            {
                if(!hit.collider.GetComponentInParent<Bomb>().isExploded)
                {hit.collider.GetComponentInParent<ItemObject>().isActivated = true;
                hit.collider.GetComponentInParent<ItemObject>().activation.isActivated = true;
                hit.collider.GetComponentInParent<ItemObject>().activation.directionFrom = itemObject.activation.directionFrom;
                GetComponent<Ball>().StopBall();}
            }
            if(hit.collider.GetComponentInParent<Ball>())
            {
                hit.collider.GetComponentInParent<ItemObject>().isActivated = true;
                hit.collider.GetComponentInParent<ItemObject>().activation.isActivated = true;
                hit.collider.GetComponentInParent<ItemObject>().activation.directionFrom = itemObject.activation.directionFrom;
                GetComponent<Ball>().StopBall();
            }
            if(hit.collider.GetComponentInParent<Objective>())
            {
                MapManager.instance.LevelFinished();
                GetComponent<Ball>().StopBall();
            }
            if(hit.collider.GetComponentInParent<ForestTile>())
            {
                itemObject.ActivateSurroundings();
                GetComponent<Ball>().StopBall();
            }
        }
        
    }
}
