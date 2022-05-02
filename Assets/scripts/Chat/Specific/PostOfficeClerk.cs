using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

class PostOfficeClerk : Conversation
{

    public float DELIVERED_HEIGHT_CAP = 100;

    public AudioSource sound;
    public int dropLine;

    public string[] script_maildrop;
    public string[] script_delivery;
    public string[] script_delivered;
    public string[] script_done;

    private SaveFileManager save;
    public int level = 0;

    private void Start()
    {
        base.Start();
        switch (SceneManager.GetActiveScene().name)
        {
            case "level 1":
                level = 0;
                break;
            case "level 2":
                level = 1;
                break;
            case "level 3":
                level = 2;
                break;
        }
        save = GameObject.FindObjectOfType<SaveFileManager>();
    }

    public override void OnConversationStart()
    {
        GameObject.FindObjectsOfType<balloon>()[0].heightCap = DELIVERED_HEIGHT_CAP; //failsafe
        switch (save.saveData.postOfficeClerkPhases[level])
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
        switch (save.saveData.postOfficeClerkPhases[level])
        {
            case MailPhase.MAILDROP:
                save.saveData.postOfficeClerkPhases[level] = MailPhase.DELIVERY;
                GameObject.FindObjectsOfType<balloon>()[0].heightCap = DELIVERED_HEIGHT_CAP; //package optional
                TutorialNPC captain = GameObject.FindObjectsOfType<TutorialNPC>()[0];
                if (captain != null)
                {
                    save.saveData.tutorialPhase = TutorialPhase.POSTOFFICEDELIVERY;
                }
                //GameObject.FindObjectsOfType<TutorialNPC>()[0].phase = TutorialPhase.POSTOFFICEDELIVERY;
                break;
            case MailPhase.DELIVERY:
                break;
            case MailPhase.DELIVERED:
                save.saveData.postOfficeClerkPhases[level] = MailPhase.DONE;
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
        if (save.saveData.postOfficeClerkPhases[level] == MailPhase.MAILDROP && index == dropLine)
        {
            sound.Play(0);
            //Debug.Log("SOUND");
        }
    }

}

public enum MailPhase
{
    MAILDROP,
    DELIVERY,
    DELIVERED,
    DONE
}
