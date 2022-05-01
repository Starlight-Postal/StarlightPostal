using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IngameDebugConsole;
using UnityEngine.SceneManagement;

public class global_data : MonoBehaviour
{
    public Sprite[] skins;
    public Color[] burnColors;
    public Color[] baseColors;
    public Color[] ventColors;
    
    public float[] heightCaps;

    public int getSkin(Sprite skin)
    {
        for(int i=0;i < skins.Length;i++)
        {
            if (skins[i] == skin)
            {
                return i;
            }
        }
        return -1;
    }

    public float getHeightCap()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "level 1":
                return heightCaps[0];
            case "level 2":
                return heightCaps[1];
            case "level 3":
                return heightCaps[2];
            default:
                return 0;
        }
    }
    public void setHeightCap(float cap)
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "level 1":
                heightCaps[0] = cap;
                return;
            case "level 2":
                heightCaps[1] = cap;
                return;
            case "level 3":
                heightCaps[2] = cap;
                return;
            default:
                return;
        }
    }

}
