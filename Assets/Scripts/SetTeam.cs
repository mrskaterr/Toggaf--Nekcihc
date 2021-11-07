using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTeam : MonoBehaviour
{
    [SerializeField] Material astro;
    [SerializeField] Material red;
    [SerializeField] Material blue;


    private void Start()
    {
        PlayerController player = GetComponent<PlayerController>();
        switch (player.roleIndex)
        {
            case 1:
                player.GetComponent<Renderer>().material = astro;
                break;
            case 2:
                player.GetComponent<Renderer>().material = red;
                break;
            case 3:
                player.GetComponent<Renderer>().material = blue;
                break;
        }
    }
}