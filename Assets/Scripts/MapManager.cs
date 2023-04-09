using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;

    public int points = 30;
    public Transform colliderYVal;

    private void Start() {
        instance = this;
    }
}
