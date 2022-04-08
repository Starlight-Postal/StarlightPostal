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

    public GameObject auraObj;
    // Start is called before the first frame update
    void Start()
    {
        TICK = 0;
        
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
                spawnAura(x, y, 0.1f, 60, s, new Color(0.6f * s, (1.5f+(y*0.05f)) * s, (2-(y*0.05f)) * s, s * 0.0025f),auraObj);
            }
        }
        /*for(int x = -20;x < 20;x+=1)
        {
            for(int y = -20;y < 20;y+=1)
            {
                float s = getAurora(x, y);
                if (s > 0.5f)
                {
                    spawnAura(x, y, 0.1f, 1, 1, auraObj);
                }
                //Debug.Log(s);
            }
        }*/
    }

    public float getAurora(float ix, float iy)
    {
        float x=ix* scale.x;
        float y=iy* scale.y;
        float TIME = TICK * speed;
        float v = Mathf.PerlinNoise(((x * 0.5f) + TIME) * 0.005f, (y + (TIME * 0.5f)) * 0.005f);
        v += Mathf.PerlinNoise(((x * 0.25f) + TIME) * 0.005f, (y - TIME) * 0.005f);
        //float v = 1;
        v *= (Mathf.Cos((iy / 40f) * Mathf.PI) + 0);// * 0.5f;
        if (v > 0.5f)
        {
            v = 1 - v;
        }
        v *= 2;
        v = Mathf.Pow(v, 10);
        //v -= 0.5f;
        return v;
    }

    void spawnAura(float x, float y, float speed,int lifespan,float size,Color c,GameObject aobj)
    {
        GameObject ao = Instantiate(aobj, new Vector3(x, y, z), Quaternion.identity);
        aura a = ao.GetComponent<aura>();
        a.speed = speed;
        a.lifetime = lifespan;
        a.size = size;
        a.color = c;
        /*flakes.Add(flakeObj.transform);
        flakeLife.Add(Random.Range(0f, 1f));
        flakeWeight.Add(Random.Range(0.9f, 1.1f));
        flakeV.Add(new Vector2(0, 0));*/
    }
}
