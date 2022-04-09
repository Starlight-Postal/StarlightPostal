using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PostOfficeClerk : Conversation
{

    public string[] script_maildrop;
    public string[] script_delivery;
    public string[] script_delivered;
    public string[] script_done;

    public MailPhase phase;

    private void Start()
    {
        phase = MailPhase.MAILDROP;
    }

    public override void OnConversationStart()
    {
        switch (phase)
        {
            case MailPhase.MAILDROP:
                phase = MailPhase.DELIVERY;
                script = script_maildrop;
                break;
            case MailPhase.DELIVERY:
                script = script_delivery;
                break;
            case MailPhase.DELIVERED:
                phase = MailPhase.DONE;
                script = script_delivered;
                break;
            case MailPhase.DONE:
            default:
                script = script_done;
                break;
        }
    }

}

enum MailPhase
{
    MAILDROP,
    DELIVERY,
    DELIVERED,
    DONE
}
