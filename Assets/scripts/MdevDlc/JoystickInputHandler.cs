using UnityEngine;

public class JoystickInputHandler : MonoBehaviour
{
    [SerializeField] private KeyCode coinButton = KeyCode.JoystickButton9;
    [SerializeField] private KeyCode interactButton = KeyCode.JoystickButton0;
    [SerializeField] private KeyCode anchorButton = KeyCode.JoystickButton2;
    [SerializeField] private KeyCode pauseButton = KeyCode.JoystickButton8;
    [SerializeField] private KeyCode reelButton = KeyCode.JoystickButton3;

    private player m_player;
    private PauseMenuBehaviour m_pause;
    private bool inMenu = false;

    private float prevHorz, prevVert;
    private bool prevReel = false;

    public void SetInMenu(bool inMenu)
    {
        this.inMenu = inMenu;
    }
    
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

        if (inMenu || (m_pause && m_pause.inMenu))
        {
            if (horz > 0.5 && prevHorz <= 0.5)
            {
                gameObject.BroadcastMessage("OnUiRight");
            }
            else if (horz < -0.5 && prevHorz >= -0.5)
            {
                gameObject.BroadcastMessage("OnUiLeft");
            }

            if (vert > 0.5 && prevVert <= 0.5)
            {
                gameObject.BroadcastMessage("OnUiUp");
            }
            else if (vert < -0.5 && prevVert >= -0.5)
            {
                gameObject.BroadcastMessage("OnUiDown");
            }

            if (Input.GetKeyDown(interactButton))
            {
                gameObject.BroadcastMessage("OnUiSelect");
            }

            if (Input.GetKeyDown(anchorButton))
            {
                gameObject.BroadcastMessage("OnUiBack");
            }
        }
        else
        {
            if (m_player && horz != prevHorz)
            {
                gameObject.BroadcastMessage(m_player.inBalloon ? "OnLean" : "OnMove", horz);
            }

            if (m_player && m_player.inBalloon && vert != prevVert)
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

            if (m_player && Input.GetKeyDown(interactButton))
            {
                gameObject.BroadcastMessage(m_player.inBalloon ? "OnLeaveBalloon" : "OnInterract");
            }

            if (Input.GetKeyDown(anchorButton))
            {
                if (m_player && m_player.inBalloon)
                {
                    gameObject.BroadcastMessage("OnAnchor");
                }
            }
        }

        if (Input.GetKeyDown(coinButton))
        {
            gameObject.BroadcastMessage("OnCreditInsert", 1);
        }
        
        if (Input.GetKeyDown(pauseButton))
        {
            gameObject.BroadcastMessage("OnPauseGame");
        }

        if (Input.GetKeyDown(pauseButton) || Input.GetKeyDown(interactButton) ||
            Input.GetKeyDown(anchorButton) || Input.GetKeyDown(reelButton))
        {
            gameObject.BroadcastMessage("OnAnyJoystickButtonDown");
        }

        prevHorz = horz;
        prevVert = vert;
    }
}
