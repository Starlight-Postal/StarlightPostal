using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{

    public bool facePlayer;
    public bool invert;
    public Transform bodyTrans;

    private Transform playerTrans;
    private Transform trans;
    private float visWidth;
    public bool facingLeft;

    void Start()
    {
        playerTrans = GameObject.Find("player").GetComponent<Transform>();
        trans = GetComponent<Transform>();
        visWidth = bodyTrans.localScale.x;
    }

    void FixedUpdate()
    {
        if (facePlayer)
        {
            if (playerTrans.position.x > trans.position.x)
            {
                facingLeft = invert;
            }
            if (playerTrans.position.x < trans.position.x)
            {
                facingLeft = !invert;
            }
        }

        if (facingLeft)
        {
            bodyTrans.localScale += new Vector3((visWidth - bodyTrans.localScale.x), 0, 0) * 0.1f;
        }
        else
        {
            bodyTrans.localScale += new Vector3((-visWidth - bodyTrans.localScale.x), 0, 0) * 0.1f;
        }
    }    

}
