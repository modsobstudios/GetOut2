using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 dir;
    int count = 0;
    int limit = 1;
    float time = 0.0f;
    // Use this for initialization
    void Start()
    {
        transform.parent = null;
        GetComponent<Rigidbody2D>().AddForce(dir.normalized * 1000);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Floor" || collision.transform.tag == "Wall")
        {
            Debug.Log("Tick.");
            count++;
            if(count > limit)
                Destroy(this.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        time += 0.01f;
        if (time > 1)
            Destroy(this.gameObject);
    }
}
