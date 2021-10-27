using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morfing : MonoBehaviour
{
    MeshFilter yourMesh;
    private string CubeString="Cube";
    void Start()
    {
        yourMesh=gameObject.GetComponent<MeshFilter>();
    }

    void FixedUpdate()
    {
        int layerMask = 1 << 8;

        layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log(hit.transform.name);
            
            if(hit.transform.name==CubeString){
                yourMesh.sharedMesh = Resources.Load<MeshFilter>(CubeString).sharedMesh;
                gameObject.GetComponent<CapsuleCollider>().height=1;
            }
            
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            //Debug.Log("No Hit");
        }
    }
}
