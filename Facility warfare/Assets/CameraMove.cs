using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] Transform cameraPos;
    // Update is called once per frame
    void Update()
    {
        transform.position = cameraPos.position;
    }
}
