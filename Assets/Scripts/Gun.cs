using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject b = Instantiate(bullet, transform);
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Vector3.Distance(transform.position, Camera.main.transform.position);
            Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
            mousePos.x -= objectPos.x;
            mousePos.y -= objectPos.y;
            b.GetComponent<Bullet>().dir = mousePos;

        }
    }
}
