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
        transform.localScale = new Vector3(1, 1, 1);
        width = sr.sprite.bounds.size.x;
        height = sr.sprite.bounds.size.y;
        worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
        transform.localScale = new Vector3(worldScreenWidth / width, worldScreenHeight / height, 1);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
