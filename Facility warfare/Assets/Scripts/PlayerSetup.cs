using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    public FirstPersonController movement;
    public GameObject camera;
    [SerializeField] GameObject graphic;
    public int loaduot = 0;
    [SerializeField] GameObject[] weapon;
    
    public string nickname;
    public TextMeshProUGUI nicknameText;
    
    public void IsLocalPlayer(int weaponIndex)
    {
        loaduot = weaponIndex;
        weapon[weaponIndex].SetActive(true);
        movement.enabled = true;
        camera.SetActive(true);
        graphic.transform.localScale = new Vector3(0, 0, 0);

        FaceCamera[] fc = FindObjectsByType<FaceCamera>(FindObjectsSortMode.None);
        for (int i = 0; i < fc.Length; i++)
        {
            fc[i].UpdateCamera();
        }
    }

   [PunRPC] 
   public void SetNickname(string _name)
    {
        nickname = _name;
        nicknameText.text = nickname;
    }
}
