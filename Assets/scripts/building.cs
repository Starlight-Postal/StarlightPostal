using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class building : MonoBehaviour
{
    public GameObject exterior;
    public Transform trans;
    public Transform player;
    public bool inside = false;
    SpriteRenderer[] sprites;
    public float extAlpha;
    // Start is called before the first frame update
    void Start()
    {
        trans = gameObject.GetComponent<Transform>();
        player = GameObject.Find("player").GetComponent<Transform>();
        sprites = exterior.GetComponentsInChildren<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        inside = (Mathf.Abs(player.position.x - trans.position.x) <= trans.localScale.x/2f && Mathf.Abs(player.position.y - (trans.position.y+trans.localScale.y/2f)) <= trans.localScale.y/2f);
        if (inside)
        {
            extAlpha += (0 - extAlpha) * 0.1f;
        } else
        {
            extAlpha += (1 - extAlpha) * 0.1f;
        }
        for(int i = 0;i < sprites.Length;i++)
        {
            sprites[i].color = new Color(sprites[i].color.r, sprites[i].color.g, sprites[i].color.b, extAlpha*1);
        }
        //exterior.SetActive(!inside);
        
    }
}
