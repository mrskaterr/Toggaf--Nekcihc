using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFree : MonoBehaviour
{
    int HowMany=10;
    
    void Update()
    {
        if(Input.GetButtonDown("F"))
            --HowMany;
        
        if(HowMany<=0)
        {
            
        }


    }
}
