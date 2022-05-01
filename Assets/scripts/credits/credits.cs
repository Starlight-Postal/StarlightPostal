using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    global_data glob;
    // Start is called before the first frame update
    void Start()
    {
        TICK = 0;
    }

    private void OnEnable()
    {
        gdata = GameObject.FindObjectOfType<SaveFileManager>();
        glob = GameObject.FindObjectOfType<global_data>();
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
            if (glob.creditsBackToMenu)
            {
                SceneManager.LoadScene("Main Menu");
                gdata.creditsBackToMenu = false;
            }
            else
            {
                gdata.saveData.introScene = false;
                CheckpointManager.GotoCheckpoint(-2, "level 1");
            }
        }
    }
}
