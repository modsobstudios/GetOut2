using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 dir;
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
}
