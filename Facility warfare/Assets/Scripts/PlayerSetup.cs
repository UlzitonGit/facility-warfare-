using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    public FirstPersonController movement;
    public GameObject camera;
    [SerializeField] GameObject graphic;
    public int loaduot = 0;
    [SerializeField] GameObject[] weapon; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   public void IsLocalPlayer(int weaponIndex)
    {
        loaduot = weaponIndex;
        weapon[weaponIndex].SetActive(true);
        movement.enabled = true;
        camera.SetActive(true);
        graphic.transform.localScale = new Vector3(0, 0, 0);
    }
}
