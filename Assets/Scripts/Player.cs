using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool grounded = true;
    Rigidbody2D rb;
    float moveForce = 10.0f;
    float jumpForce = 250.0f;
    float driftForce = 3.0f;
    float dragForce = 0.5f;
    float falloffForce = 0.99f;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (grounded)
        {
            if (Input.GetKey(KeyCode.A))
                moveLeft();
            if (Input.GetKey(KeyCode.D))
                moveRight();
            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
                drag();
            if (Input.GetKeyDown(KeyCode.Space))
                jump();
        }
        else
        {
            if (Input.GetKey(KeyCode.A))
                driftLeft();
            if (Input.GetKey(KeyCode.D))
                driftRight();
            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
                falloff();
        }



    }

    void moveLeft()
    {
        rb.AddForce(new Vector2(-moveForce, 0));
    }

    void moveRight()
    {
        rb.AddForce(new Vector2(moveForce, 0));
    }

    void jump()
    {
        rb.AddForce(new Vector2(0, jumpForce));
    }

    void drag()
    {
        rb.velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x * dragForce, GetComponent<Rigidbody2D>().velocity.y);
    }

    void driftLeft()
    {
        rb.AddForce(new Vector2(-driftForce, 0));
    }

    void driftRight()
    {
        rb.AddForce(new Vector2(driftForce, 0));
    }

    void falloff()
    {
        rb.velocity = new Vector2(rb.velocity.x * falloffForce, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Floor")
            grounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Floor")
            grounded = false;
    }
}
