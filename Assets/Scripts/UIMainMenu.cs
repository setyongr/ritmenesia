using UnityEngine;
using System.Collections;

public class UIMainMenu : MonoBehaviour {
    public GameObject infoPanel;

    private PlayerConf playerConf;

    private int easter1count = 0;
    private int easter2count = 0;

    void Start()
    {
        playerConf = GameObject.Find("GameManager").GetComponent<PlayerConf>();
    }
    public void Play()
    {
        GameObject m = GameObject.Find("GameManager");
        m.GetComponent<ScreenManager>().LoadLevel("Map", "");
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void showInfo()
    {
        infoPanel.SetActive(true);
    }

    public void hideInfo()
    {
        infoPanel.SetActive(false);
    }

    public void easter1Toogler()
    {
        easter1count++;
        Debug.Log(easter1count);

        if (easter1count >= 10)
        {
            if (!playerConf.speedMode)
            {
                playerConf.setSpeedMode(true);
            }
            else
            {
                playerConf.setSpeedMode(false);
            }
            easter1count = 0;
        }
    }


    public void easter2Toogler()
    {
        easter2count++;
        Debug.Log(easter2count);

        if (easter2count >= 10 && easter1count >= 5)
        {
            if (!playerConf.instantDeath)
            {
                Time.timeScale = 0.9f;
                playerConf.instantDeath = true;
            }
            else
            {
                Time.timeScale = 1;
                playerConf.instantDeath = false;
            }
            easter2count = 0;
        }
    }
}
