using System.Collections.Generic;
using UnityEngine;

public class PlayerStateRecorder : MonoBehaviour
{
    public struct PlayerState
    {
        public Vector3 position;
        public Quaternion rotation;
        public float timestamp;
    }

    private readonly List<PlayerState> stateBuffer = new List<PlayerState>();
    private const float RecordDuration = 5.0f;

    void Update()
    {
        RecordState();
        CleanupOldStates();
    }
    
    private void RecordState()
    {
        PlayerState currentState = new PlayerState
        {
            position = transform.position,
            rotation = transform.rotation,
            timestamp = Time.time
        };

        stateBuffer.Add(currentState);
    }
    
    private void CleanupOldStates()
    {
        stateBuffer.RemoveAll(state => Time.time - state.timestamp > RecordDuration);
    }

    public List<PlayerState> GetRecordedStates()
    {
        return new List<PlayerState>(stateBuffer);
    }
}

