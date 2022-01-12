using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPerson : MonoBehaviour // asecix dla czecieiji osoby, bo ten z asset store ciagnie jak qba za prace z assemblera
{
    Camera cam;
    [SerializeField] Transform player;
    [SerializeField] LayerMask walls;

    [SerializeField] float minDistance = .5f;
    [SerializeField] float maxDistance = 5f;
    [SerializeField] float offset = .01f;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Start()
    {
        cam.transform.LookAt(player);
    }

    private void LateUpdate()
    {
        RaycastHit hit;
        Vector3 playerPos = player.position + Vector3.up * offset;
        float dis2 = Vector3.Distance(playerPos, transform.position);
        if (Physics.Raycast(playerPos, transform.position - playerPos, out hit, maxDistance, walls))
        {
            float dis = Vector3.Distance(playerPos, hit.point);
            
            //if (dis > minDistance && dis < dis2)
            //{
            //    transform.position += transform.forward * dis2 * .1f * Time.fixedDeltaTime;
            //}
            //else if (dis < maxDistance && dis > dis2)
            //{
            //    transform.position += transform.forward * -.4f * dis * Time.fixedDeltaTime;
            //}

            if(dis > minDistance && dis < maxDistance)
            {
                //float dir = dis < dis2 ? 1f : -1f;
                transform.position += transform.forward * (dis2 - dis) * 2f * Time.fixedDeltaTime;
            }
        }
        else
        {
            if (dis2 < maxDistance)
            {
                transform.position += transform.forward * -.4f * Time.fixedDeltaTime;
            }
        }
        //Debug.DrawRay(playerPos, transform.position - playerPos, Color.blue, maxDistance);
    }


    //private void Update()
    //{
    //    transform.position += transform.forward * -1f * Time.fixedDeltaTime;
    //}
}