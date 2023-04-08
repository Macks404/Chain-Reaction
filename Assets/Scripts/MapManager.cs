using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;

    public int points = 30;

    private void Start() {
        instance = this;
    }
}
