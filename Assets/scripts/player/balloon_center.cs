using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balloon_center : MonoBehaviour
{
    public balloon balloon;
    // Start is called before the first frame update
    void Start()
    {
        balloon = GameObject.Find("balloon").GetComponent<balloon>();
    }
    void OnCollisionEnter2D(Collision2D c)
    {
        balloon.centerHit();
    }
}
