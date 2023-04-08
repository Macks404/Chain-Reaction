using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectIcon : MonoBehaviour
{
    public int iconId;

    public void OnClick()
    {
        FindObjectOfType<ObjectSpawn>().SpawnObject(UIManager.instance.objectTypes[iconId]);
    }
}
