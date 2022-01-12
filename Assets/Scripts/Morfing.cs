using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Morfing : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] MeshRenderer eye;
    [SerializeField] MeshRenderer eye2;
    [SerializeField] GameObject[] propList;
    [Space]
    [SerializeField] float MaxDistance = 2.5f;
    [SerializeField] float BackJump = 3;

    RaycastHit Hit;

    [SerializeField] Camera rayStartPoint;
    [SerializeField] GameObject indicator;
    [SerializeField] LayerMask layers;

    [SerializeField] string particlesName;

    [SerializeField] Camera camf, camt;

    public bool morphed;
    PhotonView PV;
    PlayerController playerController;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (PV.IsMine)
        {
            Ray ray = rayStartPoint.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if (Physics.Raycast(ray, out Hit, MaxDistance, layers))
            {
                Item2Morf morf = Hit.collider.GetComponent<Item2Morf>();
                if (morf != null)
                {
                    indicator.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.Mouse0) && !playerController.caught)
                    {
                        Morph(morf.index);
                    }
                }
                else
                {
                    indicator.SetActive(false);
                }
            }
            else
            {
                indicator.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Mouse1) && !playerController.caught)
            {
                Morph();
            } 
        }
        //if(Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Hit, MaxDistance))
        //    {
        //        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * Hit.distance, Color.yellow);
        //        Debug.Log(Hit.transform.name);
        //        //for(int i=0;i<GroupObj.transform.childCount;i++)
        //        //{
        //        //    if(Hit.transform.gameObject == GroupObj.transform.GetChild(i).gameObject)
        //        //    {
        //        //        Player.GetComponent<MeshFilter>().sharedMesh = GroupObj.transform.GetChild(i).GetComponent<MeshFilter>().sharedMesh;

        //        //        if(Hit.transform.GetComponent<CapsuleCollider>())
        //        //        {
        //        //            Player.GetComponent<CapsuleCollider>().height = Hit.transform.GetComponent<CapsuleCollider>().height;
        //        //        }
        //        //        else if(Hit.transform.GetComponent<BoxCollider>())
        //        //        {
        //        //            Player.GetComponent<CapsuleCollider>().height = Hit.transform.GetComponent<BoxCollider>().size.y;
        //        //        }
        //        //        else
        //        //        {
        //        //            Debug.Log("None BoxCollider |  None CapsuleCollider");
        //        //        }


        //        //        Player.GetComponent<Renderer>().material = Hit.transform.GetComponent<Renderer>().material;
        //        //    }
        //        //}
        //    }
        //    else
        //    {
        //        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * MaxDistance, Color.white);
        //        Debug.Log("No Hit");
        //    }
        //}
        //else if(Input.GetKeyDown(KeyCode.Mouse1))
        //{
        //    Player.GetComponent<Rigidbody>().velocity=new Vector3(0,BackJump,0);
        //    Player.GetComponent<CapsuleCollider>().height=2;
        //}
    }

    public void Morph()
    {
        PV.RPC("RPC_Morph", RpcTarget.All);
    }
    public void Morph(int index)
    {
        PV.RPC("RPC_Morph", RpcTarget.All, index);
    }

    void DisAll()
    {
        for (int i = 0; i < propList.Length; i++)
        {
            propList[i].SetActive(false);
        }
    }

    [PunRPC]
    void RPC_Morph()
    {
        morphed = false;
        DisAll();
        Player.SetActive(true);
        eye.enabled = true;
        eye2.enabled = true;
        if (PV.Owner == PhotonNetwork.LocalPlayer) { CamSwap(true); }
    }

    [PunRPC]
    void RPC_Morph(int index)
    {
        morphed = true;
        Player.SetActive(false);
        eye.enabled = false;
        eye2.enabled = false;
        DisAll();
        propList[index].SetActive(true);
        Destroy(PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", particlesName), transform.position + Vector3.up * .5f, transform.rotation), 10f);
        if (PV.Owner == PhotonNetwork.LocalPlayer) { CamSwap(false); }
    }

    void CamSwap(bool _p)
    {
        if (_p)
        {
            camt.gameObject.SetActive(false);
            camf.gameObject.SetActive(true);
        }
        else
        {
            camf.gameObject.SetActive(false);
            camt.gameObject.SetActive(true);
        }
    }
}
//[System.Serializable]//do zmiany ruchu
//public class Morph
//{
//    [SerializeField] string name;
//    public GameObject morfObject;
//    public float movementSpeed = 3;
//}