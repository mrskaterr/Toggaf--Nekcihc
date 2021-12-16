using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Interacting : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float range = 2f;
    [SerializeField] LayerMask layer;

    [SerializeField] GameObject HUD_Image;

    RaycastHit hit;

    [SerializeField] PhotonView PV;

    private void Start()
    {
        if (!PV.IsMine)
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        if (Look4Interact())
        {
            HUD_Image.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                Interact obj = hit.transform.GetComponent<Interact>();
                if (obj != null) { obj.Interacting(gameObject); } 
            }
        }
        else { HUD_Image.SetActive(false); }
    }

    bool Look4Interact()
    {
        return Physics.Raycast(cam.ViewportPointToRay(new Vector3(.5f, .5f, 0)), out hit, range, layer);
    }
}