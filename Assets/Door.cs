using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{   
    [SerializeField] LayerMask acceptableLayers;
    Animator animator;
    AudioSource OpenSound;
    private void Awake()
    {
        OpenSound=GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        animator.SetBool("open", true);
        OpenSound.Play();
    }
    private void OnTriggerExit(Collider other)
    {
        OpenSound.Stop();
        animator.SetBool("open", false);
    }
}