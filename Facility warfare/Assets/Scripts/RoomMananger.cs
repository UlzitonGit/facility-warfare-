using UnityEngine;
using Photon.Pun;
public class RoomMananger : MonoBehaviourPunCallbacks
{
    public static RoomMananger _instance;
    [SerializeField] GameObject player;
    [SerializeField] Transform[] spp;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] GameObject[] loadouts;
    [SerializeField] GameObject joinButton;
    [SerializeField] GameObject load;
    [SerializeField] GameObject namePicker;
    int weaponID = 0;
    private string nickname = "Player";
    private void Awake()
    {
        _instance = this;
        load.SetActive(false);
    }

   
    private void Start()
    {
        Debug.Log("Connecting....");
        Debug.Log(nickname);
        PhotonNetwork.ConnectUsingSettings();
        weaponID = PlayerPrefs.GetInt("weapon");
        joinButton.SetActive(false);
        load.SetActive(true);
        namePicker.SetActive(false);
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
        RespawnPlayer(weaponID);
    }
   
    
   
    public void RespawnPlayer(int loadout)
    {
        loadingScreen.SetActive(false);
        GameObject _player = PhotonNetwork.Instantiate(player.name, spp[Random.Range(0, spp.Length)].position, Quaternion.identity);
        _player.GetComponentInParent<PlayerSetup>().IsLocalPlayer(loadout);
        _player.GetComponentInChildren<PlayerHealth>().isLocalPlayer = true;
        
        _player.GetComponentInParent<PhotonView>().RPC("SetNickname", RpcTarget.AllBuffered, nickname);
    }
}
