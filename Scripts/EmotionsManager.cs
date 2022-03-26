using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using Newtonsoft.Json;

public class EmotionsManager : MonoBehaviour
{
    // this is the name of the .txt file without the extension (must be in a Resources folder)
    public string fileName = "emotion";
    string[] emotions;

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

        foreach (string emotionString in emotions)
        {
            GameObject[] emotionObjects = GameObject.FindGameObjectsWithTag(emotionString);
            foreach (GameObject emotionObject in emotionObjects)
            {
                float intensity = (float)myDeserializedClass.emotion.GetType().GetProperty(emotionString).GetValue(myDeserializedClass.emotion) / 100f;
                Debug.Log(emotionString);
                Debug.Log(intensity);
                Debug.Log(path);
                emotionObject.GetComponent<ExpressEmotion>().ExpressTheEmotion(intensity);
            }
        }
        this.currentIndex++;
    }

}


public class Emotion
{
    public float angry { get; set; }
    public float disgust { get; set; }
    public float fear { get; set; }
    public float happy { get; set; }
    public float sad { get; set; }
    public float surprise { get; set; }
    public float neutral { get; set; }
}

public class Region
{
    public int X { get; set; }
    public int y { get; set; }
    public int w { get; set; }
    public int h { get; set; }
}

public class Root
{
    public Emotion emotion { get; set; }
    public string dominant_emotion { get; set; }
    public Region region { get; set; }
}
