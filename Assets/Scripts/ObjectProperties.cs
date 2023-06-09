using UnityEngine;

[CreateAssetMenu(fileName ="New object type", menuName = "Create new object type")]
public class ObjectProperties : ScriptableObject{
    public string objectName = "Ball";
    public Sprite icon;
    public GameObject obj;
    public int cost;
    public float yDisplace;
    public float xDisplace;
    public float zDisplace;
    public Vector3[] activationDirections;
}
