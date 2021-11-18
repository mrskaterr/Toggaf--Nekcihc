using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanDrone : MonoBehaviour
{
    [SerializeField] Transform[] trash;
    [SerializeField] float speed = 1f;
    [SerializeField] Transform drone;
    [SerializeField] GameObject beam;
    [SerializeField] Transform trashCan;
    bool loaded;

    int target = 0;

    private void FixedUpdate()
    {
        if (!loaded)
        {
            Vector3 tar = new Vector3(trash[target].position.x, transform.position.y, trash[target].position.z);
            transform.LookAt(tar);
            if (Vector3.Distance(transform.position, tar) > .01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, tar, speed / 100);
                beam.SetActive(false);
            }
            else
            {
                beam.SetActive(true);
                if (Vector3.Distance(transform.position, trash[target].position) > .4f)
                {
                    trash[target].position = Vector3.MoveTowards(trash[target].position, tar, speed / 500);
                    return;
                }
                else
                {
                    loaded = true;
                    trash[target].parent = drone;
                }
            } 
        }
        else
        {
            Vector3 tar = new Vector3(trashCan.position.x, transform.position.y, trashCan.position.z);
            transform.LookAt(tar);
            if (Vector3.Distance(transform.position, tar) > .01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, tar, speed / 100);
            }
            else
            {
                beam.SetActive(false);
                trash[target].parent = null;
                trash[target].gameObject.AddComponent<Rigidbody>();
                trash[target].GetComponent<Rigidbody>().angularVelocity = new Vector3(Random.value, Random.value, Random.value);
                Destroy(trash[target].GetComponent<Rigidbody>(), 10f);
                loaded = false;
                target++;
                if (target >= trash.Length)
                {
                    this.enabled = false;
                }
            }
        }
    }

}