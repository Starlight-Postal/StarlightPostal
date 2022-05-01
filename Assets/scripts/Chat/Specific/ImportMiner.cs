using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private SaveFileManager save;
    // Start is called before the first frame update
    void Start()
    {
        script = helpScript;
        //scriptIndex = 0;
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
    public override void OnConversationLineUpdate(int index)
    {
        if (script == thanksScript)
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
