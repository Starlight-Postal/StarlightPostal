using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterNPC : LoopingConversation
{
    public bool deliveredTo = false;

    public GameObject package;
    public AudioSource sound;

    public int dropLine;

    private SaveFileManager save;

    void Start()
    {
        save = GameObject.FindObjectOfType<SaveFileManager>();
    }

    public override void OnConversationEnd()
    {
        base.OnConversationEnd();
        deliveredTo = true;
        save.saveData.postOfficeClerkPhases[GameObject.FindObjectsOfType<PostOfficeClerk>()[0].level] = MailPhase.DELIVERED;
        //GameObject.FindObjectsOfType<TutorialNPC>()[0].phase = TutorialPhase.DELIVERED;
    }

    public override void OnConversationLineUpdate(int index)
    {
        if (index == dropLine)
        {
            if (!package.active)
            {
                package.SetActive(true);
                sound.Play();
            }
        }
    }

}
