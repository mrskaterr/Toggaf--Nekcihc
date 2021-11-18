using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobCatcherBulletA : Bullet
{
    protected override void Hit(GameObject blob)
    {
        Morfing morfing = blob.GetComponent<Morfing>();
        morfing.Morph();
    }
}