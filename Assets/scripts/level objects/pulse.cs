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

    public bool randomPeriod = false;
    // Start is called before the first frame update
    void Start()
    {
        if (sprite == null)
        {
            sprite = gameObject.GetComponent<SpriteRenderer>();
        }
        TICK = 0;
        if (randomPeriod)
        {
            period = Random.Range(0, 2 * Mathf.PI);
        }
    }

    // Update is called once per frame
    void Update()
    {
        TICK++;
        float k = (Mathf.Sin(TICK * speed + period) + 1) / 2f;
        sprite.color = colorA * k + colorB * (1 - k);
    }
}
