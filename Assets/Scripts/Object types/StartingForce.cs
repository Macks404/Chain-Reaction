using UnityEngine;

public class StartingForce : MonoBehaviour
{
    ItemObject itemObject;
    GameObject hitbox;

    public void StartGame()
    {
        itemObject = GetComponent<ItemObject>();

        Invoke("DOIT",0.5f);
    }

    private void DOIT()
    {
        Debug.Log("Doing it!");
        itemObject.ActivateSurroundings();
    }
}
