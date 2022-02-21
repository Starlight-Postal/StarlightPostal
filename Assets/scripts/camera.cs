using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public float speed = 0.05f;
    public Transform trans;
    public Camera cam;
    public Transform balloonTrans;
    public Transform playerTrans;
    public Transform target;

    public player player;

    public float balloonSize = 20;
    public float playerSize = 10;

    public Vector2 ratio;
    public Vector2 camRange;

    public Transform range;
    // Start is called before the first frame update
    void Start()
    {
        trans = gameObject.GetComponent<Transform>();
        cam = gameObject.GetComponent<Camera>();
        balloonTrans = GameObject.Find("balloon").GetComponent<Transform>();
        playerTrans = GameObject.Find("player").GetComponent<Transform>();
        target = playerTrans;
        player = GameObject.Find("player").GetComponent<player>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        camRange = ratio * cam.orthographicSize;
        if (player.inBalloon)
        {
            target = balloonTrans;
        } else
        {
            target = playerTrans;
        }
        trans.position += (new Vector3(target.position.x,target.position.y,trans.position.z) - trans.position) * speed;
        if (target == balloonTrans)
        {
            cam.orthographicSize += (balloonSize - cam.orthographicSize) * 0.01f;
        }
        if(target == playerTrans)
        {
            cam.orthographicSize += (playerSize - cam.orthographicSize) * 0.01f;
        }

        if (range != null)
        {
            Debug.Log(trans.position.x+" , "+ (range.position.x - (range.localScale.x / 2f - camRange.x)));
            if (trans.position.x < range.position.x - (range.localScale.x / 2f - camRange.x))
            {
                trans.position = new Vector3(range.position.x - (range.localScale.x / 2f - camRange.x), trans.position.y, trans.position.z);
            }
            if (trans.position.x > range.position.x + (range.localScale.x / 2f - camRange.x))
            {
                trans.position = new Vector3(range.position.x + (range.localScale.x / 2f - camRange.x), trans.position.y, trans.position.z);
            }
            if (trans.position.y < range.position.y - (range.localScale.y / 2f - camRange.y))
            {
                trans.position = new Vector3( trans.position.x, range.position.y - (range.localScale.y / 2f - camRange.y), trans.position.z);
            }
            if (trans.position.y > range.position.y + (range.localScale.y / 2f - camRange.y))
            {
                trans.position = new Vector3(trans.position.x, range.position.y + (range.localScale.y / 2f - camRange.y), trans.position.z);
            }
        }
    }
}
