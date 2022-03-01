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
    // Start is called before the first frame update
    void Start()
    {
        trans = gameObject.GetComponent<Transform>();
        player = GameObject.Find("player").GetComponent<player>();
        playerTrans = GameObject.Find("player").GetComponent<Transform>();
        balloon = GameObject.Find("balloon").GetComponent<balloon>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!player.inBalloon)
        {
            if(Vector2.Distance(new Vector2(trans.position.x,trans.position.y),new Vector2(playerTrans.position.x, playerTrans.position.y)) <= range)
            {
                if (Input.GetKey("space"))
                {
                    Debug.Log("dropoff");
                    if (balloon.heightCap < newHeight)
                    {
                        balloon.heightCap = newHeight;
                    }
                }
            }
        }
    }
}
