using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ErrorText : MonoBehaviour
{
    float timer = 0;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 4)
        {
            timer = 0;
            GetComponent<TextMeshProUGUI>().text = "";
        }
    }
}
