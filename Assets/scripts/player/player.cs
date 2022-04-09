using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public bool free = true;

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

    public int kiDOWN = 0;
    public int kiSPACE = 0;
    //public EdgeCollider2D targetPlatform;


    public string aniMode = "idle";
    public float aniSpeed = 0.25f;
    public Sprite[] aniIdle;
    public float aniIdleSpeed = 0.25f;
    public Sprite[] aniWait;
    public float aniWaitSpeed = 0.25f;
    public Sprite[] aniWalk;
    public float aniWalkSpeed = 1f;
    public Sprite[] aniLookUp;
    public Sprite[] aniLookDown;
    public float aniFrame = 0;

    public bool facingRight;
    public float camHeight = 0;
    public float camRange = 1f;
    public float camCenter = 0.5f;

    public bool inChair = false;
    public Transform chair = null;

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

        aniMode = "idle";
        aniFrame = 0;
        aniIdle = new Sprite[8];

        for (int i = 0; i < 8; i++)
        {
            aniIdle[i] = Resources.Load<Sprite>("textures/Player/player_idle/player_idle_" + i);
        }
        aniWait = new Sprite[20];
        for (int i = 0; i < 20; i++)
        {
            aniWait[i] = Resources.Load<Sprite>("textures/Player/player_wait/player_wait_" + i);
        }
        aniWalk = new Sprite[18];
        for (int i = 0; i < 18; i++)
        {
            aniWalk[i] = Resources.Load<Sprite>("textures/Player/player_walk/player_walk_" + i);
        }
        aniLookUp = new Sprite[1];
        aniLookUp[0] = Resources.Load<Sprite>("textures/Player/player_up");

        aniLookDown = new Sprite[1];
        aniLookDown[0] = Resources.Load<Sprite>("textures/Player/player_down");
        camHeight = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (free)
        {
            UIUpdate();
            if (inBalloon)
            {
                sprite.enabled = false;
                collider.enabled = false;
                trans.position = new Vector3(balloon.basketTrans.position.x, balloon.basketTrans.position.y, 0);
                rb.velocity = new Vector2(0, 0);
                if (kiSPACE == 1)
                {
                    if (swap)
                    {
                        if (balloon.landed)
                        {
                            inBalloon = false;
                            //Debug.Log("disembark");
                            swap = false;
                        }
                    }
                }
                else
                {
                    swap = true;
                }
            }
            else
            {
                if (aniMode != "wait")
                {
                    aniMode = "idle";
                }
                sprite.enabled = true;
                collider.enabled = true;
                rb.velocity = new Vector2(rb.velocity.x * 0.8f, rb.velocity.y); ;
                rb.velocity += new Vector2(0, -gravity);
                if (Input.GetKey("right") || Input.GetKey("d"))
                {
                    rb.velocity += new Vector2(runSpeed, 0);
                    facingRight = true;
                    aniMode = "walk";
                }
                if (Input.GetKey("left") || Input.GetKey("a"))
                {
                    rb.velocity += new Vector2(-runSpeed, 0);
                    facingRight = false;
                    aniMode = "walk";
                }
                if (Input.GetKey("up") || Input.GetKey("w"))
                {
                    if (aniMode != "walk")
                    {
                        aniMode = "lookUp";
                    }
                    camHeight += ((camCenter + camRange) - camHeight) * 0.25f;
                }
                else if (Input.GetKey("down") || Input.GetKey("s"))
                {
                    if (aniMode != "walk")
                    {
                        aniMode = "lookDown";
                    }
                    camHeight += ((camCenter - camRange) - camHeight) * 0.25f;
                }
                else
                {
                    camHeight += ((camCenter) - camHeight) * 0.25f;
                }

                if (aniMode == "idle")
                {
                    if (aniFrame == 0)
                    {
                        if (Random.Range(0.0f, 1.0f) < 0.1f)
                        {
                            aniMode = "wait";
                            //Debug.Log("?");
                        }

                    }
                }
                else
                {
                    if (aniMode == "wait")
                    {
                        if (aniFrame == 0)
                        {
                            aniMode = "idle";
                            //Debug.Log("!");
                        }
                    }
                }

                if (kiDOWN == 1&&!inChair)
                {
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(0, -1), 0.6f, LayerMask.GetMask("Default"));
                    if (hit != null)
                    {
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

                    }
                    else
                    {
                        //Debug.Log("none");
                    }
                }
                else
                {
                    for (int i = 0; i < platformQueueu.Count; i++)
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
                if (kiSPACE == 1)
                {
                    if (swap)
                    {
                        if (!balloon.lockEntry && Vector3.Distance(trans.position, balloonTrans.position + new Vector3(0, 0, (trans.position.z - balloonTrans.position.z))) < 3.5f || Vector3.Distance(trans.position, anchorTrans.position + new Vector3(0, 0, (trans.position.z - anchorTrans.position.z))) < 1f)

                        {
                            if (Vector3.Distance(trans.position, balloonTrans.position + new Vector3(0, 0, (trans.position.z - balloonTrans.position.z))) < 3.5f || Vector3.Distance(trans.position, anchorTrans.position + new Vector3(0, 0, (trans.position.z - anchorTrans.position.z))) < 1f)
                            {
                                //Debug.Log("embark");
                                inBalloon = true;
                                swap = false;
                            }
                        }
                    }

                }
                else
                {
                    swap = true;
                }
            }
        }

        

        if (inBalloon)
        {
            inChair = false;
            chair = null;
        } else
        {
            if (inChair)
            {
                aniMode = "idle";
                rb.velocity *= 0;
                collider.enabled = false;
                trans.position = new Vector3(chair.position.x, chair.position.y-0.5f, trans.position.z);
                if (kiSPACE == 1 || kiDOWN == 1)
                {
                    inChair = false;
                    chair = null;
                    Debug.Log("chair out");
                }
            } else
            {
                collider.enabled = true;
            }
        }

        
        Sprite[] ani = aniIdle;
        switch (aniMode)
        {
            case "idle":
                ani = aniIdle;
                aniSpeed = aniIdleSpeed;
                break;
            case "wait":
                ani = aniWait;
                aniSpeed = aniWaitSpeed;
                break;
            case "walk":
                ani = aniWalk;
                aniSpeed = aniWalkSpeed;
                break;
            case "lookUp":
                ani = aniLookUp;
                aniSpeed = 1;
                break;
            case "lookDown":
                ani = aniLookDown;
                aniSpeed = 1;
                break;
            default:
                break;
        }

        aniFrame = aniFrame % ani.Length;
        sprite.sprite = ani[(int)aniFrame];
        aniFrame = (aniFrame + aniSpeed) % ani.Length;
        //sprite.flipX = !facingRight;
        if (facingRight)
        {
            trans.localScale += new Vector3((0.25f - trans.localScale.x) * 0.2f, 0, 0);
        } else
        {
            trans.localScale += new Vector3((-0.25f - trans.localScale.x) * 0.2f, 0, 0);
        }
    }

    void UIUpdate()
    {
        if (Input.GetKey("down") || Input.GetKey("s"))
        {
            if (kiDOWN == 0)
            {
                kiDOWN = 1;
            }
            else
            {
                kiDOWN = 2;
            }
        }
        else
        {
            kiDOWN = 0;
        }
        if (Input.GetKey("space"))
        {
            if (kiSPACE == 0)
            {
                kiSPACE = 1;
            }
            else
            {
                kiSPACE = 2;
            }
        }
        else
        {
            kiSPACE = 0;
        }
    }
}