using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cam_fade : MonoBehaviour
{
    SpriteRenderer sprite;
    public float fade = 1;
    public float fadeOut = 0.025f;
    // Start is called before the first frame update
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer > ();
        fade = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (fade > 0)
        {
            fade -= fadeOut;
        }

        sprite.color = new Color(0, 0, 0, fade);
    }
}
