using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCat : Conversation
{
    public string[] loopScript;
    public bool found = false;
    public AudioSource sfx_place;
    public bool showCat = false;
    public bool shake = true;
    public Transform tree;
    Vector3 treePos;
    int TICK = 0;
    public float speed = 0.1f;
    public float power = 1;
    public float amp = 0;
    public GameObject cat;
    // Start is called before the first frame update
    void Start()
    {
        treePos = tree.position;
    }
    void FixedUpdate()
    {
        base.FixedUpdate();
        if (player == null)
        {
           player = GameObject.FindObjectsOfType<player>()[0];
        }
        TICK++;
        cat.SetActive(showCat);
        if (shake)
        {
            float s = (Mathf.Sin(TICK * speed)+amp) * power;
            if (s < 0)
            {
                s = 0;
            }
            tree.position = treePos + new Vector3(Random.Range(-s, s), Random.Range(-s, s), 0);
        } else
        {
            tree.position = treePos;
        }
        
    }

    public override void OnConversationLineUpdate(int index)
    {
        shake = false;
        found = true;
        if (script!=loopScript)
        {
            if (index == 4)
            {
                sfx_place.Play(0);
                showCat = true;
            }
            if(index==8)
            {
                sfx_place.Play(0);
                showCat = false;
            }
        }
    }

    public override void OnConversationStart()
    {
        Debug.Log("hello mario");
        if (found)
        {
            script = loopScript;
        }
    }
}
