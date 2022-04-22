using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
    void Start()
    {
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
                soup.SetActive(true);
                sfx_place.Play(0);
            }
        }
    }
}
