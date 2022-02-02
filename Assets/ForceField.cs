using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ForceField : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("RoleID"))
        {
            if((int)PhotonNetwork.LocalPlayer.CustomProperties["RoleID"] != 1)
            {
                PlayerController player = other.GetComponent<PlayerController>();
                BlobOthers blob = other.GetComponent<BlobOthers>();
                if(player != null)
                {
                    player.Slowness(true, 3f);
                }
                if(blob != null)
                {
                    blob.SetTrapView(true);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.Slowness(false);
        }
        BlobOthers blob = other.GetComponent<BlobOthers>();
        if (blob != null)
        {
            blob.SetTrapView(false);
        }
    }
}