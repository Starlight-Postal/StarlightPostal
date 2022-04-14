using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using IngameDebugConsole;

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

    private float walkInput = 0;
    private float lookInput = 0;

    private float INTERRACT_MAX_DISTANCE = 3;

    private bool prevInBalloon = false;

    public Conversation currentConversation = null;

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

        // Register instance commands
        DebugLogConsole.AddCommandInstance("player.inballoon", "Toggle the player in and out of the balloon", "ToggleInBalloon", this);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (free)
        {
            if (inBalloon)
            {
                sprite.enabled = false;
                collider.enabled = false;
                trans.position = new Vector3(balloon.basketTrans.position.x, balloon.basketTrans.position.y, 0);
                rb.velocity = new Vector2(0, 0);
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


                rb.velocity += new Vector2(runSpeed * walkInput, 0);

                if (walkInput != 0)
                {
                    facingRight = walkInput > 0;
                    aniMode = "walk";
                }

                if (aniMode != "walk")
                {
                    if (lookInput != 0)
                    {
                        aniMode = lookInput > 0 ? "lookUp" : "lookDown";
                    }
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
        }

        if (inBalloon != prevInBalloon) {
            prevInBalloon = inBalloon;
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
                    //sfx_disembark.Play(0); //TODO: Sound should play when player gets out of char
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

    void OnMove(InputValue input)
    {
        walkInput = input.Get<float>();
    }

    void OnLook(InputValue input)
    {
        lookInput = input.Get<Vector2>().y;
    }

    void OnInterract()
    {
        // Find closest gameobject to implement the Interractable class
        var inters = GameObject.FindObjectsOfType<Interractable>();
        float closestDist = INTERRACT_MAX_DISTANCE;
        Interractable closest = null;
        for (int i = 0; i < inters.Length; i++) {
            var inter = inters[i];
            var go = inter.gameObject;
            var dist = Vector2.Distance(new Vector2(go.transform.position.x, go.transform.position.y),
                new Vector2(gameObject.transform.position.x, gameObject.transform.position.y));

            if (inter.CanPlayerInterract()) {
                if (currentConversation != null && inter is Conversation)
                {
                    if (currentConversation == (Conversation) inter && ((Conversation) inter).InterractionSearchPriority())
                    {
                        closest = inter;
                        break;
                    } else
                    {
                        if (!currentConversation.AllowConcurrentConversations())
                        {
                            continue;
                        }
                    }
                }

                if (dist <= closestDist)
                {
                    closestDist = dist;
                    closest = inter;
                }
            }
        }
        if (closest != null) {
            closest.OnPlayerInterract();
        }
    }

    void OnPlatformDrop()
    {
        if (!inChair)
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
        }
    }
    
    public void ToggleInBalloon() {
        var ps = GameObject.Find("player").GetComponent<player>();
        ps.inBalloon = !ps.inBalloon;
        Debug.Log("Toggled inBalloon state to " + ps.inBalloon);
    }

}
