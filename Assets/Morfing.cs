using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morfing : MonoBehaviour
{
    [SerializeField]    GameObject Player;
    [SerializeField]    GameObject[] BOX;
    [SerializeField]    float MaxDistance=2.5f;
    [SerializeField]    float BackJump=3;
   
    MeshFilter PlayerMesh;
    Mesh OriginalMesh;
    Material OriginalMaterial;
    RaycastHit hit;

    private string CubeString="Cube";

    void Start()
    {
        PlayerMesh=Player.GetComponent<MeshFilter>();
        OriginalMesh=PlayerMesh.sharedMesh;
        OriginalMaterial=Player.GetComponent<Renderer>().material;    
    }

    void Update()
    {
        
        int layerMask = 1 << 8;

        layerMask = ~layerMask;

        // Does the ray intersect any objects excluding the player layer
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, MaxDistance, layerMask))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log(hit.transform.name);
                for(int i=0;i<BOX.Length;i++)
                {
                    if(hit.transform.gameObject == BOX[i])
                    {
                        PlayerMesh.sharedMesh = Resources.Load<MeshFilter>(CubeString).sharedMesh;

                        if(hit.transform.GetComponent<CapsuleCollider>())
                        {
                            Player.GetComponent<CapsuleCollider>().height = hit.transform.GetComponent<CapsuleCollider>().height;
                        }
                            
                        else if(hit.transform.GetComponent<BoxCollider>())
                        {
                            Player.GetComponent<CapsuleCollider>().height = hit.transform.GetComponent<BoxCollider>().size.y;
                        }
                            
                        Player.GetComponent<Renderer>().material = hit.transform.GetComponent<Renderer>().material;
                    }
                }
            }
            else
            {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            //Debug.Log("No Hit");
            }
        }
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            Player.GetComponent<CapsuleCollider>().height=2;
            Player.GetComponent<Rigidbody>().velocity=new Vector3(0,BackJump,0);
            PlayerMesh.sharedMesh=OriginalMesh;
            Player.GetComponent<Renderer>().material=OriginalMaterial;
        }


    }
}
