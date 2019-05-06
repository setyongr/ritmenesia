using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrailHandler : MonoBehaviour {
    private Dictionary<int, GameObject> trails = new Dictionary<int, GameObject>();
    private GameObject trailPrefab;
    // Use this for initialization
    void Start () {
        trailPrefab = Resources.Load<GameObject>("Prefabs\\Trail");
	}

    // Update is called once per frame
    void Update()
    {
        // Look for all fingers
        
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

           
           
                // -- Drag
                // ------------------------------------------------
                if (touch.phase == TouchPhase.Began)
                {
                Debug.Log("New Touch");
                    // Store this new value
                    if (trails.ContainsKey(i) == false)
                    {
                       
                        Vector3 position = Camera.main.ScreenToWorldPoint(touch.position);
                        position.z = 0; // Make sure the trail is visible
                        
                        GameObject trail = MakeTrail(position);

                        if (trail != null)
                        {
                            Debug.Log(trail);
                            trails.Add(i, trail);
                        }
                    }
                }
                if(touch.phase == TouchPhase.Stationary)
            {
                if (trails.ContainsKey(i))
                {
                    GameObject trail = trails[i];

                    trail.SetActive(false);
                }
            }
                if (touch.phase == TouchPhase.Moved)
            {
                Debug.Log("Fucking move Touch");
                // Move the trail
                if (trails.ContainsKey(i))
                    {
                        GameObject trail = trails[i];

                    trail.SetActive(true);
                    Camera.main.ScreenToWorldPoint(touch.position);
                        Vector3 position = Camera.main.ScreenToWorldPoint(touch.position);
                        position.z = 0; // Make sure the trail is visible

                        trail.transform.position = position;
                    }
                }
                if (touch.phase == TouchPhase.Ended)
            {
                Debug.Log("Fucnking End Touch");
                // Clear known trails
                if (trails.ContainsKey(i))
                    {
                        GameObject trail = trails[i];

                        // Let the trail fade out
                        Destroy(trail, trail.GetComponent<TrailRenderer>().time);
                        trails.Remove(i);
                    }
                }
            
        }
    }

    public GameObject MakeTrail(Vector3 position)
    {
 
        GameObject trail = Instantiate(trailPrefab) as GameObject;
        trail.transform.position = position;

        return trail;
    }
}
