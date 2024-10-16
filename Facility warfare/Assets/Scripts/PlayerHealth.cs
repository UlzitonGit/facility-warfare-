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

    ReplayBuffer rb;
    
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
                PlayReplay();
            }
        }
    }

    private void PlayReplay()
    {
        StartCoroutine(ReplayCoroutine());
    }
    
    IEnumerator ReplayCoroutine()
    {
        List<PlayerState> replayData = rb.GetReplayData();

        foreach (PlayerState state in replayData)
        {
            transform.position = state.position;
            transform.rotation = state.rotation;
            
            yield return new WaitForSeconds(5f);
            
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
