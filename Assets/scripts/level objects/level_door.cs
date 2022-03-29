using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level_door : MonoBehaviour
{
    public string loadLevel;
    public bool upDoor = true;

    public GameObject balloonObj;
    public balloon balloon;
    public Transform trans;
    // Start is called before the first frame update
    void Start()
    {
        balloonObj = GameObject.Find("Center");
        balloon = GameObject.Find("balloon").GetComponent<balloon>();
        trans = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject == balloonObj)
        {
            if (upDoor)
            {
                if (balloon.targetHeight >= trans.position.y)
                {
                    Debug.Log("next level");
                    Application.LoadLevel(loadLevel);
                }
            } else
            {
                if (balloon.targetHeight <= trans.position.y)
                {
                    Debug.Log("next level");
                    Application.LoadLevel(loadLevel);
                }
            }
        }
    }
}
