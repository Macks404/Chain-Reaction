using UnityEngine;
using Cinemachine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera[] cameras;

    private CinemachineVirtualCamera temp;
    
    int currentCameraIndex=0;
    void Start()
    {
        cameras=FindObjectsOfType<CinemachineVirtualCamera>();

        // Ensure that the cameras array is not empty
        if (cameras.Length == 0)
        {
            Debug.LogWarning("No cameras have been assigned to the CameraCycle script.");
            return;
        }

        // Sort the cameras array by their m_Priority value
        System.Array.Sort(cameras, (a, b) => a.m_Priority.CompareTo(b.m_Priority));

        // Set the first camera in the array as active
        cameras[currentCameraIndex].Priority = 10;
    }

    void Update()
    {
        // Check for user input to cycle through cameras
        if (Input.GetKeyDown(KeyCode.A))
        {
            // Decrease the priority of the current camera
            cameras[currentCameraIndex].Priority = 0;

            // Move to the previous camera in the array
            currentCameraIndex = (currentCameraIndex + cameras.Length - 1) % cameras.Length;

            // Increase the priority of the new current camera
            cameras[currentCameraIndex].Priority = 10;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            // Decrease the priority of the current camera
            cameras[currentCameraIndex].Priority = 0;

            // Move to the next camera in the array
            currentCameraIndex = (currentCameraIndex + 1) % cameras.Length;

            // Increase the priority of the new current camera
            cameras[currentCameraIndex].Priority = 10;
        }
    }
}
