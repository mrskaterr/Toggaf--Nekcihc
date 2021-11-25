using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobCatcherBulletB : Bullet
{
    protected override void Hit(GameObject blob)
    {
        Morfing morfing = blob.GetComponent<Morfing>();
        if (!morfing.morphed)
        {
            blob.GetComponent<ICatchable>()?.Catch(true);
        }
    }
}