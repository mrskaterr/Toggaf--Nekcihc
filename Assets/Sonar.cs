using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonar : MonoBehaviour
{
    [SerializeField] GameObject arrows;
    WaitForSeconds waitSeconds = new WaitForSeconds(1.5f);

    private void Start()
    {
        InvokeRepeating("Init", 3, 3);
    }

    void Init()
    {
        StartCoroutine(ShowThem());
    }

    IEnumerator ShowThem()
    {
        arrows.SetActive(true);
        yield return waitSeconds;
        arrows.SetActive(false);
    }
}