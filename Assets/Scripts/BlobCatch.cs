using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobCatch : MonoBehaviour
{
    public GameObject cage;
    public GameObject UI;
    public GameObject body;

    public void SetFree(bool _p)
    {
        cage.SetActive(!_p);
        if (UI != null) { UI.SetActive(!_p); }
        //body.SetActive(_p);
    }
}