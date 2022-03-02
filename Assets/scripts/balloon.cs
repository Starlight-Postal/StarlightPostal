using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balloon : MonoBehaviour
{
    
    float lean;
    public float leanPower = 0.0075f;
    /*float bouyancy;
    public bool capped = true;
    public float volume = 1;
    public float weight = 1;
    public float airFric = 0.975f;
    public float gravity = 0.1f;
    public float atmPressure = 1;
    public float leanPower = 0.0075f;
    public float fillRate = 0.025f;
    public float volCap = 1;*/

    public float targetHeight;
    public float th;
    public float heightCap = 30;
    public float buoyancy = 0.0005f;
    public float weight = 1;
    public float fillRate = 0.1f;
    public Vector2 airFric;

    public float windPower = 1.25f;

    public Rigidbody2D rb;
    public Transform trans;
    public SpriteRenderer sprite;


    public wind wind;
    public GameObject anchor;
    public bool anchored;
    public float anchorD = 10;
    public float anchorRange = 10;

    public Rigidbody2D basket;
    public Transform basketTrans;
    public Collider2D basketCollider;

    public player player;

    int kiLMOUSE;
    int kiRMOUSE;

    public Transform[] altExZones;

    // Start is called before the first frame update
    void Start()
    {
        //rb = gameObject.GetComponent<Rigidbody2D>();
        //trans = gameObject.GetComponent<Transform>();
        sprite = gameObject.GetComponent<SpriteRenderer>();

        lean = 0;

        player = GameObject.Find("player").GetComponent<player>();

        //wind = GameObject.Find("wind").GetComponent<wind>();


        anchor = GameObject.Find("anchor");
        anchor.SetActive(anchored);
        //anchored = true;

        basket = GameObject.Find("Basket").GetComponent<Rigidbody2D>();
        basketCollider = GameObject.Find("Basket").GetComponent<Collider2D>();
        basketTrans = GameObject.Find("Basket").GetComponent<Transform>();

        //Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), anchor.GetComponent<Collider2D>(), true);
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), basketCollider, true);

        basket.centerOfMass = new Vector2(0, -1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UIUpdate();
        rb.velocity *= airFric;

        TargetControl();

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
            } else
            {
                anchorD += (2.5f - anchorD) * 0.01f;
                //volume *= 0.999f;
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


    void TargetControl()
    {
        sprite.color = new Color(1,0.9f,0.9f);
        if (player.inBalloon)
        {
            if (Input.GetKey("up") || Input.GetKey("w"))
            {
                th += fillRate;
                sprite.color = new Color(1,1,1);
            }
            if (Input.GetKey("down") || Input.GetKey("s"))
            {
                th -= fillRate;
                sprite.color = new Color(1, 0.8f,0.8f);
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

        if (th < 0)
        {
            th += (0 - th) * 0.1f;
        }
        if (th > heightCap)
        {
            bool ex = false;
            for(int i = 0;i < altExZones.Length;i++)
            {
                if(Mathf.Abs(trans.position.x-altExZones[i].position.x)<=altExZones[i].localScale.x/2f&& Mathf.Abs(trans.position.y - altExZones[i].position.y) <= altExZones[i].localScale.y / 2f)
                {
                    ex = true;
                    break;
                }
            }
            if (!ex)
            {
                th += (heightCap - th) * 0.1f;
            }
        }

        targetHeight = th / weight;

        float hd = targetHeight - trans.position.y;
        rb.velocity += new Vector2(lean,hd * buoyancy);

        //Debug.Log(targetHeight);
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

    float toAngle(float a,float b,float amt)
    {
        float xa = Mathf.Sin(a);
        float xb = Mathf.Sin(b);
        float ya = Mathf.Cos(a);
        float yb = Mathf.Cos(b);
        float x = xa * (1 - amt) + xb * amt;
        float y = ya * (1 - amt) + yb * amt;
        return Mathf.Atan2(y,x);
    }
}
