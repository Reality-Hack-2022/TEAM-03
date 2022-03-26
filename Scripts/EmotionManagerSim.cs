using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;

public class EmotionManagerSim : MonoBehaviour
{
    // Start is called before the first frame update
    public TextAsset textFile;     // drop your file here in inspector
    string[] emotions;

    int currentIndex = 0;

    string text;
    string[] result;

    void Start(){
        text = textFile.text;  //this is the content as string
        byte[] byteText = textFile.bytes;  //this is the content as byte array
        emotions = new string[]{"angry", "neutral", "surprise", "happy", "disgust", "fear", "sad"};
        

        result = text.Split(new string[] {"......"}, StringSplitOptions.None);
        InvokeRepeating("GetNextEmotionState", 0, 3);
    }

    void GetNextEmotionState() {

        string s = result[currentIndex];
        if (s.Length == 0) {return;}
        if (currentIndex >= result.Length) {return;}

        Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(s);

        foreach (string emotionString in emotions) {
            GameObject[] emotionObjects = GameObject.FindGameObjectsWithTag(emotionString);
            foreach (GameObject emotionObject in emotionObjects) {
                float intensity = (float)myDeserializedClass.emotion.GetType().GetProperty(emotionString).GetValue(myDeserializedClass.emotion);
                emotionObject.GetComponent<ExpressEmotion>().ExpressTheEmotion(intensity);
            }
        }

        currentIndex += 1;
        
    }

}