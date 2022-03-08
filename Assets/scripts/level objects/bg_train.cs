using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bg_train : MonoBehaviour
{
    Transform trans;
    public float x1;
    public float x2;
    public float speed = 0.001f;
    float x;
    // Start is called before the first frame update
    void Start()
    {
        trans = gameObject.GetComponent<Transform>();
        x = x1;
    }

    // Update is called once per frame
    void Update()
    {
        x += (x2 - x) * speed;
        trans.position = new Vector3(x,trans.position.y, trans.position.z);
    }
}
