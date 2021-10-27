using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobCatcher : Gun
{
    [SerializeField] Camera cam;

    public override void Use()
    {
        Shoot();
    }

    void Shoot()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(.5f, .5f));
        ray.origin = cam.transform.position;
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            hit.collider.gameObject.GetComponent<ICatchable>()?.Catch(true);
        }
    }
}