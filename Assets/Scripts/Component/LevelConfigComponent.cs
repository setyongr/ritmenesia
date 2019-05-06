using UnityEngine;
using System.Collections;

public class LevelConfigComponent : MonoBehaviour {

    public LevelConfig levelconfig;
    public UIPlay uiPlay;
	// Use this for initialization
	void Start () {
	
	}

    public void load(string level, LevelConfig cfg)
    {
        levelconfig = cfg;

        if (levelconfig.type == "Falling Note")
        {
            GameFallingNote gt = gameObject.GetComponent<GameFallingNote>();
            if (gt != null)
            {
                Destroy(gt);
            }
            GameFallingNote gm = gameObject.AddComponent<GameFallingNote>();
            gm.audioClip = Resources.Load<AudioClip>(level + "\\" + levelconfig.lagu);
        }else if(levelconfig.type == "KlikKlik")
        {
            GameKlikKlik gt = gameObject.GetComponent<GameKlikKlik>();
            if(gt != null)
            {
                Destroy(gt);
            }
            GameKlikKlik gm = gameObject.AddComponent<GameKlikKlik>();
            gm.audioClip = Resources.Load<AudioClip>(level + "\\" + levelconfig.lagu);
        }else if(levelconfig.type == "TapSwipe")
        {
            GameTapSwipe gt = gameObject.GetComponent<GameTapSwipe>();
            if (gt != null)
            {
                Destroy(gt);

                TrailHandler lh = gameObject.GetComponent<TrailHandler>();
                if (lh!= null) Destroy(lh);
            }
            GameTapSwipe gm = gameObject.AddComponent<GameTapSwipe>();
            TrailHandler llh = gameObject.AddComponent<TrailHandler>();
            gm.audioClip = Resources.Load<AudioClip>(level + "\\" + levelconfig.lagu);
        }

        uiPlay.setJudul(levelconfig.namalevel, levelconfig.namalagu);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
