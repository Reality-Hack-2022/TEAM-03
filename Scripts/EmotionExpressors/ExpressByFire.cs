using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpressByFire : ExpressEmotion
{    
    public float duration = 3f;
    public GameObject flames;
    public GameObject smoke;
    public float maxFlameScale = 7f;
    public float maxSmokeScale = 3f;

    [Range(0, 1)] public float fireThreshold = .4f;
    [Range(0, 1)] public float smokeThreshold = .1f;

    int countSincePlay;
    float intensity = 0;
    float previousIntensity;

    public override void ExpressTheEmotion(float intensity) {
        this.previousIntensity = this.intensity;
        this.intensity = intensity;
        this.countSincePlay = 0;
    }

    void FixedUpdate() {

        if (this.intensity >= this.fireThreshold) {
            this.flames.SetActive(true);
            ParticleSystem flameParticles = this.flames.GetComponent<ParticleSystem>();
            var fireStruct = flameParticles.main;
            fireStruct.startSize = Mathf.Lerp(this.previousIntensity * this.maxFlameScale, this.maxFlameScale * this.intensity, this.countSincePlay * Time.fixedDeltaTime / this.duration);
        }
        else {
            this.flames.SetActive(false);
        }

        if (this.intensity >= this.smokeThreshold) {
            this.smoke.SetActive(true);
            ParticleSystem smokeParticles = this.flames.GetComponent<ParticleSystem>();
            var smokeStruct = smokeParticles.main;
            smokeStruct.startSize = new ParticleSystem.MinMaxCurve(0, Mathf.Lerp(this.previousIntensity * this.maxSmokeScale, this.maxSmokeScale * this.intensity, this.countSincePlay * Time.fixedDeltaTime / this.duration));

            this.countSincePlay++;
        }
        else {
            this.smoke.SetActive(false);
        }
    }

}
