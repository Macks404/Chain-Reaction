using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public ObjectProperties[] objectTypes;
    public GameObject iconPrefab;
    public Transform iconParent;

    private void Start() {
        instance = this;
        
        for(int i = 0; i < objectTypes.Length; i++)
        {
            GameObject instance = Instantiate(iconPrefab,iconParent);
            instance.GetComponent<Image>().sprite = objectTypes[i].icon;
        }
    }
}
