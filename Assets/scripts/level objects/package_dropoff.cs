using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class package_dropoff : Interractable
{
    Transform playerTrans;
    Transform trans;
    player player;
    balloon balloon;
    public float newHeight;
    public float range;
    public SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        trans = gameObject.GetComponent<Transform>();
        player = GameObject.Find("player").GetComponent<player>();
        playerTrans = GameObject.Find("player").GetComponent<Transform>();
        balloon = GameObject.Find("balloon").GetComponent<balloon>();
        //sprite = gameObject.GetComponent<SpriteRenderer>();
        sprite.color = new Color(1,1,1);
    }

    public override void OnPlayerInterract()
    {
        if (balloon.heightCap < newHeight)
        {
            Debug.Log("dropoff");
            balloon.heightCap = newHeight;
            sprite.color = new Color(0.9f, 0.8f, 0.8f);
        }
    }

}
