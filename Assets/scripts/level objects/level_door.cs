using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class level_door : MonoBehaviour
{
    public string loadLevel;
    public bool upDoor = true;
    public bool sideways = false;

    public GameObject balloonObj;
    public balloon balloon;
    public Transform trans;
    public Transform balloonTrans;
    // Start is called before the first frame update
    void Start()
    {
        balloonObj = GameObject.Find("Center");
        balloon = GameObject.Find("balloon").GetComponent<balloon>();
        trans = gameObject.GetComponent<Transform>();
        balloonTrans = balloonObj.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject == balloonObj)
        {
            if (sideways)
            {
                if (upDoor)
                {
                    if (balloonTrans.position.x >= trans.position.x)
                    {
                        Debug.Log("next level");
                        Application.LoadLevel(loadLevel);
                    }
                }
                else
                {
                    if (balloonTrans.position.x <= trans.position.x)
                    {
                        Debug.Log("next level");
                        Application.LoadLevel(loadLevel);
                    }
                }
            } else
            {
                if (upDoor)
                {
                    if (balloon.targetHeight >= trans.position.y)
                    {
                        Debug.Log("next level");
                        Application.LoadLevel(loadLevel);
                    }
                }
                else
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
}
