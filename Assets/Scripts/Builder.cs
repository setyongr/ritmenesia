using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class Builder : MonoBehaviour {

    public List<string> instname;
    public List<float> time;

    public Keymap key;
    public bool record = false;
    public string levelName = "master";
    NoteComponent note;
    LevelConfigComponent levelconfig;
    InstrumentComponent instrument;
    
    public Toggle recordToggle;
    public Toggle playToggle;
    
    void Start()
    {
        //Debug.Log(Application.persistentDataPath);
        note = GetComponent<NoteComponent>();
        levelconfig = GetComponent<LevelConfigComponent>();
        instrument = GetComponent<InstrumentComponent>();
        instname = new List<string>();
        time = new List<float>();
        // Load Key Map

        recordToggle.onValueChanged.AddListener((value) =>
        {
            setRecord(value);
        });

        playToggle.onValueChanged.AddListener((value) =>
        {
            setPlay(value);
        });
	}
	
	// Update is called once per frame
	void Update () {
        
        for(int i = 0; i<key.key.Length; i++)
        {
            if (Input.GetKeyDown(key.key[i]))
            {
                GameObject g = GameObject.Find(key.instrument[i]);

                if (g)
                {
                    if (levelconfig.levelconfig.type == "Falling Note")
                    {
                        g.GetComponent<FallingNoteInstrument>().ProcessNote("pressed");
                    }else if(levelconfig.levelconfig.type == "KlikKlik")
                    {
                        g.GetComponent<KlikKlikInstrument>().ProcessNote("pressed");
                    }
                }
                
                if (!record) return;
                instname.Add(key.instrument[i]);
                time.Add(GetComponent<AudioSource>().time);
            }
            else
            {
                GameObject g = GameObject.Find(key.instrument[i]);
                
                if (g)
                {
                    if(levelconfig.levelconfig.type == "Falling Note")
                    {
                        g.GetComponent<FallingNoteInstrument>().ProcessNote("idle");
                    }
                    else if (levelconfig.levelconfig.type == "KlikKlik")
                    {
                        g.GetComponent<KlikKlikInstrument>().ProcessNote("idle");
                    }
                }
                
            }
        }
    }

   
    void setRecord(bool val)
    {
        record = val;
    }
    public void Reset()
    {
        Note n = new Note();
        n.instname = new string[0];
        n.time = new float[0];
        GetComponent<NoteComponent>().load(levelName, n);
        instname.Clear();
        time.Clear();
    }

    public void Save()
    {
        Note note = new Note();
        note.instname = instname.ToArray();
        note.time = time.ToArray();
        GetComponent<NoteComponent>().load(levelName, note);
        SaveLevel(levelName);
    }

    public void InitInstrument()
    {
        instrument.init(levelName, levelconfig.levelconfig.type);
    }
    public void SaveLevel(string level)
    {
        FileInfo fconfig = new FileInfo(Application.persistentDataPath + "\\" + level + "\\config.json");
        FileInfo finstrument = new FileInfo(Application.persistentDataPath + "\\" + level + "\\instrument.json");
        FileInfo fnote = new FileInfo(Application.persistentDataPath + "\\" + level + "\\note.json");

        StreamWriter w;

        // Write Config
        w = fconfig.CreateText();
        w.WriteLine(levelconfig.levelconfig.json());
        w.Close();


        // Write Instrument
        w = finstrument.CreateText();
        w.WriteLine(instrument.instrument.json());
        w.Close();


        // Write Game
        w = fnote.CreateText();
        w.WriteLine(note.note.json());
        w.Close();
    }

    public void ResetSong()
    {
        if (levelconfig.levelconfig.type == "Falling Note")
        {
            GameFallingNote gm = GetComponent<GameFallingNote>();
            gm.ResetSong();
        }
        else if (levelconfig.levelconfig.type == "KlikKlik")
        {
            GameKlikKlik gm = GetComponent<GameKlikKlik>();
            gm.ResetSong();
        }
       
    }

    public void LoadLevel()
    {
        StartCoroutine(LoadLevelPersistentData(levelName));
    }

    public void setPlay(bool val)
    {
      
       if(levelconfig.levelconfig.type == "Falling Note")
       {
            GameFallingNote gm = GetComponent<GameFallingNote>();
            gm.play = val;
       }
       else if(levelconfig.levelconfig.type == "KlikKlik")
       {
            GameKlikKlik gm = GetComponent<GameKlikKlik>();
            gm.play = val;
       }
        
        
    }

    IEnumerator LoadLevelPersistentData(string level)
    {
        TextAsset tconfig = Resources.Load<TextAsset>(level + "\\config");
        TextAsset tinstrument = Resources.Load<TextAsset>(level + "\\instrument");
        TextAsset tnote = Resources.Load<TextAsset>(level + "\\note");
        levelconfig.load(level, JsonUtility.FromJson<LevelConfig>(tconfig.text));
        instrument.load(level, JsonUtility.FromJson<Instrument>(tinstrument.text), levelconfig.levelconfig.type);
        note.load(level, JsonUtility.FromJson<Note>(tnote.text));
        yield return null;
    }
}
