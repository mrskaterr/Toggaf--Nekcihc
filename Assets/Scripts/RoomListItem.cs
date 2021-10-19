using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    public RoomInfo info;
    string pre;
    Button button;

    public void SetUp(RoomInfo _info)
    {
        info = _info;
        text.text = _info.Name;
        pre = text.text;
        button = GetComponent<Button>();
    }

    private void Update()
    {
        text.text = pre + " [ " + info.PlayerCount + " / " + info.MaxPlayers + " ]";
        if (info.PlayerCount == info.MaxPlayers)
        {
            button.interactable = false;
        }
        else
        {
            button.interactable = true;
        }
    }

    public void OnClick()
    {
        Launcher.instance.JoinRoom(info);
    }
}