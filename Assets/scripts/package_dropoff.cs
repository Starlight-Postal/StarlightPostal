using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class package_dropoff : MonoBehaviour
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
    }

    // Update is called once per frame
    void Update()
    {
        sprite.color = new Color(1, 0.9f, 0.9f);
        if (!player.inBalloon)
        {
            if(Vector2.Distance(new Vector2(trans.position.x,trans.position.y),new Vector2(playerTrans.position.x, playerTrans.position.y)) <= range)
            {
                if (balloon.heightCap < newHeight)
                {
                    if (Input.GetKey("space"))
                    {
                        Debug.Log("dropoff");
                        balloon.heightCap = newHeight;
                    }
                    sprite.color = new Color(1,1,1);
                }
            }
        }
    }
}
