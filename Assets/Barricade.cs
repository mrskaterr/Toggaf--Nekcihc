using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    AudioSource BarricadeSound;
    void Awake(){
        BarricadeSound=GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {

        BarricadeSound.Play();
        //other.gameObject.GetComponent<Rigidbody>().AddExplosionForce(1000f,transform.position,5f);

    }
}
