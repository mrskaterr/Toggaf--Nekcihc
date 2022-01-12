using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    public DifficultyType difficulty;
    [SerializeField] GameObject @object;
    public bool active = true;

    public void Complete()
    {

    }

    public void Init() { @object.layer = 14; }
}
public enum DifficultyType { easy, medium, hard }