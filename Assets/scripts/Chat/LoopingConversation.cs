using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingConversation : Conversation
{

    public string[] loopScript;

    public override void OnConversationEnd()
    {
        base.OnConversationEnd();
        script = loopScript;
    }

}
