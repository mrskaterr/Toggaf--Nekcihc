using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToPoint : MonoBehaviour
{
    Transform target;
    [SerializeField] Transform player;
    [SerializeField] int targetIndex;
    private void Start()
    {
        Invoke("WriteThatDown", 1f);
    }

    void WriteThatDown()
    {
        foreach (var item in PlayerHolder.instance.players)
        {
            if (item.roleIndex == targetIndex) { target = item.transform; break; }
        }
        if (target == null)
        {
            Debug.LogWarning($"There is no other player with that index: {targetIndex}");
            Destroy(this);
        }
    }

    private void Update()
    {
        Vector3 dir = target.transform.position - player.position;
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
}