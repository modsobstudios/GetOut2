﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MasterEnabler
{

    public GameObject toSpawn;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (toggled)
        {
            Instantiate(toSpawn,gameObject.transform.position,gameObject.transform.rotation);
            toggled = false;
        }
    }
}
