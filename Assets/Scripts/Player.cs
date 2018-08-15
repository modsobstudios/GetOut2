using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.A))
                moveLeft();
            if (Input.GetKey(KeyCode.D))
                moveRight();
        }
        else
            dampen();

        if (Input.GetKeyDown(KeyCode.Space))
            jump();

    }

    void moveLeft()
    {
        Debug.Log("Moving Left");
        GetComponent<Rigidbody2D>().AddForce(new Vector2(-10, 0));
    }

    void moveRight()
    {
        Debug.Log("Moving Right");
        GetComponent<Rigidbody2D>().AddForce(new Vector2(10, 0));

    }

    void jump()
    {
        Debug.Log("Jump");
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 250));

    }

    void dampen()
    {
       // GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x * 0.5f, GetComponent<Rigidbody2D>().velocity.y);
    }
}
