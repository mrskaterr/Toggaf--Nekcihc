using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamBlue : MonoBehaviour
{
    PhotonView PV;
    public GameObject[] guns;

    private void Start()
    {
        PV = GetComponent<PhotonView>();
        BlueSetUp();
    }
    void BlueSetUp()
    {
        PlayerController player = GetComponent<PlayerController>();
        if (player.roleIndex == 3)
        {
            foreach (GameObject gun in guns)
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