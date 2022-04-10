using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using IngameDebugConsole;

public class balloon : MonoBehaviour
{
    
    float lean;
    public float leanPower = 0.0075f;

    public float targetHeight;
    public float th;
    public float heightCap = 30;
    public float heightFloor = 0;
    public float buoyancy = 0.0005f;
    public float weight = 1;
    public float fillRate = 0.1f;
    float fr;
    public Vector2 airFric;

    public float windPower = 1.25f;

    public Rigidbody2D rb;
    public Transform trans;
    public SpriteRenderer sprite;
    Vector2 trackV;
    
    public Sprite[] skins;
    public int skin = 0;

    public wind wind;

    public GameObject anchorObj;
    public anchor anchor;
    public Transform anchorTrans;
    public bool anchored;
    public float anchorD = 10;
    public float anchorRange = 10;
    public Vector2 anchorStrength;
    public Vector3 anchorOrg;
    public bool landed = false;
    Vector2 anchorOA;

    public Rigidbody2D basket;
    public Transform basketTrans;
    public Collider2D basketCollider;

    public SpriteRenderer basketSprite;
    public Sprite basketTex_0;
    public Sprite basketTex_1;
    public Sprite basketTex_2;

    public player player;

    int kiLMOUSE;
    int kiRMOUSE;
    int kiE;

    public Transform[] altExZones;

    LineRenderer line;


    public bool lockEntry = false;

    global_data gdata;

    public GameObject dropCoin;

    public BonkSoundController bonk;

    private float burnVentInput = 0;
    private float leanInput = 0;

