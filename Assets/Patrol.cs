using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] Transform[] points;
    [SerializeField] float speed = 1f;
    [SerializeField] float waitTime;

    int target = 0;
    

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, points[target].position) > .1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[target].position, speed / 100);
        }
        else
        {
            target++;
            if (target >= points.Length) { target = 0; }
            transform.LookAt(points[target].transform);
        }
    }
}