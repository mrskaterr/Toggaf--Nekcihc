using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{
    public Material[] materials;

    void Start()
    {
        GetComponent<Renderer>().material = materials[Random.Range(0, materials.Length)];
    }
}