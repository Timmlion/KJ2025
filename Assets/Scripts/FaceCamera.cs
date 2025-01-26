using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main; // Cache the main camera
    }

    void LateUpdate()
    {
        // Make the health bar face the camera
        transform.forward = mainCamera.transform.forward;
    }
}
