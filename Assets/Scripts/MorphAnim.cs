using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorphAnim : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        animator.Rebind();
        animator.Update(0);
    }
}