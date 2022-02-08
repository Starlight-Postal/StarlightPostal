using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wind_cyclone : wind_structure
{
    public float power = 1;
    public float blur = 0.5f;

    public Transform trans;

    void Start()
    {
        trans = gameObject.GetComponent<Transform>();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    public override Vector2 getWind(float x, float y)
    {
        Vector2 wind = new Vector2(0, 0);

        float dist = Mathf.Sqrt(Mathf.Pow((trans.position.x-x)/(trans.localScale.x*0.5f),2)+Mathf.Pow((trans.position.y-y)/(trans.localScale.y*0.5f),2));
        if (dist < 1)
        {
            float a = Mathf.Atan2(((trans.position.y - y) / (trans.localScale.y * 0.5f)) , ((trans.position.x - x) / (trans.localScale.x * 0.5f)));
            a += (Mathf.PI / 2f);

            if (blur > 0)
            {
                dist = (dist / blur) + (1 - (1 / blur));
                if (dist > 1)
                {
                    dist = 1;
                }
                if (dist < 0)
                {
                    dist = 0;
                }
            }
            wind = new Vector2(Mathf.Cos(a) * power*(1-dist), Mathf.Sin(a) * power*(1-dist));
        }
        return wind;
    }
}