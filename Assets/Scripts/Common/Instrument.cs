using UnityEngine;
using System;

[Serializable]
public class Instrument {

    public string[] nama;
    public string[] idle;
    public string[] pressed;
    public string[] hover;
    public float[] x;
    public float[] y;
    public float[] w;
    public float[] h;
	public string json()
    {
        return JsonUtility.ToJson(this);
    }
}
