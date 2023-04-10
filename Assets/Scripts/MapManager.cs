using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;

    public int startPoints = 30;
    public int currentPoints;
    public Transform colliderYVal;
    public List<GameObject> takenTiles;
    public List<GameObject> objectLayout;
    public bool gameRunning;
    public int trampolineCount;

    public GameObject objectiveTile;
    public GameObject objectiveObj;
    public GameObject forcestartTile;
    public GameObject forcestartObj;

    [SerializeField]
    TextMeshProUGUI winText;

    [SerializeField]
    GameObject[] uiToHide;

    [SerializeField]
    TextMeshProUGUI errorText;

    private void Awake() {
        instance = this;
    }
    private void Start() {
        errorText = FindObjectOfType<ErrorText>().GetComponent<TextMeshProUGUI>();
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
        for(int i = 1; i < objectLayout.ToArray().Length; i++)
        {
            if(objectLayout[i] != null)
            {
                if(objectLayout[i].GetComponent<ItemObject>()){
                    if(!objectLayout[i].GetComponent<Objective>() && !objectLayout[i].GetComponent<StartingForce>())
                        Destroy(objectLayout[i]);
                }
            }
        }
        trampolineCount = 0;
        takenTiles.Clear();
        objectLayout.Clear();

        takenTiles.Add(objectiveTile);
        objectLayout.Add(objectiveObj);
        takenTiles.Add(forcestartTile);
        objectLayout.Add(forcestartObj);

        currentPoints = startPoints;
    }

    private void toggleUI()
    {
        for(int i = 0; i < uiToHide.Length; i++)
        {
            uiToHide[i].gameObject.SetActive(!uiToHide[i].activeSelf);
        }
    }

    public void LevelFinished()
    {
        winText.text = "Level Complete!";
        Invoke("SwitchToMenu",2f);
    }
    private void SwitchToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ResetErrorText()
    {
        Invoke("resetErrorText",1);
    }
    private void resetErrorText()
    {
        errorText.text = "";
    }
}
