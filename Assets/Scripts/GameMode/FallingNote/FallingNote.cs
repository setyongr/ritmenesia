using UnityEngine;
using System.Collections;

public class FallingNote : MonoBehaviour {

    private GameFallingNote gm;
    // Use this for initialization
	void Start () {
        gm = GameObject.Find("LevelManager").GetComponent<GameFallingNote>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0, -gm.speed * Time.deltaTime, 0);

        if(transform.position.y < -5)
        {
          Delete();
        }

    }

    public void Delete()
    {
        Debug.Log("Remove the note");
        Destroy(this.gameObject);
    }
}
