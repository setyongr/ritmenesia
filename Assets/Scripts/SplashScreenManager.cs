using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SplashScreenManager : MonoBehaviour {

    public GameObject linkenSkyLogo;
    public GameObject gemastikLogo;


    [SerializeField]
    private FadeSprite m_blackScreenCover;
    [SerializeField]
    private float m_minDuration = 1.5f;

    // Use this for initialization
    void Start () {
        StartCoroutine(FadeInit());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator FadeInit()
    {
        float endTime = Time.time + m_minDuration;
        while (Time.time < endTime)
            yield return null;
        linkenSkyLogo.SetActive(true);
        yield return StartCoroutine(m_blackScreenCover.FadeOut());
        endTime = Time.time + m_minDuration;
        while (Time.time < endTime)
            yield return null;
        yield return StartCoroutine(m_blackScreenCover.FadeIn());
        linkenSkyLogo.SetActive(false);
        gemastikLogo.SetActive(true);
        yield return StartCoroutine(m_blackScreenCover.FadeOut());
        endTime = Time.time + m_minDuration;
        while (Time.time < endTime)
            yield return null;
        yield return StartCoroutine(m_blackScreenCover.FadeIn());
        yield return SceneManager.LoadSceneAsync("MainMenu");
        yield return StartCoroutine(m_blackScreenCover.FadeOut());
    }
}
