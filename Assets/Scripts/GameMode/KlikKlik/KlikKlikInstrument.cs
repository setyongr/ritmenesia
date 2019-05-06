using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KlikKlikInstrument : MonoBehaviour {
    public float x = 0;
    public float y = 0;
    public float w;
    public float h;
    public Sprite idle;
    public Sprite hover;
    public Sprite pressed;
    public Text text;

    public float interval = 0.5f;
    
    private GameKlikKlik gm;
    private int clickCount = 0;
    private float endTime = 0;
    private float startTime;
    private bool clickMe = false;
    private bool isPressed = false;
    public UIPlay gameUI;
    public LevelConfigComponent levelconfig;
    // Use this for initialization
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = idle;
        gm = GameObject.Find("LevelManager").GetComponent<GameKlikKlik>();
        gameUI = GameObject.Find("Canvas").GetComponent<UIPlay>();
        levelconfig = GameObject.Find("LevelManager").GetComponent<LevelConfigComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        SetPosition();
        resizeInstrument();
        if (clickCount > 0)
        {
            if(Time.time < endTime)
            {
                clickMe = true;
            }
            else
            {
                //Miss Note
                Debug.Log("Miss!");
                gameUI.popScore(false);
                clickMe = false;
                ResetNote();
            }
        }
        else
        {
            clickMe = false;
            ResetNote();
        }
        /*
        if(clickCount>0 && Time.time < endTime)
        {
            clickMe = true;
        }
        else
        {
            clickMe = false;
            ResetNote();
        }
        */
        //Input handling
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Swap the sprite
                CheckInput(Input.mousePosition, "pressed");
            }

            if (Input.GetMouseButtonUp(0))
            {
                // Swap the sprite
                CheckInput(Input.mousePosition, "idle");
            }

            if (clickMe)
            {
                GetComponent<SpriteRenderer>().sprite = hover;
            }
        }
        else if (Application.platform == RuntimePlatform.Android)
        {

            if (Input.touchCount > 0)
            {
                Touch[] myTouches = Input.touches;
                for (int i = 0; i < Input.touchCount; i++)
                {
                    if (myTouches[i].phase == TouchPhase.Began)
                    {
                        CheckInput(myTouches[i].position, "pressed");
                    }

                    if (myTouches[i].phase == TouchPhase.Ended)
                    {
                        CheckInput(myTouches[i].position, "idle");
                    }
                }
            }
            else
            {
                if (clickMe)
                {
                    GetComponent<SpriteRenderer>().sprite = hover;
                }
            }
        }

        if (clickMe && !isPressed)
        {
            GetComponent<SpriteRenderer>().sprite = hover;
        }
    }

    public void SetPosition()
    {
        // Set the position
        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(x, y, 1));
    }
    void CheckInput(Vector3 pos, string state)
    {
        Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
        Vector2 touchPos = new Vector2(wp.x, wp.y);
        Collider2D hit = Physics2D.OverlapPoint(touchPos);

        if (hit != null)
        {
            if (hit.gameObject.name == gameObject.name)
            {
                if (state == "pressed")
                {
                    Debug.Log("Pressed 0");
                    ProcessNote(state);
                }
                if (state == "idle")
                {
                    ProcessNote(state);
                }
            }
        }
    }


    public void ProcessNote(string state)
    {
        if (state == "pressed")
        {
            Debug.Log("Pressed");
            isPressed = true;
            GetComponent<SpriteRenderer>().sprite = pressed;
        }
        else
        {
            isPressed = false;
            if(clickMe)
            {
                GetComponent<SpriteRenderer>().sprite = hover;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = idle;
            }
            return;
        }

        // Process Note
        if (clickCount > 0)
        {
            clickCount--;
            //Add Score
            gm.IncScore();
        }
        else
        {
            gameUI.popScore(false);
        }

        if (!levelconfig.levelconfig.hideText)
        {         // Set Text
            if (clickCount <= 0)
            {
                text.text = "";
            }
            else
            {
                text.text = clickCount.ToString();
            }
        }
    }

    public void AddNote(int count)
    {
        clickCount = count;
        if (!levelconfig.levelconfig.hideText)
        {
            if (clickCount <= 0)
            {
                text.text = "";
            }
            else
            {
                text.text = clickCount.ToString();
            }
        }
        endTime = Time.time + interval;
    }


    public void ResetNote()
    {
        GetComponent<SpriteRenderer>().sprite = idle;
        clickCount = 0;
        if (!levelconfig.levelconfig.hideText)
        {
            if (clickCount <= 0)
            {
                text.text = "";
            }
            else
            {
                text.text = clickCount.ToString();
            }
        }
        endTime = 0;
    }

    public void resizeInstrument()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        BoxCollider2D bc = GetComponent<BoxCollider2D>();

        // Resize Instrument
        float worldScreenHeight = Camera.main.orthographicSize * 2;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;


        transform.localScale = new Vector3(
            worldScreenWidth / sr.sprite.bounds.size.x * w,
            worldScreenWidth / sr.sprite.bounds.size.y * h, 1);

        // Resize Collider
        bc.size = sr.bounds.size;
        //bc.offset = new Vector2((sr.sprite.bounds.size.x / 2), 0);
    }
}
