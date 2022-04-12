using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class package : MonoBehaviour
{
    public EncounterNPC recipient;
    public SpriteRenderer sprite;
    public int line;
    public bool got = false;
    public AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
        got = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (recipient.encountered)
        {
            if (recipient.counter >= line)
            {
                if (!got)
                {
                    sound.Play(0);
                }
                got = true;
            }
        }
        sprite.enabled = got;
    }
}
