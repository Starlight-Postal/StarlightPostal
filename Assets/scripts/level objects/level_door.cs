using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level_door : MonoBehaviour
{
    GameObject balloon;
    public string loadLevel;
    // Start is called before the first frame update
    void Start()
    {
        balloon = GameObject.Find("Center");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject == balloon)
        {
            Debug.Log("next level");
            Application.LoadLevel(loadLevel);
        }
    }
}
