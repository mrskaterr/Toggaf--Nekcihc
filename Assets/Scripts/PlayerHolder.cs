using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerHolder : MonoBehaviour
{
    public static PlayerHolder instance;

    private void Awake()
    {
        if (instance != null) { Destroy(gameObject); }
        instance = this;
    }

    public List<PlayerDesc> players;
}

[System.Serializable]
public class PlayerDesc
{
    public Photon.Realtime.Player player;
    public GameObject playerObject;
    public int roleIndex;
}