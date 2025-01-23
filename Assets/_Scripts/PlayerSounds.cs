using UnityEngine;

public class PlayerSounds : MonoBehaviour {

    private PlayerMovement player;
    private float footstepTimer;
    private float footstepTimerMax = .1f;

    private void Awake() {
        player = GetComponent<PlayerMovement>();
    }

    private void Update() {
        footstepTimer -= Time.deltaTime;
        if(footstepTimer <= 0) {

            footstepTimer = footstepTimerMax;

            if (player.IsWalking()) {
                float volume = 1f;
                SoundManager.Instance.PlayFootstepsSound(player.transform.position, volume);
            }
        }   
    }
}
