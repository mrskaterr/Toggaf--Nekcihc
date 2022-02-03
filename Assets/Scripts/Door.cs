using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Door : MonoBehaviour
{
    Animator animator;
    PhotonView PV;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        PV = GetComponent<PhotonView>();
    }

    void SetAnimParam(bool _param)
    {
        PV.RPC("RPC_SetAnimParam", RpcTarget.All, _param);
    }

    [PunRPC]
    void RPC_SetAnimParam(bool _p)
    {
        if(animator != null)
        {
            animator.SetBool("open", _p);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        SetAnimParam(true);
    }
    private void OnTriggerExit(Collider other)
    {
        SetAnimParam(false);
    }
}