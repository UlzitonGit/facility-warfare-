using UnityEngine;
using Photon.Pun;
public class RoomMananger : MonoBehaviourPunCallbacks
{
    public static RoomMananger _instance;
    [SerializeField] GameObject player;
    [SerializeField] Transform[] spp;
    [SerializeField] GameObject loadingScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        Debug.Log("Connecting....");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connect to master...");
        PhotonNetwork.JoinLobby();
    }
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        PhotonNetwork.JoinOrCreateRoom("test", null, null);
        Debug.Log("Connected");
       
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();  
        RespawnPlayer();
        loadingScreen.SetActive(false);
    }
    public void RespawnPlayer()
    {
        GameObject _player = PhotonNetwork.Instantiate(player.name, spp[Random.Range(0, spp.Length)].position, Quaternion.identity);
        _player.GetComponent<PlayerSetup>().IsLocalPlayer();
        _player.GetComponent<PlayerHealth>().isLocalPlayer = true;
    }
}
