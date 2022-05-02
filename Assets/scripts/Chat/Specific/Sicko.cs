using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SickoPhase
{
    HELP,
    HELP_LOOP,
    THANKS,
    THANKS_LOOP
}

public class Sicko : Conversation
{
    public string[] helpScript;
    public string[] helpLoopScript;
    public string[] thanksScript;
    public string[] thanksLoopScript;
    public LoopingConversation neighbor;
    public GameObject soup;
    public AudioSource sfx_place;
    public int dropLine;

    private SickoPhase phase;
    // Start is called before the first frame update
    void Start()
    {
        phase = SickoPhase.HELP;
        script = helpScript;
    }
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindObjectsOfType<player>()[0];
        }
        if (neighbor.encountered)
        {
            if (phase == SickoPhase.HELP || phase == SickoPhase.HELP_LOOP)
            {
                phase = SickoPhase.THANKS;
            }
        }
    }

    public override void OnConversationStart()
    {
        switch (phase)
        {
            case SickoPhase.HELP_LOOP:
                script = helpLoopScript;
                break;
            case SickoPhase.THANKS:
                script = thanksScript;
                break;
            case SickoPhase.THANKS_LOOP:
                script = thanksLoopScript;
                break;
            default:
                script = helpScript;
                break;
        }
    }
    
    public override void OnConversationEnd()
    {
        if (phase == SickoPhase.HELP)
        {
            phase = SickoPhase.HELP_LOOP;
        }
        if (phase == SickoPhase.THANKS)
        {
            phase = SickoPhase.THANKS_LOOP;
        }
    }
    public override void OnConversationLineUpdate(int index)
    {
        if (phase == SickoPhase.THANKS)
        {
            if (index == dropLine)
            {
                soup.SetActive(true);
                sfx_place.Play(0);
            }
        }
    }
}
