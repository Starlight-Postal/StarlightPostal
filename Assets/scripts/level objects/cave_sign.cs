using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cave_sign : MonoBehaviour
{
    SpriteRenderer sprite;
    public Sprite a;
    public Sprite b;
    int TICK = 0;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        TICK = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TICK++;
        if (Mathf.Floor(TICK * speed) % 2 == 0)
        {
            sprite.sprite = a;
        } else
        {
            sprite.sprite = b;
        }
    }
}
