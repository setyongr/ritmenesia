using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class BackgroundConfig {

    public string main;
    public string[] optional;
    public float[] x;
    public float[] y;
    public float[] w;
    public float[] h;
    public string json()
    {
        return JsonUtility.ToJson(this);
    }
}
