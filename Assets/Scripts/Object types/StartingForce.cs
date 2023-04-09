using UnityEngine;

public class StartingForce : MonoBehaviour
{
    ItemObject itemObject;

    public void StartGame()
    {
        itemObject = GetComponent<ItemObject>();
        
        itemObject.activationDirections.Add(transform.forward);
        itemObject.ActivateSurroundings();

        DestroyImmediate(this.gameObject);
    }
}
