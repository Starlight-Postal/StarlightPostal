using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balloon : MonoBehaviour
{
    float bouyancy;
    float lean;
    public bool capped = true;
    public float volume = 1;
    public float weight = 1;
    public float airFric = 0.975f;
    public float gravity = 0.1f;
    public float atmPressure = 1;
    public float leanPower = 0.0075f;
    public float fillRate = 0.025f;
    public float volCap = 1;

    public float windPower = 1.25f;

    public Rigidbody2D rb;
    public Transform trans;
    public SpriteRenderer sprite;


    public wind wind;
    public GameObject anchor;
    public bool anchored;
    public float anchorD = 10;
    public float anchorRange = 10;


    public player player;

    int kiLMOUSE;
    int kiRMOUSE;

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

        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), anchor.GetComponent<Collider2D>(), true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UIUpdate();
        rb.velocity *= airFric;

        BuoyantControl(capped);
        //ThrustControl();

        rb.velocity += wind.getWind(trans.position.x, trans.position.y)*windPower;


        if (anchored)
        {
            Transform aTrans = anchor.GetComponent<Transform>();
            Rigidbody2D anchorRB = anchor.GetComponent<Rigidbody2D>();
            float d = Vector3.Distance(aTrans.position, trans.position);
            if (d > anchorD)
            {
                float a = Mathf.Atan2(trans.position.x - aTrans.position.x, trans.position.y - aTrans.position.y);
                rb.velocity += new Vector2(Mathf.Sin(a) * (d - anchorD) * -0.2f, Mathf.Cos(a) * (d - anchorD) * -0.2f);
                anchorRB.velocity += new Vector2(Mathf.Sin(a) * (d - anchorD) * 0.05f, Mathf.Cos(a) * (d - anchorD) * 0.05f);
            }

            if (player.inBalloon)
            {
                if (kiRMOUSE>0)
                {
                    anchorD *= 0.99f;
                }
                if (kiLMOUSE==1)
                {
                    //Debug.Log("retract");
                    anchored = false;
                    anchor.SetActive(false);
                }
            }
        }
        else
        {
            if (player.inBalloon)
            {
                if (kiLMOUSE==1)
                {
                    //Debug.Log("throw");
                    anchored = true;
                    anchorD = anchorRange;
                    anchor.GetComponent<Transform>().position = trans.position + new Vector3(0, -3, 0);
                    anchor.SetActive(true);
                    //anchor.GetComponent<SpringJoint2D>().distance = 10;
                }
            }
        }

    }

    void BuoyantControl(bool capped)
    {
        sprite.color = new Color(1, 0.3f, 0.3f);
        if (player.inBalloon)
        {
            if (Input.GetKey("up") || Input.GetKey("w"))
            {
                volume += fillRate;
                sprite.color = new Color(1, 0.5f, 0.5f);

            }
            if (Input.GetKey("down") || Input.GetKey("s"))
            {
                volume -= fillRate;
                sprite.color = new Color(1, 0f, 0f);
            }
            if (capped)
            {
                if (volume > volCap)
                {
                    volume += (volCap - volume) * 0.1f;
                }
                if (volume < 0)
                {
                    volume *= 0.9f;
                }
            }
            lean *= 0.75f;
            if (Input.GetKey("right") || Input.GetKey("d"))
            {
                lean += leanPower;
            }
            if (Input.GetKey("left") || Input.GetKey("a"))
            {
                lean -= leanPower;
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
                lean += leanPower; ;
            }
            if (Input.GetKey("left"))
            {
                lean -= leanPower;
            }
        }
        rb.velocity += new Vector2(lean, 0);
    }


    float getBouyancy(float y)
    {
        //return atmPressure / (y + 1);
        return (40 - y) * atmPressure;
    }
    void UIUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            if (kiLMOUSE == 0)
            {
                kiLMOUSE = 1;
            }
            else
            {
                kiLMOUSE = 2;
            }
        }
        else
        {
            kiLMOUSE = 0;
        }
        if (Input.GetMouseButton(1))
        {
            if (kiRMOUSE == 0)
            {
                kiRMOUSE = 1;
            }
            else
            {
                kiRMOUSE = 2;
            }
        }
        else
        {
            kiRMOUSE = 0;
        }
    }
}
