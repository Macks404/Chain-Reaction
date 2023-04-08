using UnityEngine;

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
        GetHoveredObject();
        if(Input.GetMouseButtonDown(0))
        {
            if(selectedGameobj != null)
            {
                tileSelectionEffect.ResetEffect(selectedGameobj);
            }
            selectedGameobj = hoveredGameobj;
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
            if(hit.collider)
                if(hit.collider.gameObject.CompareTag("tile")){
                    if(hoveredGameobj != null){
                        tileSelectionEffect.ResetEffect(hoveredGameobj);
                    }
                    hoveredGameobj=hit.collider.gameObject;
                }
                else{
                    if(hoveredGameobj != null)
                        tileSelectionEffect.ResetEffect(hoveredGameobj);
                    hoveredGameobj=null;
                }
                    
            else {
                if(hoveredGameobj != null)
                    tileSelectionEffect.ResetEffect(hoveredGameobj);
                hoveredGameobj=null;
            }
        }
        else {
            if(hoveredGameobj != null)
                tileSelectionEffect.ResetEffect(hoveredGameobj);
            hoveredGameobj = null;
        }
    }
}
