using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lift_chair : Interractable
{
    public chairlift chairlift;
    public int node = 0;
    public float speed;
    Transform trans;
    public anchor anchor;
    public Transform anchorTrans;

    public Transform playerTrans;
    public player player;
    public float range = 2;

    public AudioSource sfx_attach;
    public AudioSource sfx_sit;
    public AudioSource sfx_unsit;

    balloon balloon;

    public Transform bodyTrans;

    // Start is called before the first frame update
    void Start()
    {
        trans = gameObject.GetComponent<Transform>();
        balloon = GameObject.Find("balloon").GetComponent<balloon>();
        anchor=balloon.anchorObj.GetComponent<anchor>();
        anchorTrans = GameObject.Find("balloon").GetComponent<balloon>().anchorObj.GetComponent<Transform>();

        player = GameObject.Find("player").GetComponent<player>();
        playerTrans = player.gameObject.GetComponent<Transform>();

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 t = chairlift.path[(node + 1) % chairlift.path.Length];
        Vector2 d = t - new Vector2(trans.position.x, trans.position.y);
        if (d.x > 0)
        {
            bodyTrans.localScale = new Vector3(1, 1, 1);
        }
        if (d.x < 0)
        {
            bodyTrans.localScale = new Vector3(-1, 1, 1);
        }
        if (d.magnitude > speed)
        {
            trans.position += (Vector3)(d / d.magnitude * speed);
        } else
        {
            node = (node + 1) % chairlift.path.Length;
            trans.position += (Vector3)d;
            if (node == chairlift.path.Length-1)
            {
                node = 0;
                trans.position = new Vector3(chairlift.path[node].x, chairlift.path[node].y, trans.position.z);
                if (anchor.target == gameObject)
                {
                    anchor.stuck = false;
                    anchor.target = null;
                    anchor.targetTrans = anchorTrans;
                }
                if (player.inChair && player.chair == trans)
                {
                    player.inChair = false;
                    player.chair = null;
                    Debug.Log("chair end");
                    sfx_unsit.Play(0);
                }
            }
            
        }

        if (balloon.anchored&&new Vector2(trans.position.x - anchorTrans.position.x, trans.position.y - anchorTrans.position.y).magnitude < 1)
        {
            if (anchor.target != gameObject)
            {
                sfx_attach.Play(0);
                Debug.Log("snatched");
            }
            anchor.target = gameObject;
            anchor.targetTrans = trans;
            anchor.tOff = new Vector3(0, 0, anchorTrans.position.z - trans.position.z);// anchorTrans.position - trans.position;
            //anchorTrans.position = trans.position;
            
            
        }

    }

    public override void OnPlayerInterract()
    {
        player.inChair = !player.inChair;
        if (player.inChair)
        {
            player.chair = trans;
            Debug.Log("in chair");
            sfx_sit.Play(0);
        }
    }

}
