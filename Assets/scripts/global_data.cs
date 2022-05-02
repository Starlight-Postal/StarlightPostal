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
    
    public bool creditsBackToMenu = false;

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

}
