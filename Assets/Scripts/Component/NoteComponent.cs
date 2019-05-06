using UnityEngine;
using System.Collections;

public class NoteComponent : MonoBehaviour {

    public Note note;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void load(string level, Note cfg)
    {
        note = cfg;
    }
}
