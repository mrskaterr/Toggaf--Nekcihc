using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamRed : MonoBehaviour
{
    PhotonView PV;
    public GameObject[] guns;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
        RedSetUp();
    }
    void RedSetUp()
    {
        PlayerController player = GetComponent<PlayerController>();
        if (player.roleIndex == 2)
        {
            foreach(GameObject gun in guns)
            {
                Destroy(gun);
            }
        }
        else
        {
            Destroy(this);
        }
    }
}