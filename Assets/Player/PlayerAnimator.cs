using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    const string IS_WALKING = "IsWalking";
    Animator animator;

    [SerializeField] Player player;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool(IS_WALKING, player.IsWalking());
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, player.IsWalking());
    }

}
