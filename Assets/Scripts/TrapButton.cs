using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapButton : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> trap;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (GameObject _trap in trap)
        {
            if (_trap.GetComponent<MasterEnabler>())
            {
                _trap.GetComponent<MasterEnabler>().toggled = !_trap.GetComponent<MasterEnabler>().toggled;
            }
        }
    }
}
