using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    public PlayerController playerController;
    public int lyr;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != playerController.gameObject) 
        { 
            playerController.SetGroundedState(true); 
            lyr = other.gameObject.layer;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject != playerController.gameObject) 
        { 
            playerController.SetGroundedState(true);
            lyr = other.gameObject.layer;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != playerController.gameObject) 
        { 
            playerController.SetGroundedState(false);
            lyr = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != playerController.gameObject) 
        { 
            playerController.SetGroundedState(true);
            lyr = collision.gameObject.layer;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject != playerController.gameObject) 
        { 
            playerController.SetGroundedState(true);
            lyr = collision.gameObject.layer;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject != playerController.gameObject) 
        { 
            playerController.SetGroundedState(false);
            lyr = 0;
        }
    }
}