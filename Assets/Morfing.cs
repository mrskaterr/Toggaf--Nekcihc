using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morfing : MonoBehaviour
{
    [SerializeField]    GameObject Player;
    [SerializeField]    GameObject GroupObj;
    [SerializeField]    float MaxDistance=2.5f;
    [SerializeField]    float BackJump=3;
    Mesh OriginalMesh;
    Material OriginalMaterial;
    RaycastHit Hit;

    void Start()
    {
        OriginalMesh=Player.GetComponent<MeshFilter>().sharedMesh;
        OriginalMaterial=Player.GetComponent<Renderer>().material;    
    }

    void Update()
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        // Does the ray intersect any objects excluding the player layer
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Hit, MaxDistance, layerMask))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * Hit.distance, Color.yellow);
                Debug.Log(Hit.transform.name);
                for(int i=0;i<GroupObj.transform.childCount;i++)
                {
                    if(Hit.transform.gameObject == GroupObj.transform.GetChild(i).gameObject)
                    {
                        Player.GetComponent<MeshFilter>().sharedMesh = GroupObj.transform.GetChild(i).GetComponent<MeshFilter>().sharedMesh;

                        if(Hit.transform.GetComponent<CapsuleCollider>())
                        {
                            Player.GetComponent<CapsuleCollider>().height = Hit.transform.GetComponent<CapsuleCollider>().height;
                        }
                        else if(Hit.transform.GetComponent<BoxCollider>())
                        {
                            Player.GetComponent<CapsuleCollider>().height = Hit.transform.GetComponent<BoxCollider>().size.y;
                        }
                        else
                        {
                            Debug.Log("None BoxCollider |  None CapsuleCollider");
                        }
                            
                            
                        Player.GetComponent<Renderer>().material = Hit.transform.GetComponent<Renderer>().material;
                    }
                }
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * MaxDistance, Color.white);
                Debug.Log("No Hit");
            }
        }
        else if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            Player.GetComponent<Rigidbody>().velocity=new Vector3(0,BackJump,0);
            Player.GetComponent<CapsuleCollider>().height=2;
            Player.GetComponent<MeshFilter>().sharedMesh=OriginalMesh;
            Player.GetComponent<Renderer>().material=OriginalMaterial;
        }
    }
}
