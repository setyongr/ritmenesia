using UnityEngine;
using System;

[Serializable]
public class LevelConfig {
    public string lagu;
    public string type;
    public string namalevel;
    public string namalagu;
    public bool hideText = false;

    public string json()
    {
        return JsonUtility.ToJson(this);
    }
}
