using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpressByRotation : ExpressEmotion
{
    float RotationRate = 0;

    public override void ExpressTheEmotion(float intensity) {
        UpdateRotationRate(intensity);
    }

    void UpdateRotationRate(float intensity) {
        RotationRate = intensity * 1000;
    }

    void Update() {
        transform.Rotate(0, (float)RotationRate * Time.deltaTime, 0);
    }
}
