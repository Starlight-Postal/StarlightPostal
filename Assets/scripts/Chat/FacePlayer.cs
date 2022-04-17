using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{

    public bool facePlayer;
    public bool invert;
    public bool faceInRange;
    public Transform bodyTrans;
    public bool defaultLook;

    private Transform playerTrans;
    private Transform trans;
    private float visWidth;
    public bool facingLeft;

    public Conversation npc;

    void Start()
    {
        playerTrans = GameObject.Find("player").GetComponent<Transform>();
        trans = GetComponent<Transform>();
        visWidth = bodyTrans.localScale.x;
        npc = gameObject.GetComponent<Conversation>();
        defaultLook = (bodyTrans.localScale.x > 0);
        if (invert)
        {
            defaultLook = !defaultLook;
        }
    }

    void FixedUpdate()
    {
        facingLeft = defaultLook;
        if (facePlayer)
        {
            if (!faceInRange || npc.isInRange)
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
