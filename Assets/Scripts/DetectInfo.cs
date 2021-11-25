using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DetectInfo : MonoBehaviour
{
    [SerializeField] GameObject detectInfo;
    PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    public void SetDetectInfoState(bool _p)
    {
        if (PV.IsMine) { detectInfo.SetActive(_p); }
    }
}