using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilypadController : MonoBehaviour
{
    void Start()
    {
        this.transform.Rotate(0f, Random.Range(0f, 180f), 0f);
    }

}
