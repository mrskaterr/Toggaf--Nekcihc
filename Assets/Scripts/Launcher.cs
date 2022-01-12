using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] TMP_Text errorText;
    [SerializeField] TMP_Text roomNameText;
    [SerializeField] TMP_Text roomPlayersCountText;
    [SerializeField] Transform roomListContent;
    [SerializeField] Transform playerListContent;
    [SerializeField] GameObject roomListItemPrefab;
    [SerializeField] GameObject playerListItemPrefab;
    [SerializeField] GameObject startGameButton;
    [SerializeField] GameObject readyButton;
    [SerializeField] TMP_InputField nickInputField;
    [SerializeField] RoleButtonsParent rolesBtns;

    private void Start()
    {
        Debug.Log("Connecting to Master");
        PhotonNetwork.ConnectUsingSettings();
    }

    public void SetNick()
    {
        PhotonNetwork.NickName = nickInputField.text;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        MenuManager.instance.OpenMenu("title");
        PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString("0000");
    }

    public void CreateRoom()
    {
        if (!string.IsNullOrEmpty(roomNameInputField.text))
        {
            RoomOptions options = new RoomOptions();
            options.MaxPlayers = 3;
            PhotonNetwork.CreateRoom(roomNameInputField.text, options);
            MenuManager.instance.OpenMenu("loading");
        }
    }

    public override void OnJoinedRoom()
    {
        MenuManager.instance.OpenMenu("room");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        Player[] players = PhotonNetwork.PlayerList;

        foreach (Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < players.Length; i++)
        {
            Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }

        //startGameButton.SetActive(PhotonNetwork.IsMasterClient);

        roomPlayersCountText.text = "( " + PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers + " )";
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        //startGameButton.SetActive(PhotonNetwork.IsMasterClient);
        if (newMasterClient.CustomProperties.ContainsKey("Ready"))
        {
            newMasterClient.CustomProperties["Ready"] = false;
            newMasterClient.SetCustomProperties(newMasterClient.CustomProperties);
        }
    }

    private void Update()
    {
        if(PhotonNetwork.IsMasterClient && CheckTeams() && ArePlayersReady())
        {
            startGameButton.SetActive(true);
        }
        else
        {
            startGameButton.SetActive(false);
        }
        readyButton.SetActive(!PhotonNetwork.IsMasterClient);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Room Creation Failed: " + message;
        MenuManager.instance.OpenMenu("error");
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.instance.OpenMenu("loading");
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.instance.OpenMenu("loading");
    }

    public override void OnLeftRoom()
    {
        MenuManager.instance.OpenMenu("title");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform tr in roomListContent)
        {
            Destroy(tr.gameObject);
        }
        for (int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
            {
                continue;
            }
            Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
        roomPlayersCountText.text = "( " + PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers + " )";
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        roomPlayersCountText.text = "( " + PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers + " )";
    }

    public void RoleDisplay()
    {
        GetComponent<PhotonView>().RPC("RPC_RoleDisplay", RpcTarget.All);
    }

    bool CheckTeams()
    {
        List<int> roles = new List<int>();
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if (PhotonNetwork.PlayerList[i].CustomProperties.ContainsKey("RoleID"))
            {
                int tmp = (int)PhotonNetwork.PlayerList[i].CustomProperties["RoleID"];
                if (tmp == 0) { return false; }//brak roli alt
                if (!roles.Contains(tmp))
                {
                    roles.Add(tmp);
                }
                else { return false; }
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    bool ArePlayersReady()
    {
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if (PhotonNetwork.PlayerList[i].IsMasterClient) { continue; }
            if (PhotonNetwork.PlayerList[i].CustomProperties.ContainsKey("Ready"))
            {
                if (!(bool)PhotonNetwork.PlayerList[i].CustomProperties["Ready"]) { return false; }
            }
            else 
            { 
                return false; 
            }
        }
        return true;
    }

    [PunRPC]
    void RPC_RoleDisplay()
    {
        foreach (PlayerListItem pLI in playerListContent.GetComponentsInChildren<PlayerListItem>())
        {
            pLI.SetRoleDisplay();
        }
    }

    public void SetReady(bool _p)
    {
        if (!PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Ready"))
        {
            PhotonNetwork.LocalPlayer.CustomProperties.Add("Ready", _p);
        }
        else
        {
            PhotonNetwork.LocalPlayer.CustomProperties["Ready"] = _p;
        }
        PhotonNetwork.LocalPlayer.SetCustomProperties(PhotonNetwork.LocalPlayer.CustomProperties);
    }
}