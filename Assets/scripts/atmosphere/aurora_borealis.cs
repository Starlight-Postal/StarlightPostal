using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aurora_borealis : MonoBehaviour
{
    public int TICK = 0;
    public Vector2 scale;
    public float speed;
    public float z;

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

        for (int i = 0; i < 10; i++)
        {
            float x = Random.Range(-20, 20);
            float y = Random.Range(-20, 20);
            float s = getAurora(x, y) * 10;
            Debug.Log(s);
            if (Random.Range(0.0f, 1.0f) < s)
            {
                spawnAura(x, y, 0.1f, 30, 2, auraObj);
            }
        }
    }

    public float getAurora(float ix, float iy)
    {
        float x=ix* scale.x;
        float y=iy* scale.y;
        float TIME = TICK * speed;
        float v = Mathf.PerlinNoise(((x * 0.5f) + TIME) * 0.005f, (y + (TIME * 0.5f)) * 0.005f);
        v += Mathf.PerlinNoise(((x * 0.25f) + TIME) * 0.005f, (y - TIME) * 0.005f);
        v *= (Mathf.Sin(y * (Mathf.PI / 500)) + 1) * 0.5f;
        if (v > 1)
        {
            v = 2 - v;
        }
        v = Mathf.Pow(v, 10);
        return v;
    }

    void spawnAura(float x, float y, float speed,int lifespan,float size,GameObject aobj)
    {
        GameObject ao = Instantiate(aobj, new Vector3(x, y, z), Quaternion.identity);
        aura a = ao.GetComponent<aura>();
        a.speed = speed;
        a.lifetime = lifespan;
        a.size = size;

        /*flakes.Add(flakeObj.transform);
        flakeLife.Add(Random.Range(0f, 1f));
        flakeWeight.Add(Random.Range(0.9f, 1.1f));
        flakeV.Add(new Vector2(0, 0));*/
    }
}
