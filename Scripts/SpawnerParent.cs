using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerParent : MonoBehaviour
{

    [Range(0, 1)]
    public float progress;
    public float minScale;
    public float maxScale;
    public int numObjectsAtMaxProgress;
    public Color colorA;
    public Color colorB;

    // Update is called once per frame
    void Update()
    {
        foreach (Transform childTr in this.transform) {
            // set the properties
            LineSpawner child = childTr.GetComponent<LineSpawner>();
            child.progress = this.progress;
            child.minScale = this.minScale;
            child.maxScale = this.maxScale;
            child.numObjectsAtMaxProgress = this.numObjectsAtMaxProgress;
            child.colorA = this.colorA;
            child.colorB = this.colorB;
        }
    }
}
