using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LineSpawner : MonoBehaviour
{

    public GameObject spawnedObjectPrefab;
   [Range(0, 1)]
    public float progress;
    public float minScale;
    public float maxScale;
    public Transform startPos;
    public Transform endPos;
    public int numObjectsAtMaxProgress;
    public Color colorA;
    public Color colorB;


    GameObject[] objects;
    Vector3[] randomOffsets;

    void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(this.startPos.position, this.endPos.position);
        Gizmos.DrawSphere(this.startPos.position, .1f);
        Gizmos.DrawSphere(this.endPos.position, .1f);
    }


    // Start is called before the first frame update
    void Start()
    {

        Array.Resize(ref this.objects, this.numObjectsAtMaxProgress);
        Array.Resize(ref this.randomOffsets, this.numObjectsAtMaxProgress);

        for (int i = 0; i < numObjectsAtMaxProgress; i++)
        {
            // Debug.Log(i);
            // Debug.Log(positionPercent);
            // Debug.Log(newPos);
            this.randomOffsets[i] = UnityEngine.Random.insideUnitSphere * ((float)i / this.numObjectsAtMaxProgress);
            GameObject newObject = Instantiate(this.spawnedObjectPrefab, this.CalcPos(i), UnityEngine.Random.rotation);
            float scale = this.CalcScale(i);
            newObject.transform.localScale = new Vector3(scale, scale, scale);
            newObject.SetActive(true);
            this.objects[i] = newObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < this.numObjectsAtMaxProgress; i++)
        {
            this.objects[i].transform.position = this.CalcPos(i);
            float scale = this.CalcScale(i);
            this.objects[i].transform.localScale = new Vector3(scale, scale, scale);

            this.objects[i].transform.GetChild(0).GetComponent<Renderer>().material.color = this.CalcColor(i);
            this.objects[i].transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", this.CalcColor(i));
        }
    }

    float CalcScale(int index)
    {
        float elementDelta = ((float)index) / this.numObjectsAtMaxProgress;
        float value = (this.progress - elementDelta); // / (1.0f / this.numObjectsAtMaxProgress);
        return Mathf.Min(Mathf.Max(value, 0), 1) * (this.maxScale - this.minScale) + this.minScale;
    }

    Vector3 CalcPos(int index)
    {
        Vector3 pos = this.startPos.position + ((float)index / (this.numObjectsAtMaxProgress - 1)) * (this.endPos.position - this.startPos.position);
        return pos + this.randomOffsets[index];
    }

    Color CalcColor(int index)
    {
        float elementDelta = ((float)index) / this.numObjectsAtMaxProgress;
        float value = (this.progress - elementDelta); // / (1.0f / this.numObjectsAtMaxProgress);
        return Color.Lerp(this.colorA, this.colorB, value);
    }

}
