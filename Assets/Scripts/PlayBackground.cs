using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayBackground : MonoBehaviour {
    
    public float posX;
    public float posY;

    public float width;
    public float height;
    
    public RectTransform t;
    // Use this for initialization
    void Start()
    {
        //
        t = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
       
        t.anchorMin = new Vector2(posX - width / 2, posY - height / 2);
        t.anchorMax = new Vector2(posX + width / 2, posY + height / 2);
        t.localScale = new Vector3(1, 1, 1);
    }
}
