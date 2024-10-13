using UnityEngine;
using Photon.Pun;

public class PhotonSoundMananger : MonoBehaviour
{
    [SerializeField] AudioSource m_AudioSource;
    [SerializeField] AudioClip AkShoot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    public void PlayShootSFX()
    {
        GetComponent<PhotonView>().RPC("PlayShootSFXPhoton", RpcTarget.All);
    }
    [PunRPC]
    public void PlayShootSFXPhoton()
    {        
        m_AudioSource.PlayOneShot(AkShoot);
    }
}
