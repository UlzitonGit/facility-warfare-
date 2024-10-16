using Photon.Pun;
using UnityEngine;

public class KillCamMethods : MonoBehaviour
{
    public Camera mainCamera;
    public Camera killCam;
    public Transform killerTransform;

    private void Start()
    {
        mainCamera = GetComponentInChildren<Camera>();
    }

    private void ActivateKillCam()
    {
        mainCamera.enabled = false;
        killCam.enabled = true;
        
        killCam.transform.position = killerTransform.position;
        killCam.transform.rotation = killerTransform.rotation;
    }
    
    private void OnPlayerDeath(PhotonView killerPhotonView)
    {
        GameObject killer = PhotonView.Find(killerPhotonView.ViewID).gameObject;
        
        killerTransform = killer.transform;
        ActivateKillCam();
    }

    public void DeactivateKillCam()
    {
        killCam.enabled = false;
        mainCamera.enabled = true;
    }
    
}

