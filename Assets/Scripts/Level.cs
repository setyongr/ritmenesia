using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Level : MonoBehaviour {

    public Sprite Locked;
    public Sprite Unlocked;
    public Sprite Star1;
    public Sprite Star2;
    public Sprite Star3;

    public bool isLocked = true;

    public bool hasStar = false;

    private Image img;
    private Button btn;
	// Use this for initialization
	void Awake () {
        img = GetComponent<Image>();

        btn = GetComponent<Button>();
    }
	
	// Update is called once per frame
	void Update () {
        if (isLocked)
        {
            setLocked();
            
        }
        else
        {
            if(!hasStar) setUnlocked();
        }
	}

    void setImage(Sprite image)
    {
        img.sprite = image;
    }

    public void setLocked()
    {
        setImage(Locked);
        btn.interactable = false;
        isLocked = true;
    }

    public void setUnlocked()
    {
        setImage(Unlocked); 
        btn.interactable = true;
        isLocked = false;
    }

    public void setComplete(int star)
    {
        setUnlocked();
        hasStar = true;
        switch (star)
        {
            case 1:
                setImage(Star1);
                break;
            case 2:
                setImage(Star2);
                break;
            case 3:
                setImage(Star3);
                break;
            default:
                break;
        }
            
    }
}
