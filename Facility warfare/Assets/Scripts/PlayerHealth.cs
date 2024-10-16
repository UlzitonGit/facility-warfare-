using System.Collections;
using System.Collections.Generic;
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
    
    public ReplayBuffer replayBuffer;
    
    [PunRPC]
    public void TakeDamage(float damage)
    {
        hpText.text = "health: " + health.ToString();
        health -= damage;
        PhotonNetwork.Instantiate(part.name, transform.position, Quaternion.identity);
        if(health <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        int loadout = GetComponent<PlayerSetup>().loaduot;
        if(isLocalPlayer)
        {
            StartCoroutine(ReplayCoroutine());
            
            PhotonNetwork.Instantiate(ragdoll.name, transform.position, Quaternion.identity);
            RoomMananger._instance.RespawnPlayer(loadout);
            PhotonNetwork.Destroy(gameObject);
                
            Debug.Log(3);
        }
    }
    
    IEnumerator ReplayCoroutine()
    {
        List<PlayerState> replayData = replayBuffer.GetReplayData();

        Debug.Log(0);
        
        foreach (PlayerState state in replayData)
        {
            Debug.Log(1);
            
            transform.position = state.position;
            transform.rotation = state.rotation;
            
            yield return new WaitForSeconds(0.1f);
        }
        Debug.Log(2);
        
    }
}
