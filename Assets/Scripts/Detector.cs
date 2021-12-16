using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using TMPro;

public class Detector : Gun
{
    [SerializeField] Camera cam;
    //public GameObject mine;
    public int ammo = 2;

    [SerializeField] TMP_Text ammoTxt;

    public override void Use()
    {
        Place();
    }

    void Place()
    {
        if (ammo > 0)
        {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Detector"), cam.transform.position + transform.forward * 2, transform.rotation);
            ammo--; 
        }
    }

    private void Update()
    {
        ammoTxt.gameObject.SetActive(itemUI.activeInHierarchy);
        ammoTxt.text = ammo.ToString();
    }
}