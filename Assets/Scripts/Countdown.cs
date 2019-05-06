using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Countdown : MonoBehaviour {
    private Text countdown;

    public float time = 3f;
    public bool start = false;

    // Use this for initialization
    void Start () {
        countdown = GameObject.Find("Countdown").GetComponent<Text>();

    }

    // Update is called once per frame
    void Update () {
        if (start)
        {
            countdown.enabled = true;
            if (time > 1.0f)
            {
                time -= Time.unscaledDeltaTime;
                countdown.text = Mathf.Round(time).ToString();
            }
            else
            {
                countdown.text = "Mulai!!!";
                time -= Time.unscaledDeltaTime;
                start = false;
                countdown.enabled = false;
            }
        }
    }
}
