using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SetEmotionFromFile : MonoBehaviour
{

    // public TextAsset emotionFile;
    public string filename = "Assets/emotion.txt";
    public string emotion;
    public string magnitude;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ReadEmotionFromFile", 1f, 1f);
    }

    string ReadEmotionFromFile() {
        // string[] lines = File.ReadAllLines(this.filename);
        // Debug.Log("Text: " + lines.Length);
        // return lines[0];
        // Debug.Log(emotionFile.text);
        // return emotionFile.text;
        // Create an instance of StreamReader to read from a file.
        // The using statement also closes the StreamReader.

        // using (StreamReader sr = new StreamReader(filename))
        // {
        //     string line;
        //     // Read and display lines from the file until the end of
        //     // the file is reached.
        //     while ((line = sr.ReadLine()) != null)
        //     {
        //         Debug.Log(line.Length);
        //         Debug.Log("Text: " + line);
        //     }
        // }
        // TextAsset txt = (TextAsset)Resources.Load("emotion", typeof(TextAsset));
        // string[] lines = txt.text.Split("\n");
        // this.emotion = lines[0];
        // this.magnitude = lines[1];
        // return emotion;
        return "";
    }

}
