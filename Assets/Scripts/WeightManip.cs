using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightManip : MonoBehaviour
{
    [SerializeField]
    List<Material> Lightmats;
    [SerializeField]
    List<Material> Darkmats;
    [SerializeField]
    float range;
    [SerializeField]
    float transferRate;
    [SerializeField]
    float maxMass;
    [SerializeField]
    float minMass;
    [SerializeField]
    bool suck = true;
    int iter = 0;
    Vector2 dir;
    Vector2 ray;
    List<Vector3> positions;
    LineRenderer line;
    Rigidbody2D selfMass;
    int mask;
    [SerializeField]
    int frameRate = 0;
    int frameCount = 0;

    public bool stunned = false;
    // Use this for initialization
    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
        positions = new List<Vector3>();
        selfMass = gameObject.GetComponent<Rigidbody2D>();
        mask = ~((1 << 8) | (1 << 0));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !stunned)
        {
            suck = !suck;
        }
        if (Input.GetMouseButton(1) && !stunned)//Can be changed later
        {
            frameCount++;
            if (frameCount % frameRate == 0)
            {
                if (suck)
                {
                    if (iter < Lightmats.Count)
                    {
                        line.material = Lightmats[iter++];
                    }
                    else
                    {
                        iter = 0;
                        line.material = Lightmats[iter];
                    }
                }
                else
                {
                    if (iter < Darkmats.Count)
                    {
                        line.material = Darkmats[iter++];
                    }
                    else
                    {
                        iter = 0;
                        line.material = Darkmats[iter];
                    }
                }
            }

            if (line.enabled == false)
            {
                line.enabled = true;
            }
            RaycastHit2D hit = new RaycastHit2D();
            dir = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            ray.x = gameObject.transform.position.x;
            ray.y = gameObject.transform.position.y;

            positions.Clear();

            positions.Add(new Vector3(ray.x, ray.y, 0));

            if (transform.localScale.x > 0)
            {

                if (Mathf.Abs(Vector2.Distance(ray, dir)) > range)
                {
                    hit = Physics2D.Raycast(ray, transform.InverseTransformPoint(dir), range, mask);
                }
                else
                {
                    hit = Physics2D.Raycast(ray, transform.InverseTransformPoint(dir), Mathf.Abs(Vector2.Distance(ray, dir)), mask);
                }
            }
            else
            {
                if (Mathf.Abs(Vector2.Distance(ray, dir)) > range)
                {
                    hit = Physics2D.Raycast(ray, new Vector3(-transform.InverseTransformPoint(dir).x, transform.InverseTransformPoint(dir).y, transform.InverseTransformPoint(dir).z), range, mask);
                }
                else
                {
                    hit = Physics2D.Raycast(ray, new Vector3(-transform.InverseTransformPoint(dir).x, transform.InverseTransformPoint(dir).y, transform.InverseTransformPoint(dir).z), Mathf.Abs(Vector2.Distance(ray, dir)), mask);
                }
            }
            if (hit.transform != null)
            {
                positions.Add(new Vector3(hit.point.x, hit.point.y, 0));
            }
            else
            {
                if (Mathf.Abs(Vector2.Distance(ray, dir)) < range)
                {
                    positions.Add(new Vector3((dir.x), (dir.y), 0));
                }
                else
                {
                    Vector2 direction = dir - ray;
                    direction.Normalize();
                    positions.Add(new Vector3((ray.x + direction.x * range), (ray.y + direction.y * range), 0));
                }
            }
            if (hit.collider != null && hit.transform.tag != "Floor" && hit.transform.tag != "Wall")
            {
                //Debug.Log(hit.transform.name);
                if (hit.transform.GetComponent<Rigidbody2D>())
                {
                    if (suck)
                    {
                        if (selfMass.mass < maxMass && hit.transform.GetComponent<Rigidbody2D>().mass > minMass)
                        {
                            hit.transform.GetComponent<Rigidbody2D>().mass -= transferRate;
                            selfMass.mass += transferRate;
                        }
                    }
                    else
                    {
                        if (selfMass.mass > minMass && hit.transform.GetComponent<Rigidbody2D>().mass < maxMass)
                        {
                            hit.transform.GetComponent<Rigidbody2D>().mass += transferRate;
                            selfMass.mass -= transferRate;
                        }
                    }
                }
                //Debug.Log(selfMass.mass);
            }
            line.SetPositions(positions.ToArray());
        }
        else
        {
            if (line.enabled == true)
            {
                line.enabled = false;
            }
        }
    }
}
