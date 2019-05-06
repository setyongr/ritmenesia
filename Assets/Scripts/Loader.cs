using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System;

public class Loader : MonoBehaviour {

    // Use this for initialization
    NoteComponent note;
    LevelConfigComponent levelconfig;
    InstrumentComponent instrument;
    BackgroundConfigComponent bgconf;
	void Awake () {
        //Debug.Log(Application.persistentDataPath);
        note = GetComponent<NoteComponent>();
        levelconfig = GetComponent<LevelConfigComponent>();
        instrument = GetComponent<InstrumentComponent>();
        bgconf = GetComponent<BackgroundConfigComponent>();
    }
	

    public IEnumerator LoadLevelEnum(string level)
    {
        Debug.Log("Loading : " + level);
        TextAsset tconfig = Resources.Load<TextAsset>(level + "\\config");
        TextAsset tinstrument = Resources.Load<TextAsset>(level + "\\instrument");
        TextAsset tnote = Resources.Load<TextAsset>(level + "\\note");
        TextAsset tbg = Resources.Load<TextAsset>(level + "\\background");
        levelconfig.load(level, JsonUtility.FromJson<LevelConfig>(tconfig.text));
        instrument.load(level, JsonUtility.FromJson<Instrument>(tinstrument.text), levelconfig.levelconfig.type);
        note.load(level, JsonUtility.FromJson<Note>(tnote.text));
        bgconf.load(level, JsonUtility.FromJson<BackgroundConfig>(tbg.text));
        yield return null;    
    }
}
