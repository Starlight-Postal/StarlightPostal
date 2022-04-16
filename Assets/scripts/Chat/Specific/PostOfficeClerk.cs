using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PostOfficeClerk : Conversation
{

    public float DELIVERED_HEIGHT_CAP = 100;

    public AudioSource sound;
    public int dropLine;

    public string[] script_maildrop;
    public string[] script_delivery;
    public string[] script_delivered;
    public string[] script_done;

    public MailPhase phase;

    private void Start()
    {
        base.Start();
        phase = MailPhase.MAILDROP;
    }

    public override void OnConversationStart()
    {
        switch (phase)
        {
            case MailPhase.MAILDROP:
                script = script_maildrop;
                break;
            case MailPhase.DELIVERY:
                script = script_delivery;
                break;
            case MailPhase.DELIVERED:
                script = script_delivered;
                break;
            case MailPhase.DONE:
            default:
                script = script_done;
                break;
        }
    }

    public override void OnConversationEnd()
    {
        switch (phase)
        {
            case MailPhase.MAILDROP:
                phase = MailPhase.DELIVERY;
                GameObject.FindObjectsOfType<balloon>()[0].heightCap = DELIVERED_HEIGHT_CAP; //package optional
                GameObject.FindObjectsOfType<TutorialNPC>()[0].phase = TutorialPhase.POSTOFFICEDELIVERY;
                break;
            case MailPhase.DELIVERY:
                break;
            case MailPhase.DELIVERED:
                phase = MailPhase.DONE;
                //GameObject.FindObjectsOfType<balloon>()[0].heightCap = DELIVERED_HEIGHT_CAP; //package mandatory, but we should move this to the recipient instead if we want to do this
                break;
            case MailPhase.DONE:
            default:
                break;
        }
    }

    public override void OnConversationLineUpdate(int index)
    {
        //Debug.Log(phase + " , " + index);
        if (phase == MailPhase.MAILDROP && index == dropLine)
        {
            sound.Play(0);
            //Debug.Log("SOUND");
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
