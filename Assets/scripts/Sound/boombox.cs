using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boombox : MonoBehaviour
{
    public GameObject package;
    public float vol;
    public Jukebox music;
    public Transform trans;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        music = GameObject.Find("Jukebox").GetComponent<Jukebox>();
        trans = gameObject.transform;
        player = GameObject.Find("player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (package.active)
        {
            float d = Vector2.Distance(new Vector2(player.position.x, player.position.y), new Vector2(trans.position.x, trans.position.y));
            music.beatPower = vol*(1/(0.9f+d))-(1/9f);
        }
        else
        {
            music.beatPower = 0;
        }
    }
}
