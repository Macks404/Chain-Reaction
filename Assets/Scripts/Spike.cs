using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField]
    GameObject hitbox;

    private void Start() {
        hitbox.transform.position = MapManager.instance.colliderYVal.position;
        hitbox.transform.Translate(transform.position.x,0,transform.position.z);
    }
}
