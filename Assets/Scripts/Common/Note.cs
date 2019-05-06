using UnityEngine;
using System;

[Serializable]
public class Note {

    public string[] instname;
    public float[] time;

    public string json()
    {
        return JsonUtility.ToJson(this);
    }
}
