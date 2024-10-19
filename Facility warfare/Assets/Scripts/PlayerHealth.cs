using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public bool isLocalPlayer;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] GameObject ragdoll;
    [SerializeField] GameObject part;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [PunRPC]
    public void TakeDamage(float damage)
    {
        hpText.text = "health: " + health.ToString();
        health -= damage;
        PhotonNetwork.Instantiate(part.name, transform.position, Quaternion.identity);
        if(health <= 0)
        {
            int loadout = GetComponent<PlayerSetup>().loaduot;
            if(isLocalPlayer)
            {
                PhotonNetwork.Instantiate(ragdoll.name, transform.position, Quaternion.identity);
                RoomMananger._instance.ChooseLoadOut();
                PhotonNetwork.Destroy(gameObject);
            }
           
        }
    }
}
