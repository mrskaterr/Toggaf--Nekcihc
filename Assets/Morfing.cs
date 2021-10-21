using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morfing : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    // Update is called once per frame
    void Update()
    {
        cam.ViewportToWorldPoint (new Vector3(0.5f, 0.5f, 0.0f));
    }
}
