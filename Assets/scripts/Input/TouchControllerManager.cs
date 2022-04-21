using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControllerManager : MonoBehaviour
{

    public GameObject playerTouchControls;
    public GameObject balloonTouchControls;

    private player play;

    void Start()
    {
        play = GameObject.FindObjectOfType<player>();
    }
    
    void FixedUpdate()
    {
        #if PLATFORM_ANDROID
        playerTouchControls.SetActive(!play.inBalloon);
        balloonTouchControls.SetActive(play.inBalloon);
        #endif
    }
    
}
