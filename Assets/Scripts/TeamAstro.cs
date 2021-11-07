using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TeamAstro : MonoBehaviour
{
    PhotonView PV;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
        AstroSetUp();
    }

    void AstroSetUp()
    {
        PlayerController player = GetComponent<PlayerController>();
        if(player.roleIndex == 1)
        {

        }
        else
        {
            Destroy(this);
        }
    }
}