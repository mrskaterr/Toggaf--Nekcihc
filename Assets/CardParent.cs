using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardParent : MonoBehaviour
{
    [SerializeField] List<Animator> animators = new List<Animator>();
    
    void ResetAnim(int _index)
    {
        for (int i = 0; i < animators.Count; i++)
        {
            if (i != _index)
            {
                animators[i].Play("Idle", -1, 0f);
            }
        }
    }

    public void SetAstroGear(int _index)
    {
        ResetAnim(_index);
        Debug.Log(_index);
    }
}