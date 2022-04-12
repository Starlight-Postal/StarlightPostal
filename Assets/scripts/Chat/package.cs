using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class package : MonoBehaviour
{
    public EncounterNPC recipient;
    public SpriteRenderer sprite;
    public int line;
    public bool got = false;
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
                got = true;
            }
        }
        sprite.enabled = got;
    }
}
