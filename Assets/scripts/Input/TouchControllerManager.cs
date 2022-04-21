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
        playerTouchControls.SetActive(false);
        balloonTouchControls.SetActive(false);
    }
    
    #if PLATFORM_ANDROID
    void FixedUpdate()
    {
        playerTouchControls.SetActive(!play.inBalloon);
        balloonTouchControls.SetActive(play.inBalloon);
    }
    #endif
    
}
