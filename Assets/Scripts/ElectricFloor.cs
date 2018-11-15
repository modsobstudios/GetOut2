using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricFloor : MonoBehaviour
{
    bool isOn = false;
    int tick = 5;
    float timer = 0.0f;
    Color onColor = Color.yellow;
    Color offColor = Color.grey;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += 0.01f;
        if(timer >= tick)
        {
            timer = 0.0f;
            isOn = !isOn;
            gameObject.GetComponent<BoxCollider2D>().enabled = !gameObject.GetComponent<BoxCollider2D>().enabled;
            if (isOn)
                gameObject.GetComponentInChildren<SpriteRenderer>().color = onColor;
            else
                gameObject.GetComponentInChildren<SpriteRenderer>().color = offColor;
        }
    }

    private void OnTriggerEnter2D(Collider2D _coll)
    {
        if (isOn)
            if (_coll.transform.tag == "Player")
                _coll.transform.GetComponent<Player>().isDead = true;
    }

    private void OnTriggerStay2D(Collider2D _coll)
    {
        if (isOn)
            if (_coll.tag == "Player")
                _coll.transform.GetComponent<Player>().isDead = true;
    }
}
