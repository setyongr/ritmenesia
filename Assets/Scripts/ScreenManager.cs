// ************************************************************************ 
// Imports 
// ************************************************************************ 
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


// ************************************************************************ 
// Class: ScreenManager
// ************************************************************************
public class ScreenManager : MonoBehaviour {
	
	
	// ********************************************************************
	// Exposed Data Members 
	// ********************************************************************
	[SerializeField]
	private FadeSprite m_blackScreenCover;
	[SerializeField]
	private float m_minDuration = 1.5f;
	
	


    public void LoadLevel(string name, string level)
    {

        StartCoroutine(LoadSceneAsync(name, level));
    }

	// ********************************************************************
	// Function:	LoadScene()
	// Purpose:		Loads the supplied scene
	// ********************************************************************
	public IEnumerator LoadSceneAsync(string sceneName, string level)
	{
		// Fade to black
		yield return StartCoroutine(m_blackScreenCover.FadeIn());
		// Load loading screen
		yield return SceneManager.LoadSceneAsync("LoadingScreenPlay");
        LoadingSceneManager.setLoading(level);
	
		// !!! unload old screen (automatic)

		// Fade to loading screen
		yield return StartCoroutine(m_blackScreenCover.FadeOut());
		
		float endTime = Time.time + m_minDuration;
        
        // Load level async
        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        //GetComponent<PlayerConf>().initLevel();
        if (level != "")
        {
            GameObject lm = GameObject.Find("LevelManager");
            if (lm)
            {
                yield return StartCoroutine(lm.GetComponent<Loader>().LoadLevelEnum(level));
            }
        }


        
        while (Time.time < endTime)
			yield return null;

        // Play music or perform other misc tasks

        // Fade to black
        yield return StartCoroutine(m_blackScreenCover.FadeIn());

        // !!! unload loading screen
        SceneManager.UnloadScene("LoadingScreenPlay");
        //LoadingSceneManager.UnloadLoadingScene();
        yield return Resources.UnloadUnusedAssets();


        // Fade to new screen
        yield return StartCoroutine(m_blackScreenCover.FadeOut());
	}


}
