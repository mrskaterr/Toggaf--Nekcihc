using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SlowBomb : Gun
{
    public int ammo = 10;
    [SerializeField] Camera cam;
    public override void Use()
    {
        Throw();
    }

    void Throw()
    {
        if (ammo > 0)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "SphereBall"), cam.transform.position + transform.forward, transform.rotation);
            ammo--;
        }
    }
}