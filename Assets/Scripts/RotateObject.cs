using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] Vector3 v3;

    private void Update()
    {
        transform.Rotate(v3 * Time.fixedDeltaTime);
    }
}