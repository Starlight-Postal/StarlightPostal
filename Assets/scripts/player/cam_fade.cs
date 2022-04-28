using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam_fade : MonoBehaviour
{
    SpriteRenderer sprite;
    public float fade = 1;
    public float fadeOut = 0.025f;
    bool start = true;
    // Start is called before the first frame update
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        fade = 1;
        start = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (sprite == null)
        {
            sprite = gameObject.GetComponent<SpriteRenderer>();
        }
        if (start)
        {
            if (fade > 0)
            {
                fade -= fadeOut;
            } else
            {
                start = false;
            }
        }
        sprite.color = new Color(0, 0, 0, fade);
    }
}
