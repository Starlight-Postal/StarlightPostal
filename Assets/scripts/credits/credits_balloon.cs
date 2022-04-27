using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class credits_balloon : MonoBehaviour
{
    public float x;
    public float ty;
    public float yv;
    public float fric = 0.95f;
    public float buoyancy = 0.01f;
    public float speed = 0.025f;
    public float descent = 0.01f;
    public Transform trans;
    // Start is called before the first frame update
    void Start()
    {
        trans = gameObject.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        yv *= fric;
        ty -= descent;
        yv += (ty - trans.position.y) * buoyancy;
        trans.position += new Vector3(speed, yv, 0);
    }
}
