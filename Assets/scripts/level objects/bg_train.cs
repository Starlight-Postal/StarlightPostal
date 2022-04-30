using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IngameDebugConsole;

public class bg_train : MonoBehaviour
{
    Transform trans;
    public float x1;
    public float x2;
    public float speed = 0.005f;
    public float vel = 0.1f;
    float x;

    public player player;
    public camera cam;

    public Transform camTrans;

    public global_data gdata;

    GameObject music;
    // Start is called before the first frame update
    void Start()
    {
        gdata = GameObject.Find("Coin Global Data").GetComponent<global_data>();
        trans = gameObject.GetComponent<Transform>();
        x = x1;

        player = GameObject.Find("player").GetComponent<player>();
        cam = GameObject.Find("Main Camera").GetComponent<camera>();
        camTrans = GameObject.Find("Main Camera").GetComponent < Transform>();
        GameObject.Find("player").GetComponent<SpriteRenderer>().enabled = false;
        player.free = false;
        cam.follow = false;
        camTrans.position = new Vector3(-10,camTrans.position.y,camTrans.position.z);

        // Register skip command
        DebugLogConsole.AddCommandInstance("train.skip", "Skips train cutscene", "SkipCutscene", this);

        music = GameObject.Find("Jukebox");
        if (!gdata.introScene)
        {
            player.free = true;
            cam.follow = true;
            x = x2;
            music.SetActive(true);
        }
        else
        {
            music.SetActive(false);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float xv = (x2 - x) * speed;
        if (xv <= vel)
        {
            x += xv;
        } else
        {
            x += vel;
        }
        trans.position = new Vector3(x,trans.position.y, trans.position.z);

        if ((x2 - x) < 0.01)
        {
            player.free = true;
            cam.follow = true;
            gdata.introScene = false;
            music.SetActive(true);
        } else
        {
            if (x > -20+6.35)
            {
                camTrans.position += new Vector3(((x+6.35f-2.25f)-camTrans.position.x)*0.02f, 0,0);
            }
        }
    }

    public void SkipCutscene() {
        x = x2;
        gdata.introScene = false;
        music.SetActive(true);
    }
}
