using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecieveSkin : LoopingConversation
{
    public Sprite oldSkin;
    public Sprite newSkin;
    public SpriteRenderer skin;
    public AudioSource sound;


    public int dropLine;

    private SaveFileManager save;
    // Start is called before the first frame update
    void Start()
    {
        save = GameObject.FindObjectOfType<SaveFileManager>();
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
                save.saveData.skinRecieved = true;
            }
        }
    }
}