    // Start is called before the first frame update
    void Start()
    {
        gdata = GameObject.Find("Coin Global Data").GetComponent<global_data>();
        //rb = gameObject.GetComponent<Rigidbody2D>();
        //trans = gameObject.GetComponent<Transform>();
        sprite = gameObject.GetComponent<SpriteRenderer>();

        lean = 0;
        fr = fillRate;

        player = GameObject.Find("player").GetComponent<player>();

        //wind = GameObject.Find("wind").GetComponent<wind>();
        trackV = new Vector2(0, 0);

        bonk = gameObject.GetComponent<BonkSoundController>();

        anchorObj = GameObject.Find("anchor");
        anchor = anchorObj.GetComponent<anchor>();
        anchorObj.SetActive(anchored);
        anchorTrans = anchorObj.GetComponent<Transform>();

        anchorOA = new Vector2(anchorOrg.magnitude, Mathf.Atan2(anchorOrg.y, anchorOrg.x));
        Debug.Log("OA " + anchorOA);

        line = gameObject.GetComponent<LineRenderer>();
        line.enabled=false;

        basket = GameObject.Find("Basket").GetComponent<Rigidbody2D>();
        basketCollider = GameObject.Find("Basket").GetComponent<Collider2D>();
        basketTrans = GameObject.Find("Basket").GetComponent<Transform>();

        //Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), anchor.GetComponent<Collider2D>(), true);
        //Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), basketCollider, true); // This was causing ArgumentNullException, Parameter name: collider1

        basket.centerOfMass = new Vector2(0, -1f);

        setSkin(skin);

        Debug.Log("dingus");
        // Register instanced console commands
        DebugLogConsole.AddCommandInstance("balloon.anchor", "Toggles the balloon anchor", "ToggleAnchor", this);
        DebugLogConsole.AddCommandInstance("balloon.heightcap", "Gets the balloon height cap", "GetHeightCap", this);
        DebugLogConsole.AddCommandInstance("balloon.heightcap", "Sets the balloon height cap", "SetHeightCap", this);
        DebugLogConsole.AddCommandInstance("balloon.heightfloor", "Gets the balloon height floor", "GetHeightFloor", this);
        DebugLogConsole.AddCommandInstance("balloon.heightfloor", "Sets the balloon height floor", "SetHeightFloor", this);
        DebugLogConsole.AddCommandInstance("balloon.leanpower", "Gets the balloon lean power", "GetLeanPower", this);
        DebugLogConsole.AddCommandInstance("balloon.leanpower", "Sets the balloon lean power", "SetLeanPower", this);
        DebugLogConsole.AddCommandInstance("balloon.fillrate", "Gets the balloon fill rate", "GetFillRate", this);
        DebugLogConsole.AddCommandInstance("balloon.fillrate", "Sets the balloon fill rate", "SetFillRate", this);
        DebugLogConsole.AddCommandInstance("balloon.windpower", "Gets the balloon wind power", "GetWindPower", this);
        DebugLogConsole.AddCommandInstance("balloon.windpower", "Sets the balloon wind power", "SetWindPower", this);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        trackV = rb.velocity;
        rb.velocity *= airFric;

        TargetControl();

        rb.velocity += wind.getWind(trans.position.x, trans.position.y)*windPower;

        anchorOrg = new Vector3(Mathf.Cos(anchorOA.y + (basketTrans.eulerAngles.z * (Mathf.PI / 180f))) * anchorOA.x, Mathf.Sin(anchorOA.y + (basketTrans.eulerAngles.z * (Mathf.PI / 180f))) * anchorOA.x, 0);

        if (anchored)
        {
            
            Rigidbody2D anchorRB = anchor.GetComponent<Rigidbody2D>();
            float d = Vector3.Distance(anchorTrans.position, trans.position+anchorOrg);
            if (d > anchorD)
            {
                float a = Mathf.Atan2(trans.position.x + anchorOrg.x - anchorTrans.position.x, trans.position.y + anchorOrg.y - anchorTrans.position.y);
                rb.velocity += new Vector2(Mathf.Sin(a) * (d - anchorD) * -anchorStrength.x, Mathf.Cos(a) * (d - anchorD) * -anchorStrength.x);
                if (!anchor.stuck) {
                    anchorRB.velocity += new Vector2(Mathf.Sin(a) * (d - anchorD) * anchorStrength.y, Mathf.Cos(a) * (d - anchorD) * anchorStrength.y);
                }
            }

            if (player.inBalloon)
            {
                /*if (Input.GetKey("q"))
                {
                    anchorD *= 0.99f;
                }*/
            } else
            {
                anchorD += (5f - anchorD) * 0.005f;
                //volume *= 0.999f;
            }

            line.enabled = true;
            line.SetPosition(0, basketTrans.position + anchorOrg+new Vector3(0,0,0.5f));
            line.SetPosition(1, anchorTrans.position+new Vector3(0,0.25f,0.5f));

            if (anchor.landed)
            {
                if (basketTrans.position.y > anchor.targetTrans.position.y && basketTrans.position.y - anchor.targetTrans.position.y < 1.5f)
                {
                    landed = true;
                } else
                {
                    landed = false;
                }
                
                th += ((anchor.targetTrans.position.y+4f) - th) * 0.0025f;
                //anchorD += (1 - anchorD) * 0.01f;
            } else
            {
                landed = false;
            }
        }
        else
        {
            line.enabled = false;
            landed = false;
        }

        if (player.inBalloon)
        {
            basketSprite.sprite = basketTex_1;
        } else
        {
            basketSprite.sprite = basketTex_0;
        }


    }


