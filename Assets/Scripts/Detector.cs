using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class Detector : Gun
{
    [SerializeField] Camera cam;
    public GameObject mine;

    public override void Use()
    {
        Place();
    }

    void Place()
    {
        //GameObject g = Instantiate(mine, cam.transform.position + transform.forward * 2, transform.rotation);
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Detector"), cam.transform.position + transform.forward * 2, transform.rotation);
    }
}