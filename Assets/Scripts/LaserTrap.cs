using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrap : MasterEnabler
{
    [SerializeField]
    private bool enabled = true;
    [SerializeField]
    private float range;
    [SerializeField]
    private float Frequency = 0;
    [SerializeField]
    private float weightThreshold = 1;
    [SerializeField]
    private float velocityThreshold = 5;
    [SerializeField]
    private Vector2 dir;
    private Vector2 ray;
    private List<Vector3> positions;
    [System.NonSerialized]
    public LineRenderer line;
    private Rigidbody2D selfMass;
    private float elapsedTime = 0;
    private bool flip = false;
    // Use this for initialization
    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
        positions = new List<Vector3>();
        selfMass = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (toggled)
        {
            Laserfy(); //Pew pew!
        }
        else
        {
            line.enabled = false;
        }
        if(toggled && line.enabled == false)
        {
            line.enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (toggled)// If it ain't on, don't check it.
        {
            if (collision.transform.tag == "Bullet")
            {
                //This one is easy... it got shot. Disable.
                line.enabled = false;
                toggled = false;
            }
            else
            {
                if (collision.gameObject.GetComponent<Rigidbody2D>())
                {
                    if (collision.relativeVelocity.magnitude >= velocityThreshold) //If the object's velocity is quick enough
                    {
                        if (collision.transform.GetComponent<Rigidbody2D>().mass >= weightThreshold)
                        {
                            //Object is moving quick enough and is considered heavy
                            //Heavy boop, turn off.
                            toggled = false;
                            line.enabled = false;
                        }
                    }
                }
            }
        }
    }

    void Laserfy()//Does the shooty shoot, frequency = 0 for constant shooty shoot.
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= Frequency && !flip)
        {
            enabled = false;
            elapsedTime = 0;
            flip = !flip;
        }
        if (elapsedTime >= Frequency && flip)
        {
            enabled = true;
            elapsedTime = 0;
            flip = !flip;
        }
        if (enabled)
        {
            if (line.enabled == false)
            {
                line.enabled = true;
            }
            RaycastHit2D hit = new RaycastHit2D();
            dir = -gameObject.transform.right;

            ray.x = gameObject.transform.position.x;
            ray.y = gameObject.transform.position.y;

            positions.Clear();

            positions.Add(new Vector3(ray.x, ray.y, 0));

            if (Mathf.Abs(Vector2.Distance(ray, dir)) > range)
            {
                hit = Physics2D.Raycast(ray, dir, range);
            }
            else
            {
                hit = Physics2D.Raycast(ray, dir, range);
            }
            if (hit.transform != null)
            {
                positions.Add(new Vector3(hit.point.x, hit.point.y, 0));
            }
            else
            {
                positions.Add(new Vector3((ray.x + -transform.right.x * (range)), (ray.y + -transform.right.y * (range)), 0));
            }
            if (hit.collider != null && hit.transform.tag == "Player")
            {
                hit.transform.GetComponent<Player>().isDead = true;
            }
            else if (hit.transform.GetComponent<Rigidbody2D>() && hit.transform.tag != "Wall" && hit.transform.tag != "Floor")
            {
                if (hit.transform.GetComponent<Rigidbody2D>().mass < weightThreshold)
                {
                    Destroy(hit.transform.gameObject);
                }
            }
            line.SetPositions(positions.ToArray());
        }
    }
}
