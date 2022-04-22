using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astronomer : LoopingConversation
{
    public bool deliveredTo = false;
    public Sprite oldSkin;
    public Sprite newSkin;
    public SpriteRenderer skin;
    public AudioSource sound;

    public GameObject photo;
    public int skinLine;
    public int photoLine;
    public int turnLine;

    public FacePlayer face;
    public bool faceTelescope = false;

    // Start is called before the first frame update
    void Start()
    {
        photo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindObjectsOfType<player>()[0];
        }
        if (faceTelescope)
        {
            face.facePlayer = false;
            face.facingLeft = true;
        } else
        {
            face.facePlayer = true;
        }
    }

    public override void OnConversationLineUpdate(int index)
    {
        if (index == skinLine)
        {
            if (skin.sprite != newSkin)
            {
                skin.sprite = newSkin;
                sound.Play();
                deliveredTo = true;
            }
        }
        if (index == photoLine)
        {
            if (!photo.active)
            {
                photo.SetActive(true);
                sound.Play();
            }
        }


        faceTelescope = (index == turnLine);

    }
}
