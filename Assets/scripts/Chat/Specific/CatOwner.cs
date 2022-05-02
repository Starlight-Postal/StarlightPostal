using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CatOwnerPhase
{
    HELP,
    HELP_LOOP,
    THANKS,
    THANKS_LOOP
}

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

    private CatOwnerPhase phase;
    // Start is called before the first frame update
    void Start()
    {
        script = helpScript;
        //scriptIndex = 0;
        sprite.sprite = sad;
        phase = CatOwnerPhase.HELP;
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
        if (phase == CatOwnerPhase.HELP)
        {
            phase = CatOwnerPhase.HELP_LOOP;
        }
        if (phase == CatOwnerPhase.THANKS)
        {
            phase = CatOwnerPhase.THANKS_LOOP;
        }
    }
    public override void OnConversationStart()
    {
        if (tree.found)
        {
            sprite.sprite = happy;
        }
        switch (phase)
        {
            case CatOwnerPhase.HELP_LOOP:
                script = helpLoopScript;
                break;
            case CatOwnerPhase.THANKS:
                script = thanksScript;
                break;
            case CatOwnerPhase.THANKS_LOOP:
                script = thanksLoopScript;
                break;
            default:
                script = helpScript;
                break;
        }
    }
    public override void OnConversationLineUpdate(int index)
    {
        if (phase == CatOwnerPhase.THANKS)
        {
            if (index == dropLine)
            {
                cat.SetActive(true);
                sfx_place.Play(0);
            }
        }
    }
}
