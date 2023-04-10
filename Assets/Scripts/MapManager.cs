using UnityEngine;
using System.Collections.Generic;
public class MapManager : MonoBehaviour
{
    public static MapManager instance;

    public int startPoints = 30;
    public int currentPoints;
    public Transform colliderYVal;
    public List<GameObject> takenTiles;
    public List<GameObject> objectLayout;
    public bool gameRunning;

    [SerializeField]
    GameObject[] uiToHide;

    private void Awake() {
        instance = this;
    }
    private void Start() {
        currentPoints = startPoints;
    }

    private void Update() {
        if(Input.GetKeyDown("space"))
        {
            EndGame();
            toggleUI();
        }
        
    }

    public void StartGame()
    {
        gameRunning = true;
        toggleUI();
    }

    public void EndGame()
    {
        gameRunning = false;
        for(int i = 0; i < objectLayout.Count; i++)
        {
            Destroy(objectLayout[i]);
        }

        objectLayout.Clear();
        takenTiles.Clear();

        currentPoints = startPoints;
    }

    private void toggleUI()
    {
        for(int i = 0; i < uiToHide.Length; i++)
        {
            uiToHide[i].gameObject.SetActive(!uiToHide[i].activeSelf);
        }
    }
}
