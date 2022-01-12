using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeRotation : MonoBehaviour
{
    Quaternion initRot;

    private void OnEnable()
    {
        initRot = transform.rotation;
    }

    private void LateUpdate()
    {
        transform.rotation = initRot;
    }
}