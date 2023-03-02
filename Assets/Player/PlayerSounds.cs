using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    Player player;
    float footStepTimer;
    float footStepTimerMax = .1f;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        footStepTimer -= Time.deltaTime;
        if(footStepTimer < 0f)
        {
            footStepTimer = footStepTimerMax;
            float volumne = 1f;
            if(player.IsWalking())
            {
                SoundManager.Instance.PlayFootstepSound(player.transform.position, volumne);
            }
        }
    }

}
