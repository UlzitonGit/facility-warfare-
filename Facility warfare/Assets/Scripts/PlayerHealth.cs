using System.Collections;
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
    
    [SerializeField] KillCamMethods kcMethods;
    [SerializeField] ReplayManager rp;
    
    [PunRPC]
    public void TakeDamage(float damage)
    {
        hpText.text = "health: " + health.ToString();
        health -= damage;
        PhotonNetwork.Instantiate(part.name, transform.position, Quaternion.identity);
        if(health <= 0)
        {
            if(isLocalPlayer)
            {
                StartCoroutine(EndReplay());
            }
        }
    }
    
    private IEnumerator EndReplay()
    {
        rp.OnPlayerDeath();
        int loadout = GetComponent<PlayerSetup>().loaduot;
        
        yield return new WaitForSeconds(5f);
        
        kcMethods.DeactivateKillCam();
        
        PhotonNetwork.Instantiate(ragdoll.name, transform.position, Quaternion.identity);
        RoomMananger._instance.RespawnPlayer(loadout);
        PhotonNetwork.Destroy(gameObject);
    }
}
