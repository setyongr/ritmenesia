using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BackgroundConfigComponent : MonoBehaviour {

    public BackgroundConfig conf;
    public Image mainBg;
    public GameObject bgParent;
    public GameObject bgPrefab;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void load(string level, BackgroundConfig cfg)
    {
		Debug.Log ("BG Loaded");
        conf = cfg;
        // Set main bg
        mainBg.sprite = Resources.Load<Sprite>(level + "\\" + conf.main);

        // Set Optional bg
        for(int i = 0; i < conf.optional.Length; i++)
        {
			Debug.Log ("BG Load " + i);

            // Instatiate the background
            GameObject o = (GameObject)Instantiate(bgPrefab);
            o.transform.SetParent(bgParent.transform);
            PlayBackground pb = o.GetComponent<PlayBackground>();

            if(conf.optional[i] != "Default")
            {
                o.GetComponent<Image>().color = Color.white;
                o.GetComponent<Image>().sprite = Resources.Load<Sprite>(level + "\\" + conf.optional[i]);
            }

            pb.posX = conf.x[i];
            pb.posY = conf.y[i];
            pb.width = conf.w[i];
            pb.height = conf.h[i];
        }
        

    }
}
