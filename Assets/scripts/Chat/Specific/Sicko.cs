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

    private SaveFileManager save;
    // Start is called before the first frame update
    void Start()
    {
        script = helpScript;
        save = GameObject.FindObjectOfType<SaveFileManager>();
    }
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindObjectsOfType<player>()[0];
        }
        if (neighbor.encountered)
        {
            if (save.saveData.sickoSnowmanPhase == SickoPhase.HELP || save.saveData.sickoSnowmanPhase == SickoPhase.HELP_LOOP)
            {
                save.saveData.sickoSnowmanPhase = SickoPhase.THANKS;
            }
        }
    }

    public override void OnConversationStart()
    {
        switch (save.saveData.sickoSnowmanPhase)
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
        if (save.saveData.sickoSnowmanPhase == SickoPhase.HELP)
        {
            save.saveData.sickoSnowmanPhase = SickoPhase.HELP_LOOP;
        }
        if (save.saveData.sickoSnowmanPhase == SickoPhase.THANKS)
        {
            save.saveData.sickoSnowmanPhase = SickoPhase.THANKS_LOOP;
        }
    }
    public override void OnConversationLineUpdate(int index)
    {
        if (save.saveData.sickoSnowmanPhase == SickoPhase.THANKS)
        {
            if (index == dropLine)
            {
                soup.SetActive(true);
                sfx_place.Play(0);
            }
        }
    }
}
