using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIPlay : MonoBehaviour {

    public Image starImg;
    public Text completeScore;
    public Sprite star1;
    public Sprite star2;
    public Sprite star3;
    public Button nextLevelButton;
    public Text descText;
    public GameObject pausePanel;
    public Text judulText;
    public Text scorePop;
    public Text completeStageName;
    private PlayerConf conf;
    private LevelConfig levelConf;
    public GameObject completePanel;
    public GameObject helpPanel;
    public Text helpText;
    public Text scoreText;
    public GameObject playBt;
    public GameObject failPanel;
    public Image performace;

    private string nextLevel;
    private float endTime;

    private bool isPlay = false;
    // Use this for initialization
    void Start () {
        conf = GameObject.Find("GameManager").GetComponent<PlayerConf>();
        levelConf = GameObject.Find("LevelManager").GetComponent<LevelConfigComponent>().levelconfig;
    }
	
	// Update is called once per frame
	void Update () {
	    //Hide score pop
        if(Time.time > endTime)
        {
            scorePop.gameObject.SetActive(false);
        }

	}

    public void setScoreText(string val)
    {
        scoreText.text = val;
    }
    public void popScore(bool ok)
    {
        scorePop.gameObject.SetActive(false);
        if (isPlay)
        {
            if (ok)
            {
                scorePop.text = "Bagus!!";
                scorePop.color = Color.white;
                if (performace.fillAmount < 1)
                {
                    performace.fillAmount += 0.1f;
                }
            }
            else
            {
                Debug.Log("Fail");
                scorePop.text = "Terlewat";
                scorePop.color = Color.red;
                Handheld.Vibrate();
                if (conf.instantDeath)
                {
                    Failed();
                    return;
                }
                if (performace.fillAmount > 0)
                {
                    performace.fillAmount -= 0.1f;
                }
                else
                {
                    //Show Failed
                    Failed();
                }
            }
        }
        scorePop.gameObject.SetActive(true);
        endTime = Time.time + 0.5f;
 
    }
    public void setJudul(string asal, string judul)
    {
        judulText.text = asal + " - " + judul;
    }
    public void setDesc()
    {

    }
    public void NextLevel()
    {
        GameObject m = GameObject.Find("GameManager");
        m.GetComponent<ScreenManager>().LoadLevel("Play", nextLevel);
    }

    public void Peta()
    {
        GameObject m = GameObject.Find("GameManager");
        m.GetComponent<ScreenManager>().LoadLevel("Map", "");
    }

    public void RestartLevel()
    {
        performace.fillAmount = 1;
        Pause(false);
        string type = levelConf.type;

        if (type == "Falling Note")
        {
            GameFallingNote gm = GameObject.Find("LevelManager").GetComponent<GameFallingNote>();
            gm.RestartLevel();
        }else if(type == "KlikKlik")
        {
            GameKlikKlik gm = GameObject.Find("LevelManager").GetComponent<GameKlikKlik>();
            gm.RestartLevel();
        }
        else if (type == "TapSwipe")
        {
            GameTapSwipe gm = GameObject.Find("LevelManager").GetComponent<GameTapSwipe>();
            gm.RestartLevel();
        }

        ShowComplete(false);
        showPlay(true);
        failPanel.SetActive(false);
    }


    public void setScore(int score, string LevelName)
    {
        completeScore.text = score.ToString();
        completeStageName.text = LevelName;
        int star = 0;
        if (score >= 2000)
        {
            star = 3;
        }
        else if (score >= 1000)
        {
            star = 2;
        }
        else if (score >= 0)
        {
            star = 1;
        }

        setStar(star);
        // Save Highscore

        if(score > PlayerPrefs.GetInt("score" + LevelName))PlayerPrefs.SetInt("score" + LevelName, score);

        if(star > PlayerPrefs.GetInt("star" + LevelName))PlayerPrefs.SetInt("star" + LevelName, star);

        if (star == 3)
        {
            //nextLevelButton.interactable = true;
            nextLevel = conf.getNextLevel(LevelName);
            if (nextLevel != null)
            {
                nextLevelButton.interactable = true;
            }
        }
    }

    public void showHelp()
    {
        if (levelConf.type == "Falling Note")
        {
            helpText.text = "Tekan pada alat musik pada saat NOT menyentuh alat musik";
        } else if (levelConf.type == "KlikKlik")
        {
            helpText.text = "Tekan pada alat musik sebanyak nomor yang muncul atau dimana alat musik menyala";
        }else if(levelConf.type == "TapSwipe")
        {
            helpText.text = "Tekan pada alat musik, lalu gesek layar sesuai dengan nomor yang munucul";
        }

        PauseGame(true);
        PauseTime(true);
        helpPanel.SetActive(true);
    }

    public void hideHelp()
    {
        playBt.SetActive(true);
        helpPanel.SetActive(false);
    }
    public void setStar(int star)
    {
        switch (star)
        {
            case 1:
                starImg.sprite = star1;
                break;
            case 2:
                starImg.sprite = star2;
                break;
            case 3:
                starImg.sprite = star3;
                break;
        }
    }

    public void ShowComplete(bool val)
    {
        completePanel.SetActive(val);
    }

    public void PauseTime(bool val)
    {
        if (val)
        {
          
            Time.timeScale = 0;
        }
        else
        {
            if (conf.speedMode)
            {
                Time.timeScale = 1.5f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }

    public void PauseGame(bool val)
    {
        isPlay = !val;
        string type = levelConf.type;

        if (type == "Falling Note")
        {
            GameFallingNote gm = GameObject.Find("LevelManager").GetComponent<GameFallingNote>();
            gm.play = !val;
        }
        else if (type == "KlikKlik")
        {
            GameKlikKlik gm = GameObject.Find("LevelManager").GetComponent<GameKlikKlik>();
            gm.play = !val;
        }
        else if (type == "TapSwipe")
        {
            GameTapSwipe gm = GameObject.Find("LevelManager").GetComponent<GameTapSwipe>();
            gm.play = !val;
        }

        PauseTime(val);

    }

    public void Pause(bool val)
    {
        PauseGame(val);
        PauseTime(val);
        
        pausePanel.SetActive(val);
        playBt.SetActive(false); 
    }

    
    public void showPlay(bool val)
    {
        playBt.SetActive(val);
    }

    public void Menu()
    {
        if (conf.speedMode)
        {
            Time.timeScale = 1.5f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        GameObject m = GameObject.Find("GameManager");
        m.GetComponent<ScreenManager>().LoadLevel("Jawa", "");
    }

    public void Failed()
    {
        PauseGame(true);
        PauseTime(true);
        failPanel.SetActive(true);
    }
}
