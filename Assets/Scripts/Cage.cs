using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : Interact
{
    [SerializeField] PlayerController blob;
    public override void Interacting(GameObject whoInteracts = null)
    {
        blob.Catch(true, true);
    }
}