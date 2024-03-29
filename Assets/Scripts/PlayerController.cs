﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPunCallbacks, ICatchable
{
    public GameObject cameraHolder;
    [HideInInspector] public Camera cam;

    [SerializeField] float mouseSensitivity, sprintSpeed, walkSpeed, jumpForce, smoothTime;
    float  _sprintSpeed, _walkSpeed, _jumpForce;

    [SerializeField] Item[] items;

    int itemIndex, previousItemIndex = -1;

    float verticalLookRotation;
    [SerializeField] bool grounded;
    Vector3 smoothMoveVelocity;
    Vector3 moveAmount;

    Rigidbody rb;

    [HideInInspector]public PhotonView PV;

    BlobCatch blobCatch;

    public int roleIndex = 0;

    [HideInInspector] public bool caught, eliminated;
    [SerializeField] bool human;

    private void Awake()
    {
        _jumpForce = jumpForce;
        _sprintSpeed = sprintSpeed;
        _walkSpeed = walkSpeed;
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
        blobCatch = GetComponent<BlobCatch>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (PV.IsMine)
        {
            if (human)
            {
                EquipItem(0); 
            }
        }
        else
        {
            //Destroy(GetComponentInChildren<Camera>().gameObject);// wrong camera
            cam = GetComponentInChildren<Camera>();
            cam.gameObject.SetActive(false);
            Destroy(rb);// janky movement
        }
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("RoleID"))
        {
            //roleIndex = (int)PhotonNetwork.LocalPlayer.CustomProperties["RoleID"];
            roleIndex = (int)PV.Owner.CustomProperties["RoleID"];
        }
        transform.position = Spawns.instance.points[roleIndex - 1].position;
        transform.eulerAngles = Spawns.instance.points[roleIndex - 1].eulerAngles;
        if (roleIndex == 1) { human = true; }
        PlayerHolder.instance.players.Add(this);
    }

    private void Update()
    {
        if (!PV.IsMine) { return; }// don't mess with others's clients

        Look();

        Move();

        Jump();

        if (human)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (Input.GetKeyDown((i + 1).ToString()))
                {
                    EquipItem(i);
                    break;
                }
            }

            if (Input.GetAxisRaw("Mouse ScrollWheel") > 0f)
            {
                if (itemIndex >= items.Length - 1)
                {
                    EquipItem(0);
                }
                else
                {
                    EquipItem(itemIndex + 1);
                }
            }
            else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0f)
            {
                if (itemIndex <= 0)
                {
                    EquipItem(items.Length - 1);
                }
                else
                {
                    EquipItem(itemIndex - 1);
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                items[itemIndex].Use();
            } 
        }
    }

    private void FixedUpdate()
    {
        if (!PV.IsMine) { return; }

        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }

    void Look()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);

        verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;
    }

    void Move()
    {
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref smoothMoveVelocity, smoothTime);//TODO: cala sekcja do zmiany ( inputy )
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            GetComponent<CharacterAnimatorHandler>().JumpAnim();
            rb.AddForce(transform.up * jumpForce);
        }
    }

    void EquipItem(int _index)
    {
        if(_index == previousItemIndex) { return; }

        itemIndex = _index;

        items[itemIndex].itemGameObject.SetActive(true);
        items[itemIndex].itemUI.SetActive(true);

        if (previousItemIndex != -1)
        {
            items[previousItemIndex].itemGameObject.SetActive(false);
            items[previousItemIndex].itemUI.SetActive(false);
        }

        previousItemIndex = itemIndex;

        if (PV.IsMine)
        {
            Hashtable hash = new Hashtable();
            hash.Add("itemIndex", itemIndex);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (!PV.IsMine && targetPlayer == PV.Owner)
        {
            EquipItem((int)changedProps["itemIndex"]);
        }
    }

    public void SetGroundedState(bool _grounded)
    {
        grounded = _grounded;
    }

    public void Catch(bool state,bool state2)
    {
        PV.RPC("RPC_Catch", RpcTarget.All, state, state2);//RPC params: method name, target players, method params
    }

    [PunRPC]
    void RPC_Catch(bool state, bool state2)
    {
        if (blobCatch != null) { blobCatch.SetFree(!state); }
        if (!PV.IsMine)
        {
            return;
        }
        caught = state;
        eliminated = state2;
        SetMovement(!state);
        if (eliminated)
        {
            Spectate();
        }
    }

    void Spectate()
    {
        PlayerController[] controllers = PlayerHolder.instance.players.ToArray();
        PlayerController controller = null;
        PlayerController[] cont = new PlayerController[PhotonNetwork.CurrentRoom.MaxPlayers];
        int ind = 0;
        for (int i = 0; i < controllers.Length; i++)
        {
            if (CheckBlob(controllers[i]))
            {
                cont[ind] = controllers[i];
                ind++;
            }
        }
        if (ind > 0)
        {
            int index = Random.Range(0, ind);
            while (!CheckBlob(cont[index]))
            {
                index = Random.Range(0, ind);
            }
            controller = cont[index];
            Debug.Log(controller.name);
            if (controller.cam != null)
            {
                controller.cam.gameObject.SetActive(true);
            }
            if (cam != null)
            {
                cam.gameObject.SetActive(false);
            }
            PhotonNetwork.Destroy(gameObject);
        }
        else
        {
            PV.RPC("RPC_EndGame", RpcTarget.All, 2);
        }
    }

    bool CheckBlob(PlayerController player)
    {
        return player != null && player != this && player.roleIndex != 1 && !player.eliminated;
    }

    [PunRPC]
    void RPC_EndGame(int index)
    {
        PhotonNetwork.LoadLevel(index);
    }
    public void SetMovement(bool _p)
    {
        if (_p)
        {
            sprintSpeed = _sprintSpeed;
            walkSpeed = _walkSpeed;
            jumpForce = _jumpForce;
        }
        else
        {
            sprintSpeed = 0;
            walkSpeed = 0;
            jumpForce = 0;
        }

    }
}