using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateObject : MonoBehaviour
{

    public float spinRate = 40;
    
    void Update() {
        transform.Rotate(0f, spinRate * Time.deltaTime, 0f);
    }


    
}
