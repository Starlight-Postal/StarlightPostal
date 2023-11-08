using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatOwner : Conversation
{
    public string[] helpScript;
    public string[] helpLoopScript;
    public string[] thanksScript;
    public string[] thanksLoopScript;
    public TreeCat tree;
    public GameObject cat;
    public AudioSource sfx_place;
    public int dropLine;

    public SpriteRenderer sprite;
    public Sprite happy;
    public Sprite sad;
    // Start is called before the first frame update
    void Start()
    {
        script = helpScript;
        //scriptIndex = 0;
        sprite.sprite = sad;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindObjectsOfType<player>()[0];
        }
        if (tree.found)
        {
            if (script == helpScript || script == helpLoopScript)
            {
                script = thanksScript;
            }
        }
    }
    public override void OnConversationEnd()
    {
        if (script == helpScript)
        {
            script = helpLoopScript;
        }
        if (script == thanksScript)
        {
            script = thanksLoopScript;
        }
    }
    public override void OnConversationStart()
    {
        if (tree.found)
        {
            sprite.sprite = happy;
        }
    }
    public override void OnConversationLineUpdate(int index)
    {
        if (script == thanksScript)
        {
            if (index == dropLine)
            {
                cat.SetActive(true);
                sfx_place.Play(0);
                
                // Award points for package delivery
                FindObjectOfType<SaveFileManager>().saveData.score += 2000;
            }
        }
    }
}
