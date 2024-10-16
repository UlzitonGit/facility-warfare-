using System.Collections.Generic;
using UnityEngine;

public class ReplayBuffer : MonoBehaviour
{
    private List<PlayerState> buffer = new List<PlayerState>();
    private float replayDuration = 5.0f;

    public void RecordState(Transform playerTransform)
    {
        PlayerState newState = new PlayerState(playerTransform.position, playerTransform.rotation, Time.time);
        buffer.Add(newState);
        
        buffer.RemoveAll(state => Time.time - state.timestamp > replayDuration);
    }

    private void Update()
    {
        RecordState(transform);
    }

    public List<PlayerState> GetReplayData()
    {
        return new List<PlayerState>(buffer);
    }
}
