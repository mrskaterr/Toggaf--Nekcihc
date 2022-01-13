using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] LayerMask acceptableLayers;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        animator.SetBool("open", true);
    }
    private void OnTriggerExit(Collider other)
    {
        animator.SetBool("open", false);
    }
}