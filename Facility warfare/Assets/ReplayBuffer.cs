using System.Collections.Generic;
using UnityEngine;

public class ReplayBuffer
{
    private List<PlayerState> buffer = new List<PlayerState>();
    private float replayDuration = 5.0f;

    public void RecordState(FirstPersonController player)
    {
        PlayerState newState = new PlayerState()
        {
            position = player.transform.position,
            rotation = player.transform.rotation,
            time = Time.time
        };
        buffer.Add(newState);
        
        buffer.RemoveAll(state => Time.time - state.time > replayDuration);
    }

    public List<PlayerState> GetReplayData()
    {
        return new List<PlayerState>(buffer);
    }
}
