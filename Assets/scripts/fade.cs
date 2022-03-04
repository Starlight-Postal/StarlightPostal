using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fade : MonoBehaviour
{
    SpriteRenderer sprite;
    public float alpha = 1;
    public float speed = 0.01f;
    // Start is called before the first frame update
    void Start()
    {
        sprite =gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        alpha += speed;
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
    }
}
