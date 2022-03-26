using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpressByGrowth : ExpressEmotion
{    
    public float duration = 3f;

    int countSincePlay;
    float intensity = 0;
    Color colorA;
    Color colorB;
    float previousIntensity;
    Color previousColorA;
    Color previousColorB;

    public override void ExpressTheEmotion(float intensity)
    {
        this.previousIntensity = this.intensity;
        this.intensity = intensity;
        this.countSincePlay = 0;
    }
    public void ExpressTheEmotion(float intensity, Color newColorA, Color newColorB) {
        this.previousIntensity = this.intensity;
        this.previousColorA = this.colorA;
        this.previousColorB = this.colorB;
        this.intensity = intensity;
        this.colorA = newColorA;
        this.colorB = newColorB;
        this.countSincePlay = 0;
    }

    void FixedUpdate() {
        SpawnerParent comp = this.GetComponent<SpawnerParent>();
        comp.progress = Mathf.Lerp(this.previousIntensity, this.intensity, this.countSincePlay * Time.fixedDeltaTime / this.duration);
        comp.colorA = Color.Lerp(this.previousColorA, this.colorA, this.countSincePlay * Time.fixedDeltaTime / this.duration);
        comp.colorB = Color.Lerp(this.previousColorB, this.colorB, this.countSincePlay * Time.fixedDeltaTime / this.duration);

        this.countSincePlay++;
    }

}
