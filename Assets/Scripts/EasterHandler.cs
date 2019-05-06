using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterHandler : MonoBehaviour {

    private PlayerConf playerConf;
    private int easter1count = 0;

    // Use this for initialization
    void Start () {
        playerConf = GameObject.Find("GameManager").GetComponent<PlayerConf>();

    }

    // Update is called once per frame
    void Update () {
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
        }
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
                easter1count++;
                Debug.Log(easter1count);

                if (easter1count >= 10)
                {
                    if (playerConf.speedMode)
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
        }
    }

}
