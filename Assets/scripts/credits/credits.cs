using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class credits : MonoBehaviour
{
    public GameObject[] screens;
    public Vector2[] times;
    public int TICK = 0;
    public int fps = 60;
    public int end;
    public int fadeLength = 3;
    public SpriteRenderer fade;
    public string nextLevel;
    // Start is called before the first frame update
    void Start()
    {
        TICK = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for(int i = 0;i < screens.Length; i++)
        {
            if (times[i].x*fps <= TICK && times[i].y*fps > TICK)
            {
                screens[i].SetActive(true);
            } else
            {
                screens[i].SetActive(false);
            }
        }
        if (TICK > (end - fadeLength) * fps)
        {
            fade.color = new Color(0, 0, 0,1-((end-((float)TICK/fps)) / fadeLength));
        } else
        {
            fade.color = new Color(0, 0, 0, 0);
        }
        TICK++;
        if (TICK > end * fps)
        {
            Application.LoadLevel(nextLevel);
        }
    }
}
