using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TaskLogic : Interact
{
    [SerializeField] protected Task task;

    public override void Interacting(GameObject whoInteracts = null)
    {
        AllUHave2Do(whoInteracts);
    }
    public virtual void AllUHave2Do(GameObject _player)
    { }
}