using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject particles;
    [SerializeField] GameObject dissolution;
    [Range(.1f, 1f)] [SerializeField] float radius;
    [Range(.1f, 10f)] [SerializeField] float range;
    [SerializeField] LayerMask target;
    [SerializeField] LayerMask ignore;

    private void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed * Time.deltaTime * 100;
    }

    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius, ignore);
        if (colliders.Length > 0)
        {
            Collider[] blobs = Physics.OverlapSphere(transform.position, range, target);
            for (int i = 0; i < blobs.Length; i++)
            {
                Hit(blobs[i].gameObject);
                Debug.Log("B: " + blobs[i].name);
            }
            if (dissolution != null)
            {
                Instantiate(dissolution, transform.position, Quaternion.identity);
            }
            if (particles != null)
            {
                particles.transform.parent = null;
                particles.transform.localScale = Vector3.one;
                particles.GetComponent<ParticleSystem>().Stop();
            }
            Destroy(gameObject);
        }
    }

    protected virtual void Hit(GameObject blob)
    {
        Debug.Log(blob.name + " hit!");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}