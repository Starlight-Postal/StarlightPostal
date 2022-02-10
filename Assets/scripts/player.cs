using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public bool inBalloon = false;
    public Rigidbody2D rb;
    public Transform trans;
    public SpriteRenderer sprite;
    public Collider2D collider;

    public float runSpeed = 0.5f;
    public float gravity = 0.25f;
    public Transform balloon;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        collider = gameObject.GetComponent<Collider2D>();
        trans = gameObject.GetComponent<Transform>();
        balloon = GameObject.Find("balloon").GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (inBalloon)
        {
            sprite.enabled = false;
            collider.enabled = false;
            trans.position = balloon.position;
        } else
        {
            sprite.enabled = true;
            collider.enabled = true;
            rb.velocity = new Vector2(rb.velocity.x * 0.8f, rb.velocity.y); ;
            rb.velocity += new Vector2(0, -gravity);
            if (Input.GetKey("right") || Input.GetKey("d"))
            {
                rb.velocity += new Vector2(runSpeed, 0);
            }
            if (Input.GetKey("left") || Input.GetKey("a"))
            {
                rb.velocity += new Vector2(-runSpeed, 0);
            }

            if (Vector3.Distance(trans.position, balloon.position) < 3f)
            {
                Debug.Log("balloon range!");
                if (Input.GetKey("space"))
                {
                    inBalloon = true;
                }
            }
        }
    }
}
