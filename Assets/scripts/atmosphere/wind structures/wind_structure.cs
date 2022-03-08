using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wind_structure : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
    public virtual Vector2 getWind(float x, float y) { return new Vector2(0, 0); }
}
