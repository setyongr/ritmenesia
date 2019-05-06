using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TapSwipeInstrument : MonoBehaviour
{
    public float x = 0;
    public float y = 0;
    public float w;
    public float h;
    public Sprite idle;
    public Sprite hover;
    public Sprite pressed;
    public Text text;

    public float interval = 2.0f;

    private GameTapSwipe gm;
    private int clickCount = 0;
    private float endTime = 0;
    private float startTime;
    private bool clickMe = false;
    private bool isPressed = false;

    //Swipe
    private float fingerStartTime = 0.0f;
    private Vector2 fingerStartPos = Vector2.zero;

    private bool isSwipe = false;
    private float minSwipeDist = 2.0f;
    private float maxSwipeTime = 0.5f;

    public UIPlay gameUI;

    // Use this for initialization
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = idle;
        gm = GameObject.Find("LevelManager").GetComponent<GameTapSwipe>();
        gameUI = GameObject.Find("Canvas").GetComponent<UIPlay>();
    }

    // Update is called once per frame
    void Update()
    {
        SetPosition();
        resizeInstrument();
        if (clickCount > 0)
        {
            if (Time.time < endTime)
            {
                clickMe = true;
            }
            else
            {
                //Miss Note
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



        if (Input.touchCount > 0)
            {
                Touch[] myTouches = Input.touches;
                for (int i = 0; i < Input.touchCount; i++)
                {
                    switch (myTouches[i].phase)
                    {
                        case TouchPhase.Began:
                            CheckInput(myTouches[i].position, "pressed");
                            break;
                        case TouchPhase.Canceled:
                            isSwipe = false;
                            break;
                        case TouchPhase.Ended:
                            CheckInput(myTouches[i].position, "idle");
                            break;
                    }
                }
            }
            

        if(clickMe && !isPressed)
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
        else
        {
            Debug.Log("Maybe Swipe");
            //Maybe its swipe
            if(state == "pressed")
            {
                isSwipe = true;
                fingerStartTime = Time.time;
                fingerStartPos = touchPos;
                Debug.Log("Maybe Swipe Start");
            }

            if(state == "idle")
            {
                Debug.Log("Maybe Swipe Stop");
                float gestureTime = Time.time - fingerStartTime;
                float gestureDist = (touchPos - fingerStartPos).magnitude;
                Debug.Log("Swipe Dist :" + gestureDist.ToString());
                if (isSwipe && gestureTime < maxSwipeTime && gestureDist > minSwipeDist)
                {
                    Vector2 direction = touchPos - fingerStartPos;
                    Vector2 swipeType = Vector2.zero;

                    if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                    {
                        Debug.Log("Horizontal");
                        // the swipe is horizontal:
                        swipeType = Vector2.right * Mathf.Sign(direction.x);
                    }
                    else
                    {
                        // the swipe is vertical:
                        Debug.Log("Vertical");
                        swipeType = Vector2.up * Mathf.Sign(direction.y);
                    }

                    if(swipeType.y != 0.0f)
                    {
                        if (swipeType.y > 0.0f)
                        {
                            Debug.Log("Process Up");
                            // MOVE UP
                            ProcessNote("swipe");
                        }
                        else
                        {
                            Debug.Log("Process Down");
                            // MOVE DOWN
                            ProcessNote("swipe");
                        }
                    }
                }
            }
        }
    }


    public void ProcessNote(string state)
    {
        if (state == "pressed")
        {
            Debug.Log("Pressed");
            GetComponent<SpriteRenderer>().sprite = pressed;
            isPressed = true;
        }
        else if(state == "swipe")
        {
            if (isPressed)
            {
                // Process Note
                if (clickCount > 0)
                {
                    clickCount--;
                    //Add Score
                    gm.IncScore();
                }
                // Set Text
                if (clickCount <= 0)
                {
                    isPressed = false;
                    text.text = "";
                }
                else
                {
                    text.text = clickCount.ToString();
                }
            }
        }
        else if(state == "idle")
        {
            isPressed = false;
            if (clickMe)
            {
                GetComponent<SpriteRenderer>().sprite = hover;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = idle;
            }
        }

       
    }

    public void AddNote(int count)
    {
        isPressed = false;
        clickCount = count;
        if (clickCount <= 0)
        {
            text.text = "";
        }
        else
        {
            text.text = clickCount.ToString();
        }
        endTime = Time.time + interval;
    }

    public void ResetNote()
    {
        GetComponent<SpriteRenderer>().sprite = idle;
        clickCount = 0;
        if (clickCount <= 0)
        {
            text.text = "";
        }
        else
        {
            text.text = clickCount.ToString();
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
