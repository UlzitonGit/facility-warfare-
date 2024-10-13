using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    public FirstPersonController movement;
    public GameObject camera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public void IsLocalPlayer()
    {
        movement.enabled = true;
        camera.SetActive(true);
    }
}
