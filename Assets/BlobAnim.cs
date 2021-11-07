using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobAnim : CharacterAnimatorHandler
{
    public Animator animator;
    public override void JumpAnim()
    {
        animator.SetTrigger("jump");
    }
}