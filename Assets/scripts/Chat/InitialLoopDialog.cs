using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialLoopDialog : Conversation
{

    public string[] loopScript;

    public override void OnConversationEnd()
    {
        script = loopScript;
    }

}
