using UnityEngine;
using UnityEngine.UI;
using System.Collections;


// ************************************************************************ 
// Class: LoadingSceneManager
// ************************************************************************
public class LoadingSceneManager : Singleton<LoadingSceneManager> 
{
    public Image loadingImg;
    public Image lineImg;
    public Text loadingText;
    public Text loadingDesc;
    public LevelLoadingConfig[] level;

	// ********************************************************************
	// Function:	UnloadLoadingScene()
	// Purpose:		Destroys the loading scene
	// ********************************************************************
	public static void UnloadLoadingScene()
	{
		GameObject.Destroy(instance.gameObject);
	}

    public static void setLoading(string LevelName)
    {
        if(LevelName == "")
        {
            LevelName = "Default";
        }
        for (int i=0; i<instance.level.Length; i++)
        {
            if(instance.level[i].nama == LevelName)
            {
                instance.loadingImg.enabled = true;
                instance.lineImg.enabled = true;
                instance.loadingImg.sprite = instance.level[i].levelImage;
                instance.loadingText.text = instance.level[i].title;
                instance.loadingDesc.text = instance.level[i].desc;
                return;
            }
        }
    }

   


}
