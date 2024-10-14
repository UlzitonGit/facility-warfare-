using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    [SerializeField] FirstPersonController controller;
    Camera cam;
    private void Start()
    {
        cam = controller.playerCamera;
    }
    void Update()
    {
        transform.LookAt(cam.transform);
    }
}
