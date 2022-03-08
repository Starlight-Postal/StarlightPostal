using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bg_train : MonoBehaviour
{
    Transform trans;
    public float x1;
    public float x2;
    public float speed = 0.005f;
    public float vel = 0.1f;
    float x;
    // Start is called before the first frame update
    void Start()
    {
        trans = gameObject.GetComponent<Transform>();
        x = x1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float xv = (x2 - x) * speed;
        if (xv <= vel)
        {
            x += xv;
        } else
        {
            x += vel;
        }
        trans.position = new Vector3(x,trans.position.y, trans.position.z);
    }
}