    void TargetControl()
    {
        sprite.color = new Color(1,0.9f,0.9f);
        if (player.inBalloon)
        {

            if (burnVentInput != 0)
            {
                sprite.color = new Color(1, 0.9f + (burnVentInput / 10), 0.9f + (burnVentInput / 10));
                fr += ((fillRate * 3 - fr) * 0.0025f) * Mathf.Abs(burnVentInput);
                th += burnVentInput > 0 ? fr : fr * -1;
            } else
            {
                fr += (fillRate - fr) * 0.1f;
            }

            lean *= 0.75f;
            if (leanInput != 0)
            {
                lean += leanPower * leanInput;
            }

        }

        if (th < heightFloor)
        {
            th += (heightFloor - th) * 0.1f;
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

    void OnBurnVent(InputValue value)
    {
        burnVentInput = value.Get<float>();
    }

    void OnLean(InputValue value)
    {
        leanInput = value.Get<float>();
    }

    void OnAnchor()
    {
        if (anchored)
        {
            //Debug.Log("retract");
            anchored = false;
            anchorObj.SetActive(false);
            anchor.stuck = false;
            anchor.landed = false;
        } else
        {
            //Debug.Log("throw");
            anchored = true;
            anchorD = anchorRange;
            anchorObj.GetComponent<Transform>().position = basketTrans.position + anchorOrg;
            anchorObj.SetActive(true);
            anchor.stuck = false;
            anchor.landed = false;
            //anchor.GetComponent<SpringJoint2D>().distance = 10;
        }
    }

    void OnLeaveBalloon()
    {
        if (landed)
        {
            player.inBalloon = false;
        }
    }

    public void centerHit()
    {
        float d = (trackV-rb.velocity).magnitude;
        //Debug.Log(d);
        bonk.Bonk(d);
        if (d >= 4)
        {
            //dropCoins((int)Mathf.Floor(d) - 3);
            dropCoins((int)Mathf.Floor(Mathf.Pow((d - 4) / 15f, 0.5f) * 10));
        }
    }

    void dropCoins(int n)
    {
        //Debug.Log(n);
        if (n > gdata.coins)
        {
            n = gdata.coins;
        }

        bonk.CoinsDropped(n);

        gdata.coins -= n;
        for(int i = 0;i < n;i++)
        {
            Instantiate(dropCoin, basketTrans.position, Quaternion.identity);
        }
    }

    void setSkin(int id)
    {
        skin = id % skins.Length;
        sprite.sprite = skins[skin];
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

    [ConsoleMethod("balloon.skin.id","change the balloon skin")]
    public static void SetBalloonSkin(int id)
    {
        GameObject.Find("balloon").GetComponent<balloon>().setSkin(id);
        Debug.Log("Changed balloon skin id to " + id);

    }

    [ConsoleMethod("balloon.skin", "change the balloon skin")]
    public static void ChangeBalloonSkin(string name)
    {
        var spriteRender = GameObject.Find("balloon").GetComponent<SpriteRenderer>();
        var sprite = Resources.Load<Sprite>("textures/Balloons/" + name);
        if (sprite != null)
        {
            spriteRender.sprite = sprite;
            Debug.Log("Changed balloon skin to " + name);
        }
        else
        {
            Debug.LogError("Could not load balloon skin '" + name + "'. Does it exist?");
        }
    }

    [ConsoleMethod("balloon.skin", "get the current balloon skin")]
    public static string FetchBalloonSkin() {
        var spriteRender = GameObject.Find("balloon").GetComponent<SpriteRenderer>();
        return spriteRender.sprite.name;
    }

    [ConsoleMethod("balloon.skins", "get a list of valid balloon skins")]
    public static void FetchValidBalloonSkins() {
        var sprites = Resources.LoadAll<Sprite>("textures/Balloons/") as Sprite[];
        string output = "Valid balloon skins:";
        bool first = true;
        foreach (var sprite in sprites) {
            if (!first) {
                output += ",";
            }
            output += " " + sprite.name;
            first = false;
        }
        Debug.Log(output);
    }

    public void ToggleAnchor() {
        anchored = !anchored;
        Debug.Log("Toggled anchor to " + anchored);
    }

    public void GetHeightCap() {
        Debug.Log("Balloon height cap: " + heightCap);
    }

    public void SetHeightCap(float newHeightCap) {
        heightCap = newHeightCap;
        GetHeightCap();
    }

    public void GetHeightFloor() {
        Debug.Log("Balloon height floor: " + heightFloor);
    }

    public void SetHeightFloor(float newHeightFloor) {
        heightFloor = newHeightFloor;
        GetHeightFloor();
    }

    public void GetLeanPower() {
        Debug.Log("Balloon lean power: " + leanPower);
    }

    public void SetLeanPower(float newLeanPower) {
        leanPower = newLeanPower;
        GetLeanPower();
    }

    public void GetFillRate() {
        Debug.Log("Balloon fill rate: " + fillRate);
    }

    public void SetFillRate(float newFillRate) {
        fillRate = newFillRate;
        GetFillRate();
    }

    public void GetWindPower() {
        Debug.Log("Balloon wind power: " + windPower);
    }

    public void SetWindPower(float newWindPower) {
        windPower = newWindPower;
        GetWindPower();
    }
    
}
