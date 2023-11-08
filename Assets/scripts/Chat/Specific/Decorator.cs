using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decorator : LoopingConversation
{
    public SpriteRenderer tree;
    public Sprite blank;
    public Sprite decorated;
    bool deliveredTo = false;
    public int dropLine;
    public AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindObjectsOfType<player>()[0];
        }
        if (deliveredTo)
        {
            tree.sprite = decorated;
        } else
        {
            tree.sprite = blank;
        }
    }

    public override void OnConversationLineUpdate(int index)
    {
        if (index == dropLine)
        {
            if (!deliveredTo)
            {
                
                sound.Play();
                deliveredTo = true;
                
                // Award points for package delivery
                FindObjectOfType<SaveFileManager>().saveData.score += 2000;
            }
        }
    }
}
