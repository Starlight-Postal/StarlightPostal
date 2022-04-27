using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class credits_sky : MonoBehaviour
{
    public Transform trans;
    public Vector2 speed;
    // Start is called before the first frame update
    void Start()
    {
        trans = gameObject.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        trans.position += new Vector3(speed.x, speed.y, 0);
    }
}
