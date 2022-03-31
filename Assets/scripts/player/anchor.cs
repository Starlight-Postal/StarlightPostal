using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anchor : MonoBehaviour
{
    public bool stuck;
    public bool landed;
    public GameObject target;
    public Transform targetTrans;
    public Vector3 tOff;
    public Transform trans;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        stuck = false;
        trans = gameObject.GetComponent<Transform>();
        landed = false;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stuck)
        {
            trans.position = targetTrans.position + tOff;
            rb.bodyType = RigidbodyType2D.Static;
            landed = (target.tag == "landing");
        } else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            landed = false;
        }
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.layer == 0|| c.gameObject.layer == 9)
        {
            //Debug.Log("stick");
            stuck = true;
            target = c.gameObject;
            targetTrans = target.GetComponent<Transform>();
            tOff = trans.position - targetTrans.position;
        }
    }
    /*void OnCollisionExit2D(Collision2D c)
    {
        Debug.Log("unstick");
    }*/
}
