using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameFallingNote : MonoBehaviour
{

    private NoteComponent note;
    private int count;
    private AudioSource aud;
    private bool isPlaying;
    private Countdown countdown;
    private PlayerConf playerConf;

    public float scoreInterval;
    public UIPlay gameUI;
    public LevelConfigComponent conf;

    public GameObject noteObj;
    public AudioClip audioClip;
    public bool play = false;
    public float speed = 5;
    public float spawnHeight = 10;
    public float score = 0;
    public PrefabLoader prefabLoader;


    // Use this for initialization
    void Start()
    {
        playerConf = GameObject.Find("GameManager").GetComponent<PlayerConf>();

        prefabLoader = gameObject.GetComponent<PrefabLoader>();
        note = GetComponent<NoteComponent>();
        aud = GetComponent<AudioSource>();
        countdown = GetComponent<Countdown>();
        gameUI = GameObject.Find("Canvas").GetComponent<UIPlay>();
        conf = GetComponent<LevelConfigComponent>();
        aud.clip = audioClip;
        count = 0;
        noteObj = prefabLoader.Note;
        isPlaying = play;
        
        scoreInterval = (float)3000 / note.note.time.Length;

    }

    // Update is called once per frame
    void Update()
    {
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
                    isPlaying = true;

                    aud.Play();
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
                if (aud.time + (spawnHeight / speed) >= note.note.time[count])
                {
                    GameObject par = GameObject.Find("NotePar");
                    GameObject ob = GameObject.Find(note.note.instname[count]);
                    GameObject ins = Instantiate(noteObj, new Vector2(ob.transform.position.x, ob.transform.position.y + spawnHeight), Quaternion.identity) as GameObject;
                    ins.transform.parent = par.transform;
                    ins.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(conf.levelconfig.namalevel + "\\note");
                    count++;

                }
            }

            //Check if game finish
            if (!aud.isPlaying)
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
