using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rotation;
    [Space]
    [SerializeField] float range;
    [SerializeField] LayerMask targetLayers;
    [Space]
    [SerializeField] MeshRenderer swap;
    [SerializeField] Material normal;
    [SerializeField] Material onDetect;

    private void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed * Time.deltaTime;
    }

    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, targetLayers);
        Material[] materials = swap.materials;
        if(colliders.Length > 0)
        {
            materials[1] = onDetect;
            swap.materials = materials;
            ShowDetect(true);
        }
        else
        {
            materials[1] = normal;
            swap.materials = materials;
            ShowDetect(false);
        }
    }

    void ShowDetect(bool param)
    {
        foreach (PlayerController player in PlayerHolder.instance.players)
        {
            if (player.roleIndex == 1)
            {
                player.GetComponent<DetectInfo>().SetDetectInfoState(param);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}