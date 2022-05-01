using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingConversation : Conversation
{

    public string[] loopScript;
    public bool encountered = false;

    public override void OnConversationEnd()
    {
        base.OnConversationEnd();
        script = loopScript;
        encountered = true;
    }

}
