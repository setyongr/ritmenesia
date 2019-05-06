using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerConf : MonoBehaviour {

    public List<string> stageName = new List<string>();

    //public string currentLevel;
    public int levelCompleted = 0;
    public int starArchived = 0;
    public bool superMode = false;
    public bool speedMode = false;
    public bool instantDeath = false;
	// Use this for initialization
	void Start () {
        //currentLevel = PlayerPrefs.GetString("currentLevel");
	}
	
    public void setSpeedMode(bool b)
    {
        speedMode = b;
        if(b)
        {
            Time.timeScale = 1.5f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
	
    public void readStatus()
    {
        GameObject lv = GameObject.Find("LvCompText");
        GameObject st = GameObject.Find("StarCompText");

        if (!lv || !st)
        {
            return;
        }

        Text lvt = lv.GetComponent<Text>();
        Text stt = st.GetComponent<Text>();
        lvt.text = levelCompleted.ToString() + " / " + stageName.Count + " Terselesaikan";
        stt.text = starArchived.ToString() + " Bintang";
    }
 
    public string getNextLevel(string name)
    {
		/**
        int currentLevelIdx = stageName.FindIndex(x => x == currentLevel);
        int i = stageName.FindIndex(x => x == name);
        i++;
        if (i < stageName.Count)
        {
            if(currentLevelIdx == i-1)
            {
                PlayerPrefs.SetString("currentLevel", stageName[i]);
            }
            return stageName[i];
        }
        else
        {
            return null;
        }
        */
		return null;
    }
    
    public void initLevel()
    {
        levelCompleted = 0;
        starArchived = 0;
        //currentLevel = PlayerPrefs.GetString("currentLevel");
        Debug.Log("Init Level Called");
        GameObject level = GameObject.Find("Level");
        if (!level)
        {
            return;
        }

        
        Debug.Log("Has Level");
        /*
		if (currentLevel == "")
        {
            currentLevel = stageName[0];
            PlayerPrefs.SetString("currentLevel", currentLevel);
        }


        if (!superMode)
        {
            //Lock All Level
            foreach (Transform c in level.transform)
            {
                c.gameObject.GetComponent<Level>().setLocked();
            }

        }
		*/

		/*
        for (int i = 0; i < stageName.Count; i++)
        {
           
            if (stageName[i] == currentLevel)
            {
                // Set Unlocked
                foreach (Transform c in level.transform)
                {
                    if (c.gameObject.name == currentLevel)
                    {
                        if (PlayerPrefs.HasKey("star" + stageName[i]))
                        {
                            starArchived += PlayerPrefs.GetInt("star" + stageName[i]);
                            c.gameObject.GetComponent<Level>().setComplete(PlayerPrefs.GetInt("star" + stageName[i]));
                        }
                        else
                        {
                            c.gameObject.GetComponent<Level>().setUnlocked();
                        }
                    }
                }
                break;
            }

            // Set Stage to Complete and Add Star
            foreach (Transform c in level.transform)
            {
                if (c.gameObject.name == stageName[i])
                {
                    levelCompleted++;
                    starArchived += PlayerPrefs.GetInt("star" + stageName[i]);
                    Level lv = c.gameObject.GetComponent<Level>();
                    lv.setComplete(PlayerPrefs.GetInt("star" + stageName[i]));
                }
            }
        }
		*/
		for (int i = 0; i < stageName.Count; i++)
		{


			// Set Unlocked
			foreach (Transform c in level.transform)
			{
				
                if(c.gameObject.name == stageName[i])
                {
                    if (PlayerPrefs.HasKey("star" + stageName[i]))
                    {
                        levelCompleted++;
                        starArchived += PlayerPrefs.GetInt("star" + stageName[i]);
                        c.gameObject.GetComponent<Level>().setComplete(PlayerPrefs.GetInt("star" + stageName[i]));
                    }
                }
			}
				
		}
    }


}
