using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DestroyOther : MonoBehaviour
{
    [SerializeField] PhotonView PV;

    private void Start()
    {
        if (!PV.IsMine)
        {
            Destroy(gameObject);
        }
    }
}