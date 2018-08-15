using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    SpriteRenderer sr;
    float worldScreenHeight;
    float worldScreenWidth;
    float width;
    float height;

    // Use this for initialization
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
  
        // Make sure scale is origin
        transform.localScale = new Vector2(1, 1);
        
        // Get size of background image
        width = sr.sprite.bounds.size.x;
        height = sr.sprite.bounds.size.y;

        // Get size of window at runtime
        worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        // Scale to scale
        transform.localScale = new Vector2(worldScreenWidth / width, worldScreenHeight / height);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
