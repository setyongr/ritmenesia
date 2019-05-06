using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InstrumentComponent : MonoBehaviour {

    public Instrument instrument;
    public PrefabLoader prefabLoader;
    public GameObject prfK;
    // Use this for initialization
    void Start () {
        prefabLoader = gameObject.GetComponent<PrefabLoader>();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void init(string level, string type)
    {

        GameObject prf = prfK;
        GameObject inst = GameObject.Find("Instrument");

        // Remove all child
        foreach (Transform c in inst.transform)
        {
            GameObject.Destroy(c.gameObject);
        }

        // Generate Instrument
        for (int i = 0; i < instrument.nama.Length; i++)
        {
            GameObject obj = (GameObject)Instantiate(prf, Vector3.zero, Quaternion.identity);
            obj.transform.parent = inst.transform;
            obj.name = instrument.nama[i];

            if (type == "Falling Note")
            {
                obj.AddComponent<FallingNoteInstrument>();
                FallingNoteInstrument note = obj.GetComponent<FallingNoteInstrument>();
                note.idle = Resources.Load<Sprite>(level + "\\" + instrument.idle[i]);
                note.pressed = Resources.Load<Sprite>(level + "\\" + instrument.pressed[i]);
                note.x = instrument.x[i];
                note.y = instrument.y[i];
                note.w = instrument.w[i];
                note.h = instrument.h[i];
            }
            else if (type == "KlikKlik")
            {
                GameObject prfText = prefabLoader.Text;
                Canvas parentCanvas = GameObject.Find("TopCanvas").GetComponent<Canvas>();
                obj.AddComponent<KlikKlikInstrument>();
                KlikKlikInstrument note = obj.GetComponent<KlikKlikInstrument>();
                GameObject tKlik = Instantiate(prfText);
                tKlik.transform.SetParent(parentCanvas.transform, false);

                note.idle = Resources.Load<Sprite>(level + "\\" + instrument.idle[i]);
                note.pressed = Resources.Load<Sprite>(level + "\\" + instrument.pressed[i]);
                note.hover = Resources.Load<Sprite>(level + "\\" + instrument.hover[i]);
                note.x = instrument.x[i];
                note.y = instrument.y[i];
                note.w = instrument.w[i];
                note.h = instrument.h[i];
                note.SetPosition();
                Vector2 pos = note.transform.position;
                Vector2 v = Camera.main.WorldToViewportPoint(pos);
                tKlik.GetComponent<RectTransform>().anchorMin = v;
                tKlik.GetComponent<RectTransform>().anchorMax = v;


                note.text = tKlik.GetComponent<Text>();
            }
            else if (type == "TapSwipe")
            {
                GameObject prfText = prefabLoader.Text;
                Canvas parentCanvas = GameObject.Find("TopCanvas").GetComponent<Canvas>();
                obj.AddComponent<TapSwipeInstrument>();
                TapSwipeInstrument note = obj.GetComponent<TapSwipeInstrument>();
                GameObject tKlik = Instantiate(prfText);
                tKlik.transform.SetParent(parentCanvas.transform, false);

                note.idle = Resources.Load<Sprite>(level + "\\" + instrument.idle[i]);
                note.pressed = Resources.Load<Sprite>(level + "\\" + instrument.pressed[i]);
                note.hover = Resources.Load<Sprite>(level + "\\" + instrument.hover[i]);
                note.x = instrument.x[i];
                note.y = instrument.y[i];
                note.w = instrument.w[i];
                note.h = instrument.h[i];
                note.SetPosition();
                Vector2 pos = note.transform.position;
                Vector2 v = Camera.main.WorldToViewportPoint(pos);
                tKlik.GetComponent<RectTransform>().anchorMin = v;
                tKlik.GetComponent<RectTransform>().anchorMax = v;


                note.text = tKlik.GetComponent<Text>();
            }


        }
    }
    public void load(string level, Instrument cfg, string type)
    {
        Debug.Log("Instrument Load");
        instrument = cfg;
        init(level, type);
    }
}
