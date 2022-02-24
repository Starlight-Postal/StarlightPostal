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
    public Transform balloonTrans;
    public Transform anchorTrans;
    public balloon balloon;

    bool swap = false;

    int kiDOWN = 0;
    //public EdgeCollider2D targetPlatform;


    //string aniMode = "idle";
    //public float aniSpeed = 0.25f;
    //public Sprite[] aniIdle;
    //int aniFrame = 0;

    public List<EdgeCollider2D> platformQueueu;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        collider = gameObject.GetComponent<Collider2D>();
        trans = gameObject.GetComponent<Transform>();
        //balloonTrans = GameObject.Find("balloon").GetComponent<Transform>();
        balloon = GameObject.Find("balloon").GetComponent<balloon>();
        anchorTrans = GameObject.Find("anchor").GetComponent<Transform>();

        kiDOWN = 0;

        //aniMode = "idle";
        //aniFrame = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UIUpdate();
        if (inBalloon)
        {
            sprite.enabled = false;
            collider.enabled = false;
            trans.position = balloonTrans.position+new Vector3(0,-2.5f,0);
            rb.velocity = new Vector2(0, 0);
            if (Input.GetKey("space"))
            {
                if (swap)
                {
                    //if (balloon.anchored&&balloon.anchorD<1f)
                    if(balloon.anchored)
                    {
                        inBalloon = false;
                        //Debug.Log("disembark");
                        swap = false;
                    }
                }
            } else
            {
                swap = true;
            }
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

            if (kiDOWN == 1)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(0, -1), 0.6f,LayerMask.GetMask("Default"));
                if (hit!=null) {
                    Collider2D platform = hit.collider;
                    //EdgeCollider2D platform = targetPlatform;
                    //Debug.Log(platform.GetType());
                    if (platform.GetType() == typeof(EdgeCollider2D))
                    {
                        if (platform.gameObject.GetComponent<PlatformEffector2D>() != null)
                        {
                            Physics2D.IgnoreCollision(collider, platform, true);
                            platformQueueu.Add((EdgeCollider2D)platform);
                            //Debug.Log("through");
                        }
                    }
                    
                } else
                {
                    //Debug.Log("none");
                }
            } else
            {
                for(int i = 0;i < platformQueueu.Count;i++)
                {
                    EdgeCollider2D platform = platformQueueu[i];
                    Transform platShape = platform.gameObject.GetComponent<Transform>();
                    Vector2 pA = (new Vector2(platShape.position.x, platShape.position.y) + (platform.points[0] * new Vector2(platShape.lossyScale.x, platShape.lossyScale.y)));
                    Vector2 pB = (new Vector2(platShape.position.x, platShape.position.y) + (platform.points[1] * new Vector2(platShape.lossyScale.x, platShape.lossyScale.y)));
                    if (Mathf.Abs(trans.position.x - (pA.x + pB.x) / 2f) > Mathf.Abs(pA.x - pB.x) / 2f)
                    {
                        Physics2D.IgnoreCollision(collider, platform, false);
                        //Debug.Log("out");
                        platformQueueu.RemoveAt(i);
                        i--;
                    }
                    else
                    {
                        float m = (pA.y - pB.y) / (pA.x - pB.x);
                        float b = pA.y - (m * pA.x);
                        if (trans.position.y - ((trans.position.x * m) + b) < -0.5f)
                        {
                            Physics2D.IgnoreCollision(collider, platform, false);
                            //Debug.Log("under");
                            platformQueueu.RemoveAt(i);
                            i--;
                        }
                    }
                }
                
            }

            //Debug.Log("balloon range!");
            if (Input.GetKey("space"))
            {
                if (swap)
                {
                    if (Vector3.Distance(trans.position, balloonTrans.position) < 3.5f || Vector3.Distance(trans.position, anchorTrans.position) < 1f)
                    {
                        //Debug.Log("embark");
                        inBalloon = true;
                        swap = false;
                    }
                }
            } else
            {
                swap = true;
            }
            
        }
        //Debug.Log(swap);

        //aniFrame = (int)Mathf.Floor(aniFrame + (aniSpeed)) % aniIdle.Length;
        //sprite = aniIdle[aniFrame];
        
    }

    void UIUpdate()
    {
        if (Input.GetKey("down") || Input.GetKey("s"))
        {
            if (kiDOWN == 0)
            {
                kiDOWN = 1;
            } else
            {
                kiDOWN = 2;
            }
        } else
        {
            kiDOWN = 0;
        }
    }
}