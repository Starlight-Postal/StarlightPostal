using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balloon : MonoBehaviour
{
    float bouyancy;
    float lean;
    public float volume = 1;
    public float weight = 1;
    public float airFric = 0.975f;
    public float gravity = 0.1f;
    public float atmPressure = 1;
    public Rigidbody2D rb;
    public Transform trans;
    public SpriteRenderer sprite;

    public wind wind;
    public GameObject anchor;
    public bool anchored;
    public float anchorD = 10;

    public player player;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        trans = gameObject.GetComponent<Transform>();
        sprite = gameObject.GetComponent<SpriteRenderer>();

        lean = 0;

        player = GameObject.Find("player").GetComponent<player>();

        //wind = GameObject.Find("wind").GetComponent<wind>();

        anchor = GameObject.Find("anchor");
        anchor.SetActive(true);
        anchored = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity *= airFric;

        BuoyantControl();
        //ThrustControl();

        rb.velocity += wind.getWind(trans.position.x, trans.position.y);


        //Debug.Log(bouyancy+" - "+gravity*weight);
        //Debug.Log(wind.getWind(trans.position.x, trans.position.y));

        if (anchored)
        {
            Transform aTrans = anchor.GetComponent<Transform>();
            Rigidbody2D anchorRB = anchor.GetComponent<Rigidbody2D>();
            float d = Vector3.Distance(aTrans.position, trans.position);
            if (d>anchorD)
            {
                float a = Mathf.Atan2(trans.position.x - aTrans.position.x, trans.position.y - aTrans.position.y);
                rb.velocity += new Vector2(Mathf.Sin(a) * (d - anchorD) * -0.2f, Mathf.Cos(a) * (d - anchorD)*-0.2f);
                anchorRB.velocity += new Vector2(Mathf.Sin(a) * (d - anchorD)*0.05f, Mathf.Cos(a) * (d - anchorD)*0.05f);
            }

            if (player.inBalloon)
            {
                if (Input.GetKey("q"))
                {
                    //Debug.Log("retract");
                    anchored = false;
                    anchor.SetActive(false);
                }

                /*
                if (Input.GetKey("space"))
                {
                    player.inBalloon = false;
                    Debug.Log("disembark");
                }
                */
            }
        } else
        {
            if (player.inBalloon)
            {
                if (Input.GetKey("e"))
                {
                    //Debug.Log("throw");
                    anchored = true;
                    anchor.GetComponent<Transform>().position = trans.position + new Vector3(0, -3, 0);
                    anchor.SetActive(true);
                    //anchor.GetComponent<SpringJoint2D>().distance = 10;
                }
            }
        }

    }

    void BuoyantControl()
    {
        sprite.color = new Color(1, 0.3f, 0.3f);
        if (player.inBalloon)
        {
            if (Input.GetKey("up") || Input.GetKey("w"))
            {
                volume += (1 - volume) * 0.0075f;
                sprite.color = new Color(1, 0.5f, 0.5f);

            }
            if (Input.GetKey("down") || Input.GetKey("s"))
            {
                volume += (0 - volume) * 0.0075f;
                sprite.color = new Color(1, 0f, 0f);
            }
            lean *= 0.75f;
            if (Input.GetKey("right") || Input.GetKey("d"))
            {
                lean += 0.0075f; ;
            }
            if (Input.GetKey("left") || Input.GetKey("a"))
            {
                lean -= 0.0075f;
            }
        }
        bouyancy = getBouyancy(trans.position.y);

        rb.velocity += new Vector2(0, bouyancy * volume);
        rb.velocity += new Vector2(0, -gravity * weight);
        rb.velocity += new Vector2(lean, 0);
    }

    void ThrustControl()
    {
        sprite.color = new Color(1, 0.3f, 0.3f);
        rb.velocity += new Vector2(0, -0.025f);
        if (player.inBalloon)
        {
            if (Input.GetKey("up"))
            {
                rb.velocity += new Vector2(0, 0.1f);
                sprite.color = new Color(1, 0.5f, 0.5f);

            }
            if (Input.GetKey("down"))
            {
                rb.velocity += new Vector2(0, -0.1f);
                sprite.color = new Color(1, 0f, 0f);
            }
            lean *= 0.75f;
            if (Input.GetKey("right"))
            {
                lean += 0.0075f; ;
            }
            if (Input.GetKey("left"))
            {
                lean -= 0.0075f;
            }
        }
        rb.velocity += new Vector2(lean, 0);
    }


    float getBouyancy(float y)
    {
        //return atmPressure / (y + 1);
        return (40 - y) * atmPressure;
    }
}
