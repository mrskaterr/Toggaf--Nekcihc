using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameTime : MonoBehaviour
{
    bool startTimer = false;
    double timerIncrementValue;
    double startTime;
    [SerializeField] TMP_Text text;
    [SerializeField] double timer = 20;
    ExitGames.Client.Photon.Hashtable CustomeValue;

    void Start()
    {
        if (!GetComponent<PhotonView>().IsMine) { Destroy(gameObject); }
        if (PhotonNetwork.IsMasterClient)
        {
            CustomeValue = new ExitGames.Client.Photon.Hashtable();
            startTime = PhotonNetwork.Time;
            startTimer = true;
            CustomeValue.Add("StartTime", startTime);
            PhotonNetwork.CurrentRoom.SetCustomProperties(CustomeValue);
        }
        else
        {
            Invoke("NotHostStart", .1f);
        }
    }

    void NotHostStart()
    {
        startTime = double.Parse(PhotonNetwork.CurrentRoom.CustomProperties["StartTime"].ToString());
        startTimer = true;
    }

    void Update()
    {
        if (!startTimer) return;
        timerIncrementValue = PhotonNetwork.Time - startTime;
        TimeSpan time = TimeSpan.FromSeconds(timer - timerIncrementValue);
        text.text = time.ToString("mm':'ss");
        if (timerIncrementValue >= timer)
        {
            GetComponent<PhotonView>().RPC("RPC_EndGame", RpcTarget.All, 2);
            Destroy(gameObject);
        }
    }

    [PunRPC]
    void RPC_EndGame(int index)
    {
        PhotonNetwork.LoadLevel(index);
    }
}