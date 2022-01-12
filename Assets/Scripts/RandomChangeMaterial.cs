using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomChangeMaterial : MonoBehaviour
{
    [SerializeField] Material[] materials;
    [SerializeField] float timePeriodMin = 1;
    [SerializeField] float timePeriodMax = 2;

    MeshRenderer mesh;

    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        StartCoroutine(ChangeColor(1f));
    }
    IEnumerator ChangeColor(float _p)
    {
        yield return new WaitForSeconds(_p);
        float newTime = Random.Range(timePeriodMin, timePeriodMax);
        int randIndex = Random.Range(0, materials.Length);
        mesh.material = materials[randIndex];
        StartCoroutine(ChangeColor(newTime));
    }
}