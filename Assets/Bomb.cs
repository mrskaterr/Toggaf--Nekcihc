using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] GameObject boom;
    [SerializeField] float speed = 10f;
    [SerializeField] float duration = 5f;

    private void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed * Time.deltaTime * 100;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(Instantiate(boom, transform.position, Quaternion.identity), duration);
        Destroy(gameObject);
    }
}