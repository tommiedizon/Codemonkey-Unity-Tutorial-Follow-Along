using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

    [SerializeField] PlayerMovement player;

    private Animator animator;
    private const string IS_WALKING = "IsWalking";

    private void Awake() {
        animator = GetComponent<Animator>();
        animator.SetBool(IS_WALKING, player.IsWalking());
    }

    private void Update() {
        animator.SetBool(IS_WALKING, player.IsWalking());
    }
}
