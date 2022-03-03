using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anchor : MonoBehaviour
{
    public bool stuck;
    public Transform target;
    public Vector3 tOff;
    public Transform trans;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        stuck = false;
        trans = gameObject.GetComponent<Transform>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (stuck)
        {
            trans.position = target.position + tOff;
            rb.bodyType = RigidbodyType2D.Static;
        } else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.layer == 0)
        {
            //Debug.Log("stick");
            stuck = true;
            target = c.gameObject.GetComponent<Transform>();
            tOff = trans.position - target.position;
        }
    }
    /*void OnCollisionExit2D(Collision2D c)
    {
        Debug.Log("unstick");
    }*/
}
