using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoilController : MonoBehaviour
{
    [SerializeField] Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void StartRecoil()
    {
        animator.SetTrigger("Recoil");
    }
}