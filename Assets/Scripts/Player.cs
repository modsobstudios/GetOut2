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


    public GameObject arm;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Controls while touching the floor
        if (grounded)
        {
            // Left/Right
            if (Input.GetKey(KeyCode.A))
                moveLeft();
            if (Input.GetKey(KeyCode.D))
                moveRight();
            // If not trying to move, stop quickly but naturally.
            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
                drag();
            // Jump only if on the floor
            if (Input.GetKeyDown(KeyCode.Space))
                jump();
        }
        // Controls while in the air
        else
        {
            // Different movement force while in the air
            if (Input.GetKey(KeyCode.A))
                driftLeft();
            if (Input.GetKey(KeyCode.D))
                driftRight();
            // Different drag calculation while in the air
            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
                falloff();
        }

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector3 objectPos = Camera.main.WorldToScreenPoint(arm.transform.position);
        mousePos.x -= objectPos.x;
        mousePos.y -= objectPos.y;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        arm.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (Input.GetKeyDown(KeyCode.Mouse0))
            rb.AddForce(-mousePos.normalized * 300);

    }

    //
    // Ground Controls
    //

    void moveLeft()
    {
        // No infinite speed!
        if(rb.velocity.x >= -moveForce)
            rb.AddForce(new Vector2(-moveForce, 0));
    }

    void moveRight()
    {
        // No infinite speed!
        if (rb.velocity.x <= moveForce)
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

    //
    // Air Controls
    //

    void driftLeft()
    {
        // No infinite speed!
        if (rb.velocity.x >= -driftForce)
            rb.AddForce(new Vector2(-driftForce, 0));
    }

    void driftRight()
    {
        // No infinite speed!
        if (rb.velocity.x <= driftForce)
            rb.AddForce(new Vector2(driftForce, 0));
    }

    void falloff()
    {
        rb.velocity = new Vector2(rb.velocity.x * falloffForce, rb.velocity.y);
    }

    //
    // Collision Rules
    //

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
