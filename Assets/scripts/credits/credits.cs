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
    SaveFileManager gdata;
    // Start is called before the first frame update
    void Start()
    {
        TICK = 0;
        gdata = GameObject.FindObjectOfType<SaveFileManager>();
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
        } else if (TICK < (fadeLength*0.5f)*fps) {
            fade.color = new Color(0, 0, 0, 1 - (((float)TICK / fps) / (fadeLength*0.5f)));
        } else
        {
            fade.color = new Color(0, 0, 0, 0);
        }
        TICK++;
        if (TICK > end * fps)
        {
            //Application.LoadLevel(nextLevel);
            if (gdata != null)
            {
                gdata.saveData.introScene = false;
            }
            CheckpointManager.GotoCheckpoint(-2, "level 1");
        }
    }
}
