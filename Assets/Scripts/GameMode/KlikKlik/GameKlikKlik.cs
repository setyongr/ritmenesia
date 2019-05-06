using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameKlikKlik : MonoBehaviour {

    private NoteComponent note;
    private int count;
    private AudioSource aud;
    private bool isPlaying;
    private Countdown countdown;
    private PlayerConf playerConf;

    public float scoreInterval;
    public UIPlay gameUI;
    public LevelConfigComponent conf;

   
    public AudioClip audioClip;
    public bool play = false;
    public float speed = 5;
    public float spawnHeight = 10;
    public float score = 0;
    // Use this for initialization
    void Start () {
        playerConf = GameObject.Find("GameManager").GetComponent<PlayerConf>();

        note = GetComponent<NoteComponent>();
        aud = GetComponent<AudioSource>();
        countdown = GetComponent<Countdown>();
        gameUI = GameObject.Find("Canvas").GetComponent<UIPlay>();
        conf = GetComponent<LevelConfigComponent>();
        aud.clip = audioClip;
        count = 0;
        isPlaying = play;
        scoreInterval = (float)3000 / note.note.time.Length;
    }
	
	// Update is called once per frame
	void Update () {
        aud.pitch = Time.timeScale;
        /*
        if (Input.touchCount > 0 && !play && playText.activeInHierarchy)
        {
            playText.SetActive(false);
            play = true;
        }
        */


        if (play)
        {
            if (!isPlaying)
            {
                // Start Coundown

                if (!countdown.start)
                {
                    Time.timeScale = 0;
                    countdown.time = 4f;
                    countdown.start = true;
                }

                if (countdown.time < 1)
                {
                    if (playerConf.speedMode)
                    {
                        Time.timeScale = 1.5f;
                    }
                    else
                    {
                        Time.timeScale = 1;
                    }
                    aud.Play();
                    isPlaying = true;
                }

            }
        }
        else
        {
            isPlaying = false;
            aud.Pause();
        }

        // Read note
        if (play && isPlaying)
        {
            //Update score
            gameUI.setScoreText(Mathf.RoundToInt(score).ToString());
            if (count < note.note.time.Length)
            {
               
                string instCount = note.note.instname[count];
                float instTime = note.note.time[count];
                if(aud.time >= instTime)
                {
                    int tmpCount = 1;
                    int countX = count;
                    for (int i = countX + 1; i < note.note.time.Length; i++)
                    {
                        if (note.note.instname[i] != instCount || (note.note.time[i] - instTime) > 1.0f)
                        {
                            Debug.Log("Its same");
                            break;
                        }
                        Debug.Log("Add Count");
                        tmpCount++;
                        count = i;
                    }
                    count++;
                    GameObject ob = GameObject.Find(instCount);
                    ob.GetComponent<KlikKlikInstrument>().AddNote(tmpCount);
                }
            }

            //Check if game finish
            if (!aud.isPlaying && isPlaying)
            {
                gameUI.ShowComplete(true);
                gameUI.setScore(Mathf.RoundToInt(score), conf.levelconfig.namalevel);

            }
        }

    }

    public bool getIsPlaying()
    {
        return isPlaying;
    }

    public void ResetSong()
    {
        aud.Stop();
        count = 0;
    }

    public void RestartLevel()
    {
        ResetSong();

        //Reset all note
        GameObject[] ob = GameObject.FindGameObjectsWithTag("Instrument");
        foreach(GameObject o in ob){
            o.GetComponent<KlikKlikInstrument>().ResetNote();
        }

        play = false;
        isPlaying = false;
        score = 0;
        gameUI.ShowComplete(false);
        gameUI.showPlay(true);
    }

    public void IncScore()
    {
        score += scoreInterval;

        gameUI.popScore(true);
    }

}
