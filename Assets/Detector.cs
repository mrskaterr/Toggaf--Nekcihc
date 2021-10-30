using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        GameObject g = Instantiate(mine, cam.transform.position + transform.forward * 2, transform.rotation);
    }
}