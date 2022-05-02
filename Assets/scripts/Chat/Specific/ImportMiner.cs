using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ImportMinerPhase
{
    HELP,
    HELP_LOOP,
    THANKS,
    THANKS_LOOP
}

public class ImportMiner : Conversation
{
    public string[] helpScript;
    public string[] helpLoopScript;
    public string[] thanksScript;
    public string[] thanksLoopScript;
    public LoopingConversation supplier;
    public GameObject import;
    public GameObject empty;
    public AudioSource sfx_place;
    public AudioSource sfx_pay;
    public int dropLine;
    public int payLine;
    public int pay;

    private ImportMinerPhase phase;
    private SaveFileManager save;
    // Start is called before the first frame update
    void Start()
    {
        phase = ImportMinerPhase.HELP;
        script = helpScript;
        //scriptIndex = 0;
    }

    private void OnEnable()
    {
        base.OnEnable();
        save = GameObject.FindObjectOfType<SaveFileManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindObjectsOfType<player>()[0];
        }
        if (supplier.encountered)
        {
            if (phase == ImportMinerPhase.HELP || phase == ImportMinerPhase.HELP_LOOP)
            {
                phase = ImportMinerPhase.THANKS;
            }
        }
    }
    
    public override void OnConversationStart()
    {
        switch (phase)
        {
            case ImportMinerPhase.HELP_LOOP:
                script = helpLoopScript;
                break;
            case ImportMinerPhase.THANKS:
                script = thanksScript;
                break;
            case ImportMinerPhase.THANKS_LOOP:
                script = thanksLoopScript;
                break;
            default:
                script = helpScript;
                break;
        }
    }
    
    public override void OnConversationEnd()
    {
        if (phase == ImportMinerPhase.HELP)
        {
            phase = ImportMinerPhase.HELP_LOOP;
        }
        if (phase == ImportMinerPhase.THANKS)
        {
            phase = ImportMinerPhase.THANKS_LOOP;
        }
    }
    public override void OnConversationLineUpdate(int index)
    {
        if (phase == ImportMinerPhase.THANKS)
        {
            if (index == dropLine)
            {
                import.SetActive(true);
                empty.SetActive(false);
                sfx_place.Play(0);
            }
            if (index == payLine)
            {
                save.saveData.coins += pay;
                sfx_pay.Play(0);
            }
        }
    }
}
