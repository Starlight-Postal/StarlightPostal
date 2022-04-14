using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pulse : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Color colorA;
    public Color colorB;
    public float speed;
    public float period;
    int TICK = 0;
    // Start is called before the first frame update
    void Start()
    {
        TICK = 0;
    }

    // Update is called once per frame
    void Update()
    {
        TICK++;
        float k = (Mathf.Sin(TICK * speed + period) + 1) / 2f;
        sprite.color = colorA * k + colorB * (1 - k);
    }
}
