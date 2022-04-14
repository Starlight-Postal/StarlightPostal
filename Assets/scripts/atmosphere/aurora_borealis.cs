using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aurora_borealis : MonoBehaviour
{
    public int TICK = 0;
    public Vector2 scale;
    public float speed;
    public float z;
    public int rate = 15;
    public float window = 0.025f;
    public float alpha = 0.25f;
    public float size = 1;
    public int decay = 60;
    public float aniSpeed = 0.1f;

    public GameObject auraObj;
    //public float mapScale = 1;
    Camera cam;
    Transform trans;
    // Start is called before the first frame update
    void Start()
    {
        TICK = 0;
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        trans = gameObject.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TICK++;

        for (int i = 0; i < rate; i++)
        {
            float x = Random.Range(-36, 36);
            float y = Random.Range(-20, 20);
            float s = getAurora(x, y) * 10;
            if (Random.Range(0.0f, 1.0f) < s)
            {
                if (s > 10)
                {
                    s = 10;
                }
                spawnAura(x, y, aniSpeed, decay, s*size, new Color(0.6f * s, (1.5f+(y*0.05f)) * s, (2-(y*0.05f)) * s, s * 0.01f*alpha),auraObj);
            }
        }

        //mapScale = cam.orthographicSize / 20f;
        trans.localScale = new Vector3(cam.orthographicSize * 0.05f, cam.orthographicSize * 0.05f, 1);
    }

    public float getAurora(float ix, float iy)
    {
        float x=ix* scale.x;
        float y=iy* scale.y;
        float TIME = TICK * speed;
        float v = Mathf.PerlinNoise(((x * 0.5f) + TIME) * 0.005f, (y + (TIME * 0.5f)) * 0.005f);
        v += Mathf.PerlinNoise(((x * 0.25f) + TIME) * 0.005f, (y - TIME) * 0.005f);
        v *= (Mathf.Cos((iy * window) * Mathf.PI));// * 0.5f;
        if (v > 0.5f)
        {
            v = 1 - v;
        }
        v *= 2;
        v = Mathf.Pow(v, 10);
        return v;
    }

    void spawnAura(float x, float y, float speed,int lifespan,float size,Color c,GameObject aobj)
    {
        GameObject ao = Instantiate(aobj, gameObject.transform.position + new Vector3(x*trans.localScale.x, y*trans.localScale.y, z), Quaternion.identity); //gameObject.transform.position + new Vector3(x, y, z)
        ao.transform.parent = gameObject.transform;
        //ao.transform.position = new Vector3(x, y, z);
        aura a = ao.GetComponent<aura>();
        a.speed = speed;
        a.period = Random.Range(0, Mathf.PI * 2);
        a.lifetime = lifespan;
        a.size = size;
        a.color = c;

        
    }
}
