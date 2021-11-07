using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class ReadRoleTest : MonoBehaviour
{
    [SerializeField] PlayerController player;

    private void Start()
    {
        if (!player.GetComponent<PhotonView>().IsMine) { Destroy(gameObject); }
        Text text = GetComponent<Text>();
        int role = player.roleIndex;
        switch (role)
        {
            default:
                text.text = "none";
                break;
            case 1:
                text.text = "astro";
                break;
            case 2:
                text.text = "red";
                break;
            case 3:
                text.text = "blue";
                break;
        }
    }
}