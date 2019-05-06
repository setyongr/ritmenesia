using UnityEngine;
using System;

[Serializable]
public class Keymap
{
    public string[] key;
    public string[] instrument;
    public string json()
    {
        return JsonUtility.ToJson(this);
    }
}
