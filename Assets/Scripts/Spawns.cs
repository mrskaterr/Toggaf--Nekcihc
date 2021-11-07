using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawns : MonoBehaviour
{
    public static Spawns instance;
    private void Awake()
    {
        instance = this;
    }

    public Transform[] points;
}