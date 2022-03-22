using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wind : MonoBehaviour
{
    public wind_structure[] windStructs;
    public Vector2 w;
    // Start is called before the first frame update
    void Start()
    {
        w = new Vector2(0, 0);
        windStructs = GameObject.FindObjectsOfType<wind_structure>();
        for(int i = 0;i < windStructs.Length;i++)
        {
            if (windStructs[i] is wind_fill)
            {
                wind_structure last = windStructs[windStructs.Length - 1];
                windStructs[windStructs.Length - 1] = windStructs[i];
                windStructs[i] = last;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 getWind(float x,float y)
    {
        w = new Vector2(0, 0);
        for(int i = 0;i < windStructs.Length;i++)
        {
            w += windStructs[i].getWind(x, y);
        }
        //Debug.Log(wind*100);
        return w;
    }
}
