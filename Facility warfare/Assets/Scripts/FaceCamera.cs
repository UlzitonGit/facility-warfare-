using UnityEngine;
using Photon.Pun;

public class FaceCamera : MonoBehaviour
{
    private Camera enemyCamera;
    public void UpdateCamera()
    {
        if (PhotonNetwork.IsConnected)
        {
            GameObject enemyCameraObject = GameObject.FindGameObjectWithTag("MainCamera");

            if (enemyCameraObject != null)
            {
                enemyCamera = enemyCameraObject.GetComponent<Camera>();
            }
        }
        
        if (enemyCamera == null)
        {
            enemyCamera = Camera.main;
        }
    }

    private void Update()
    {
        if (enemyCamera != null)
        {
            transform.LookAt(transform.position + enemyCamera.transform.rotation * Vector3.forward, enemyCamera.transform.rotation * Vector3.up);
        }
    }
}

