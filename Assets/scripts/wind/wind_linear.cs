using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wind_linear : wind_structure
{
    public float angle = 0;
    public float power = 0;
    public float blur = 0.5f;

    public Transform trans;

    void Start()
    {
        trans = gameObject.GetComponent<Transform>();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    public override Vector2 getWind(float x, float y) {
        Vector2 wind = new Vector2(0, 0);
        float distX = Mathf.Abs(x - trans.position.x) / (trans.localScale.x * 0.5f);
        float distY = Mathf.Abs(y - trans.position.y) / (trans.localScale.y * 0.5f);
        if (distX<=1&&distY<=1) {
            float dist = 0;
            if (blur > 0)
            {

                dist = distX;
                if (distY > dist)
                {
                    dist = distY;
                }
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
            wind = new Vector2(Mathf.Cos(angle*(Mathf.PI/180f)) * power * (1 - dist), -Mathf.Sin(angle * (Mathf.PI / 180f)) * power * (1 - dist));
        }
        return wind;
    }
}
