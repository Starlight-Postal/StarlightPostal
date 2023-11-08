using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketNote : Conversation
{
    bool encountered = false;
    balloon balloon;
    // Start is called before the first frame update
    void Start()
    {
        encountered = false;
        balloon = GameObject.Find("balloon").GetComponent<balloon>();
        balloon.lockEntry = true;
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindObjectsOfType<player>()[0];
        }
        if (player.inBalloon)
        {
            balloon.lockEntry = false;
            this.gameObject.SetActive(false);
        }
    }

    public override void OnConversationStart()
    {
        encountered = true;
        balloon.lockEntry = false;
    }
    public override void OnConversationEnd()
    {
        this.gameObject.SetActive(false);
                
        // Award points for reading the note
        FindObjectOfType<SaveFileManager>().saveData.score += 100;
    }
}
