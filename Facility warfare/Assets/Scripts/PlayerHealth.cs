using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public bool isLocalPlayer;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] GameObject ragdoll;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [PunRPC]
    public void TakeDamage(int damage)
    {
        hpText.text = "health: " + health.ToString();
        health -= damage;
        if(health <= 0)
        {
            int loadout = GetComponent<PlayerSetup>().loaduot;
            if(isLocalPlayer)
            {
                PhotonNetwork.Instantiate(ragdoll.name, transform.position, Quaternion.identity);
                RoomMananger._instance.RespawnPlayer(loadout);
                PhotonNetwork.Destroy(gameObject);
            }
           
        }
    }
}
