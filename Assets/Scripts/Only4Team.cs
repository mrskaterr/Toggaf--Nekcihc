using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Only4Team : MonoBehaviour
{
    [SerializeField] List<int> roleIndexes;
    [SerializeField] PhotonView PV;
    private void Start()
    {
        if (!roleIndexes.Contains((int)PhotonNetwork.LocalPlayer.CustomProperties["RoleID"]))
        {
            Destroy(gameObject);
        }
    }
}