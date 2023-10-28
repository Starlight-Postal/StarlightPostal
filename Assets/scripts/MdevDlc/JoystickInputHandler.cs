using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoystickInputHandler : MonoBehaviour
{
    [SerializeField] private KeyCode coinButton = KeyCode.JoystickButton6;
    [SerializeField] private KeyCode interactButton = KeyCode.JoystickButton4;
    [SerializeField] private KeyCode anchorButton = KeyCode.JoystickButton5;
    [SerializeField] private KeyCode pauseButton = KeyCode.JoystickButton7;
    [SerializeField] private KeyCode reelButton = KeyCode.JoystickButton0;

    private player m_player;
    private PauseMenuBehaviour m_pause;

    private float prevHorz, prevVert;
    private bool prevReel = false;
    
    // Start is called before the first frame update
    void Start()
    {
        m_player = FindObjectOfType<player>();
        m_pause = FindObjectOfType<PauseMenuBehaviour>();

        prevHorz = 0.0f;
        prevVert = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float horz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        if (m_pause && m_pause.inMenu)
        {
            
        }
        else
        {
            if (!m_player)
            {
                Debug.LogError("Player script invalid!");
            }

            if (horz != prevHorz)
            {
                gameObject.BroadcastMessage(m_player.inBalloon ? "OnLean" : "OnMove", horz);
            }

            if (m_player.inBalloon && vert != prevVert)
            {
                gameObject.BroadcastMessage("OnBurnVent", vert);
            }
            else
            {
                if (vert < -0.5f)
                {
                    gameObject.BroadcastMessage("OnPlatformDrop");
                }
            }

            bool reel = Input.GetKey(reelButton);
            if (reel != prevReel)
            {
                gameObject.BroadcastMessage("OnReel", reel ? 1.0f : 0.0f);
            }
            prevReel = reel;

            if (Input.GetKeyDown(interactButton))
            {
                gameObject.BroadcastMessage(m_player.inBalloon ? "OnLeaveBalloon" : "OnInterract");
            }

            if (Input.GetKeyDown(anchorButton))
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
        
        if (Input.GetKeyDown(pauseButton))
        {
            gameObject.BroadcastMessage("OnPauseGame");
        }

        prevHorz = horz;
        prevVert = vert;
    }
}
