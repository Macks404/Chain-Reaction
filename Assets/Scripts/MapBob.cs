using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBob : MonoBehaviour
{
    [SerializeField]
    Transform parent;

    public float effectY = 0.2f;
    public float duration = 0.6f;

    private void Update() {
        float t = Mathf.PingPong(Time.time, duration) / duration;
        Vector3 start = parent.transform.position;
        Vector3 end = parent.transform.position;
        start.y = transform.position.y + effectY;
        end.y = transform.position.y - effectY;
        parent.transform.position = Vector3.Lerp(start,end, t);
    }
}
