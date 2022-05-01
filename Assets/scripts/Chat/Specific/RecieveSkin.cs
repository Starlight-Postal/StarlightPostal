using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecieveSkin : LoopingConversation
{
    public bool deliveredTo = false;
    public Sprite oldSkin;
    public Sprite newSkin;
    public SpriteRenderer skin;
    public AudioSource sound;


    public int dropLine;
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
    }

    public override void OnConversationLineUpdate(int index)
    {
        if (index == dropLine)
        {
            if (skin.sprite!=newSkin)
            {
                skin.sprite = newSkin;
                sound.Play();
                deliveredTo = true;
            }
        }
    }
}
