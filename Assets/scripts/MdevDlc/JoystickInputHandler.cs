using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoystickInputHandler : MonoBehaviour
{

    [SerializeField] private float horizontal, vertical;

    private player m_player;
    private PauseMenuBehaviour m_pause;
    
    // Start is called before the first frame update
    void Start()
    {
        m_player = FindObjectOfType<player>();
        m_pause = FindObjectOfType<PauseMenuBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (m_pause && m_pause.inMenu)
        {
            
        }
        else
        {
            if (!m_player)
            {
                Debug.LogError("Player script invalid!");
            }
            
            gameObject.BroadcastMessage(m_player.inBalloon ? "OnLean" : "OnMove", horizontal);

            if (m_player.inBalloon)
            {
                gameObject.BroadcastMessage("OnBurnVent", vertical);
            }
            else
            {
                if (vertical < -0.5f)
                {
                    gameObject.BroadcastMessage("OnPlatformDrop");
                }
            }
        
            //TODO: reel

            if (Input.GetKeyDown(KeyCode.JoystickButton2))
            {
                gameObject.BroadcastMessage(m_player.inBalloon ? "OnLeaveBalloon" : "OnInterract");
            }

            if (Input.GetKeyDown(KeyCode.JoystickButton3))
            {
                if (m_player.inBalloon)
                {
                    gameObject.BroadcastMessage("OnAnchor");
                }
            }

            if (Input.GetKeyDown(KeyCode.JoystickButton4))
            {
            }
        }

        if (Input.GetKeyDown(KeyCode.JoystickButton0))
        {
        }
        
        if (Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            gameObject.BroadcastMessage("OnPauseGame");
        }
    }
}
