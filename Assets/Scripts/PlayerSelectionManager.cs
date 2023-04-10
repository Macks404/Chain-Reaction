using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerSelectionManager : MonoBehaviour
{
    public GameObject hoveredGameobj;
    public GameObject selectedGameobj;
    
    [SerializeField]
    private Camera cam;

    private TileSelectionEffect tileSelectionEffect;
    private void Start() {
        tileSelectionEffect = GetComponent<TileSelectionEffect>();
    }
    private void Update() {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            //if not over UI
            GetHoveredObject();
            if(Input.GetMouseButtonDown(0))
            {
                if(selectedGameobj != null)
                {
                    if(selectedGameobj.CompareTag("tile"))
                    {
                        tileSelectionEffect.ResetEffect(selectedGameobj);
                    }
                }
                selectedGameobj = hoveredGameobj;
            }

            if(Input.GetMouseButtonDown(1))
            {
                if(hoveredGameobj && !MapManager.instance.gameRunning) {
                    if(hoveredGameobj.GetComponent<ItemObject>())
                    {
                        hoveredGameobj.GetComponent<ItemObject>().DestroySelf();
                    }
                    else if(hoveredGameobj.GetComponentInParent<ItemObject>())
                    {
                        hoveredGameobj.GetComponentInParent<ItemObject>().DestroySelf();
                    }
                }
            }
        }
        if(selectedGameobj != null)
        {
            if(selectedGameobj.CompareTag("tile"))
            {
                tileSelectionEffect.DoSelectionEffect(selectedGameobj);
            }
        }
        if(hoveredGameobj != null)
        {
            if(hoveredGameobj.CompareTag("tile") && hoveredGameobj != selectedGameobj)
            {
                tileSelectionEffect.DoHoverEffect(hoveredGameobj);
            }
        }
    }

    private void GetHoveredObject()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if(hit.collider){
                if(hit.collider.gameObject.CompareTag("tile") || hit.collider.gameObject.GetComponent<ItemObject>() || hit.collider.gameObject.GetComponentInParent<ItemObject>()){
                    if(hoveredGameobj != null){
                        if(hoveredGameobj.CompareTag("tile"))
                        {
                            tileSelectionEffect.ResetEffect(hoveredGameobj);
                        }
                    }
                    hoveredGameobj=hit.collider.gameObject;
                }
                else{
                    SetHoverToNull(hit);
                }
            }
            else {
                SetHoverToNull(hit);
            }
        }
        else {
            SetHoverToNull(hit);
        }
    }

    private void SetHoverToNull(RaycastHit hit)
    {
        if(hoveredGameobj != null)
            if(hoveredGameobj.CompareTag("tile"))
                tileSelectionEffect.ResetEffect(hoveredGameobj);
        hoveredGameobj=null;
    }
}
