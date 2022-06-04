using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckCollider : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    void OnControllerColliderHit(ControllerColliderHit colObj)
    {
        tag = colObj.gameObject.tag;
        if (tag == "Untagged")
        {
            return;
        }
        if(tag == "LoseTrigger")
        {
            print("You lost!");
        }
        print(colObj.gameObject.tag);
        
    }
}
