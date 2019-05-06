using UnityEngine;
using System.Collections;

public class UIMap : MonoBehaviour {

    private PlayerConf conf; 
    void Start()
    {
        conf = GameObject.Find("GameManager").GetComponent<PlayerConf>();
        conf.initLevel();
        conf.readStatus();
    }
    

    public void back()
    {
        GameObject m = GameObject.Find("GameManager");
        m.GetComponent<ScreenManager>().LoadLevel("MainMenu", "");
    }

    public void backToMap()
    {
        GameObject m = GameObject.Find("GameManager");
        m.GetComponent<ScreenManager>().LoadLevel("Map", "");
    }




    public void play(string level)
    {
        GameObject m = GameObject.Find("GameManager");
        m.GetComponent<ScreenManager>().LoadLevel("Play", level);
    }

    public void loadScene(string scene)
    {
        GameObject m = GameObject.Find("GameManager");
        m.GetComponent<ScreenManager>().LoadLevel(scene, "");
    }
}
