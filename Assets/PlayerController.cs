using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPunCallbacks, ICatchable
{
    [SerializeField] GameObject cameraHolder;
    [HideInInspector] public Camera cam;

    [SerializeField] float mouseSensitivity, sprintSpeed, walkSpeed, jumpForce, smoothTime;

    [SerializeField] Item[] items;

    int itemIndex, previousItemIndex = -1;

    float verticalLookRotation;
    [SerializeField] bool grounded;
    Vector3 smoothMoveVelocity;
    Vector3 moveAmount;

    Rigidbody rb;

    PhotonView PV;

    public int roleIndex = 0;

    bool caught;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (PV.IsMine)
        {
            EquipItem(0);
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
    }

    private void Update()
    {
        if (!PV.IsMine) { return; }// don't mess with others's clients

        Look();

        Move();

        Jump();

        for (int i = 0; i < items.Length; i++)
        {
            if(Input.GetKeyDown((i + 1).ToString()))
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
            rb.AddForce(transform.up * jumpForce);
        }
    }

    void EquipItem(int _index)
    {
        if(_index == previousItemIndex) { return; }

        itemIndex = _index;

        items[itemIndex].itemGameObject.SetActive(true);

        if (previousItemIndex != -1)
        {
            items[previousItemIndex].itemGameObject.SetActive(false);
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

    public void Catch(bool state)
    {
        PV.RPC("RPC_Catch", RpcTarget.All, state);//RPC params: method name, target players, method params
    }

    [PunRPC]
    void RPC_Catch(bool state)
    {
        if (!PV.IsMine)
        {
            return;
        }
        caught = state;
        //for now ...
        if (caught)
        {
            Spectate();
        }
    }

    void Spectate()
    {
        PlayerController[] controllers = FindObjectsOfType<PlayerController>();// TODO: zrzucanie PlayerController do playerHoldera w starcie
        PlayerController controller = null;
        bool con = true;
        while (con)
        {
            int index = Random.Range(0, PhotonNetwork.PlayerList.Length);
            controller = controllers[index];
            if (controller != this && controller.roleIndex != 1)
            {
                con = false;
            }
        }
        controller.cam.gameObject.SetActive(true);
        cam.gameObject.SetActive(false);
    }
}