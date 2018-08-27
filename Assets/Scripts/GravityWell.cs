using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityWell : MonoBehaviour {

    public Player thePlayer;
	// Use this for initialization
	void Start ()
    {
        thePlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            if(thePlayer.transform.position.x > this.gameObject.transform.position.x)
            {
                thePlayer.GetComponent<Rigidbody2D>().AddForce(new Vector2(-100, 0));
            }
            else if(thePlayer.transform.position.x < this.gameObject.transform.position.x)
            {
                thePlayer.GetComponent<Rigidbody2D>().AddForce(new Vector2(100, 0));
            }
        }
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            if (thePlayer.transform.position.x > this.gameObject.transform.position.x)
            {
                thePlayer.GetComponent<Rigidbody2D>().AddForce(new Vector2(-15, 0));
            }
            else if (thePlayer.transform.position.x < this.gameObject.transform.position.x)
            {
                thePlayer.GetComponent<Rigidbody2D>().AddForce(new Vector2(15, 0));
            }

             if (thePlayer.transform.position.y > this.gameObject.transform.position.y)
            {
                thePlayer.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -15));
            }
            else if (thePlayer.transform.position.y < this.gameObject.transform.position.y)
            {
                thePlayer.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 15));
            }

        }
    }
}
