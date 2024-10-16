using UnityEngine;

public class PlayerState
{
    public Vector3 position;
    public Quaternion rotation;
    public float timestamp;

    public PlayerState(Vector3 pos, Quaternion rot, float time)
    {
        position = pos;
        rotation = rot;
        timestamp = time;
    }
}
