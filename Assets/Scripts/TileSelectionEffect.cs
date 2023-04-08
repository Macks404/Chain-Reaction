using UnityEngine;

public class TileSelectionEffect : MonoBehaviour
{
    PlayerSelectionManager playerSelectionManager;

    public float effectY = 0.2f;
    public float duration = 0.6f;

    private float elapsedTime;

    public void DoSelectionEffect(GameObject obj) {
        float t = Mathf.PingPong(Time.time, duration) / duration;
        Vector3 start = obj.transform.position;
        Vector3 end = obj.transform.position;
        start.y = transform.position.y + effectY-1;
        end.y = transform.position.y - effectY-1;
        obj.transform.position = Vector3.Lerp(start,end, t);
    }
    public void DoHoverEffect(GameObject obj) {
        obj.transform.position = new Vector3(obj.transform.position.x, -0.8f,obj.transform.position.z);
    }
    public void ResetEffect(GameObject obj) {
        obj.transform.position = new Vector3(obj.transform.position.x, -1, obj.transform.position.z);
    }

}
