using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoost : MonoBehaviour
{
    [SerializeField] PlayerController controller;
    [SerializeField] PlayerGroundCheck gc;
    [SerializeField] float multiplier = 2;

    private void Update()
    {
        if(gc.lyr == 17)
        {
            controller.JumpBoostSet(true, multiplier);
        }
        else { controller.JumpBoostSet(false); }
    }
}