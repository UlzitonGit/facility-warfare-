using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    public FirstPersonController movement;
    public GameObject camera;
    [SerializeField] GameObject graphic;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public void IsLocalPlayer()
    {
        movement.enabled = true;
        camera.SetActive(true);
        graphic.SetActive(false);
    }
}
