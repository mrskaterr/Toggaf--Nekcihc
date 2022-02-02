using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlobOthers : MonoBehaviour
{
    [SerializeField] Animator trapImage;

    public void SetTrapView(bool _p)
    {
        trapImage.SetBool("Cool", _p);
    }
}