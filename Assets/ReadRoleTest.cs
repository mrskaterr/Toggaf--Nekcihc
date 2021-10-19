using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadRoleTest : MonoBehaviour
{
    [SerializeField] Color astro;
    [SerializeField] Color red;
    [SerializeField] Color blue;
    [SerializeField] Color none;

    [SerializeField] Image image;
    [SerializeField] PlayerController player;

    private void Start()
    {
        int role = player.roleIndex;
        switch (role)
        {
            default:
                image.color = none;
                break;
            case 1:
                image.color = astro;
                break;
            case 2:
                image.color = red;
                break;
            case 3:
                image.color = blue;
                break;
        }
    }
}