using UnityEngine;
using Cinemachine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera[] virtualCameras;
    private int mainVCamIndex;
    private int secondaryVCamIndex;

    private void Start() {
        virtualCameras = FindObjectsOfType<CinemachineVirtualCamera>();
        int highestIndex = 0;
        for(int i = 0; i < virtualCameras.Length; i++)
        {
            if(virtualCameras[i].m_Priority > virtualCameras[highestIndex].m_Priority)
            {
                highestIndex = i;
            }
        }
        mainVCamIndex = highestIndex;
        if(highestIndex == 1)
        {
            secondaryVCamIndex = 0;
        }
        else
        {
            secondaryVCamIndex = 1;
        }
    }

    private void Update() {
        if(Input.GetKeyDown("d"))
        {
            virtualCameras[mainVCamIndex].Priority = 1;
            virtualCameras[secondaryVCamIndex].Priority = 2;
        }
        if(Input.GetKeyDown("a"))
        {
            virtualCameras[mainVCamIndex].Priority = 2;
            virtualCameras[secondaryVCamIndex].Priority = 1;
        }
    }
}
