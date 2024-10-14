using UnityEngine;
using Photon.Pun;
public class RoomMananger : MonoBehaviourPunCallbacks
{
    public static RoomMananger _instance;
    [SerializeField] GameObject player;
    [SerializeField] Transform[] spp;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] GameObject[] loadouts;
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
        ChooseLoadOut();
        
    }
    public void ChooseLoadOut()
    {
       
        Cursor.lockState = CursorLockMode.None;        
        Cursor.visible = true;
        loadingScreen.SetActive(true);
        for (int i = 0; i < loadouts.Length; i++)
        {
            loadouts[i].SetActive(true);
        }
    }
   
    public void RespawnPlayer(int loadout)
    {
        loadingScreen.SetActive(false);
        GameObject _player = PhotonNetwork.Instantiate(player.name, spp[Random.Range(0, spp.Length)].position, Quaternion.identity);
        _player.GetComponent<PlayerSetup>().IsLocalPlayer(loadout);
        _player.GetComponent<PlayerHealth>().isLocalPlayer = true;
    }
}
