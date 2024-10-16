using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCamPlayer : MonoBehaviour
{
        public List<PlayerStateRecorder.PlayerState> ReplayStates;
        public float replayDuration = 5.0f;
        private float replayStartTime;

        public void StartReplay()
        {
            replayStartTime = Time.time;
            StartCoroutine(PlayReplay());
        }

        private IEnumerator PlayReplay()
        {
            foreach (var state in ReplayStates)
            {
                float waitTime = state.timestamp - (Time.time - replayStartTime);
                if (waitTime > 0) yield return new WaitForSeconds(waitTime);
                
                transform.position = state.position;
                transform.rotation = state.rotation;
                
                if (Time.time - replayStartTime > replayDuration) break;
            }
        }
}
