using UnityEngine;

public class ReplayManager : MonoBehaviour
{
    public GameObject playerClonePrefab;
    public PlayerStateRecorder playerStateRecorder;

    public void OnPlayerDeath()
    {
        GameObject playerClone = Instantiate(playerClonePrefab, playerStateRecorder.transform.position, playerStateRecorder.transform.rotation);
        
        KillCamPlayer killCamPlayer = playerClone.GetComponent<KillCamPlayer>();
        
        killCamPlayer.ReplayStates = playerStateRecorder.GetRecordedStates();
        
        killCamPlayer.StartReplay();
    }
}
