using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserButton : MonoBehaviour {

    [SerializeField]
    public LaserTrap trap;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        trap.masterEnabled = !trap.masterEnabled;
        trap.line.enabled = false;
    }
}
