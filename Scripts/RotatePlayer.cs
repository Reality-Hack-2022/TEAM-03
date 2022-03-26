using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{

    public float spinRate = 100;
    void Start()
    {
        Debug.LogWarning("Does console work");
    }

    void Update() {
        if (Input.GetKey("right") || Input.GetKey(KeyCode.D)) {
            transform.Rotate(0, spinRate * Time.deltaTime, 0);
        }

        if (Input.GetKey("left") || Input.GetKey(KeyCode.A)) {
            transform.Rotate(0, -spinRate * Time.deltaTime, 0);
        }
    }
}
