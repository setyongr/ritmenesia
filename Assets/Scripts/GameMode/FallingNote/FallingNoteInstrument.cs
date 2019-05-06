using UnityEngine;
using System.Collections;

public class FallingNoteInstrument : MonoBehaviour {
    public float x = 0;
    public float y = 0;
    public float w;
    public float h;
    public Sprite idle;
    public Sprite pressed;
    public UIPlay gameUI;
    private bool over = false;
    private GameObject noteObject = null;
    private GameFallingNote gm;
    private bool notePassed = false;
    // Use this for initialization
    void Start () {
        GetComponent<SpriteRenderer>().sprite = idle;
        gm = GameObject.Find("LevelManager").GetComponent<GameFallingNote>();
        gameUI = GameObject.Find("Canvas").GetComponent<UIPlay>();
    }
	
	// Update is called once per frame
	void Update () {
        // Set the position
        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(x, y, 1));
        resizeInstrument();
     
            if (Input.touchCount > 0)
            {
                Touch[] myTouches = Input.touches;
                for (int i = 0; i < Input.touchCount; i++)
                {
                    if (myTouches[i].phase == TouchPhase.Began)
                    {
                        CheckInput(myTouches[i].position, "pressed");
                    }
                    else if (myTouches[i].phase == TouchPhase.Moved)
                    {
                        CheckInput(myTouches[i].position, "idle");
                    }
                    else if (myTouches[i].phase == TouchPhase.Ended)
                    {
                        CheckInput(myTouches[i].position, "idle");
                    }
                }
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = idle;      
            }
        
    }

    void CheckInput(Vector3 pos, string state)
    {
        Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
        Vector2 touchPos = new Vector2(wp.x, wp.y);
        Collider2D hit = Physics2D.OverlapPoint(touchPos);

        if(hit != null)
        {
            if (hit.gameObject.name == gameObject.name)
            {
                if (state == "pressed")
                {
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
            GetComponent<SpriteRenderer>().sprite = idle;
        }
    }


    public void ProcessNote(string state)
    {
        if (state == "pressed")
        {
            GetComponent<SpriteRenderer>().sprite = pressed;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = idle;
            return;
        }

        if (over && noteObject != null)
        {
            Debug.Log("Its Over");
            over = false;
            gm.IncScore();
            noteObject.GetComponent<FallingNote>().Delete();
            noteObject = null;
            // Hit the note
            // Play the sound
        }
        else
        {

            Debug.Log("Its Not Over");
            gameUI.popScore(false);
            // Miss the note
            // Play fail sound
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Note")
        {
            over = true;
            noteObject = coll.gameObject;
            Debug.Log("Note Enter");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Note")
        {
            // Maybe its miss
            if (over && !notePassed)
            {
                gameUI.popScore(false);
            }
            over = false;
            noteObject = null;
            Debug.Log("Note Exit");
        }
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
