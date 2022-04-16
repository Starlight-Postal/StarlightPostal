using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterNPC : Conversation
{

    public string[] loopScript;
    public bool deliveredTo = false;

    public GameObject package;
    public AudioSource sound;

    public override void OnConversationEnd()
    {
        if (!deliveredTo)
        {
            package.SetActive(true);
            sound.Play();
        }
        deliveredTo = true;
        script = loopScript;
        GameObject.FindObjectsOfType<PostOfficeClerk>()[0].phase = MailPhase.DELIVERED;
        GameObject.FindObjectsOfType<TutorialNPC>()[0].phase = TutorialPhase.DELIVERED;
    }

}
