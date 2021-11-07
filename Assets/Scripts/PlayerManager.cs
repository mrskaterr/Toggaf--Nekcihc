using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (PV.IsMine)
        {
            
            if (PV.Owner.CustomProperties.ContainsKey("RoleID"))
            {
                CreateController((int)PV.Owner.CustomProperties["RoleID"]);
            }
        }
    }

    void CreateController(int roleIndex)
    {
        //PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), Vector3.zero, Quaternion.identity);
        switch (roleIndex)
        {
            case 1:
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), Vector3.zero, Quaternion.identity);
                break;
            case 2:
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController Red"), Vector3.zero, Quaternion.identity);
                break;
            case 3:
                PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController Blue"), Vector3.zero, Quaternion.identity);
                break;
        }
    }
}