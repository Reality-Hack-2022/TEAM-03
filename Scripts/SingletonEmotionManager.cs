using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using Newtonsoft.Json;

public class SingletonEmotionManager : MonoBehaviour
{
    // this is the name of the .txt file without the extension (must be in a Resources folder)
    public string fileName = "emotion";
    string[] emotions;
    public GameObject tree;
    public Animator emotionAnimationController;
    public float emissionIntensity = 1f;
    public Color happyA;
    public Color happyB;
    public Color fearA;
    public Color fearB;
    public Color disgustA;
    public Color disgustB;
    public Color surpriseA;
    public Color surpriseB;
    public Color sadA;
    public Color sadB;
    public Color angryA;
    public Color angryB;
    public Color neutralA;
    public Color neutralB;

    int currentIndex = 0;

    void Start(){
        InvokeRepeating("GetNextEmotionState", 1, 2);
    }

    void GetNextEmotionState() {
        string path = @"E:\Northeastern\Spring 2022\Mixed Reality\Tree_Spawner\Tree_Spawner\Assets\Resources\emotion" + this.currentIndex +".txt";
        emotions = new string[] { "angry", "neutral", "surprise", "happy", "disgust", "fear", "sad" };

        string text = File.ReadAllText(path);

        if (text.Length == 0)
        {
            currentIndex++;
            return;
        }

        Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(text);

        Debug.Log("Invocation " + currentIndex);

        float intensity = (float)myDeserializedClass.emotion.GetType().GetProperty(myDeserializedClass.dominant_emotion).GetValue(myDeserializedClass.emotion) / 100f;
        SpawnerParent sp = this.tree.GetComponent<SpawnerParent>();
        Color colorA;
        Color colorB;

        switch (myDeserializedClass.dominant_emotion) {
            case "angry":
                colorA = this.angryA;
                colorB = this.angryB;
                break;
            case "disgust":
                colorA = this.disgustA;
                colorB = this.disgustB;
                break;
            case "fear":
                colorA = this.fearA;
                colorB = this.fearB;
                break;
            case "happy":
                colorA = this.happyA;
                colorB = this.happyB;
                break;
            case "surprise":
                colorA = this.surpriseA;
                colorB = this.surpriseB;
                break;
            case "sad":
                colorA = this.sadA;
                colorB = this.sadB;
                break;
            default:
                colorA = this.neutralA;
                colorB = this.neutralB;
                break;
        }

        this.tree.GetComponent<ExpressByGrowth>().ExpressTheEmotion(intensity, colorA * this.emissionIntensity, colorB * this.emissionIntensity);

        this.emotionAnimationController.SetTrigger(myDeserializedClass.dominant_emotion);
        this.currentIndex++;
    }

}