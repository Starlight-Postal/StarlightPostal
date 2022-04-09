using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Conversation : MonoBehaviour, Interractable
{

    [SerializeField] private GameObject visualCue;
	[SerializeField] private string[] script;
    private bool inMenu;

    private int scriptIndex;
    private bool encountered;
    private bool waitingForReady;
    
    private VisualElement root;
    private Button chatButton;
    private Label chatScript;

    ////  \/ STARTUP FUNCTIONS \/ ////

    void Start()
    {
        inMenu = false;
        scriptIndex = 0;
        encountered = false;
        waitingForReady = false;
    }

    void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        chatButton = root.Q<Button>("chatButton");
        chatScript = root.Q<Label>("chatLabel");

        chatButton.RegisterCallback<ClickEvent>(evt => { OnChatButtonClick(); });
        
        root.visible = false;
    }

    ////  /\ STARTUP FUCNTIONS /\  ////

    ////  \/ ON UPDATES \/  ////

    void FixedUpdate()
    {
        if (waitingForReady)
        {
            if (ReadyToAdvanceTo(scriptIndex)) {
                TurnOnDisplay();
                waitingForReady = false;
            }
        } else
        {
            if (AutoAdvanceConditionMet(scriptIndex))
            {
                ForceAdvanceScript();
            }
        }
    }

    ////  /\ ON UPDATES /\  ////

    ////  \/ EXTERNAL TRIGGERS AND PLAYER INPUT \/  ////

    public void OnPlayerInterract()
    {

    }

    public void OnConversationAdvance()
    {
        
    }

    private void OnChatButtonClick()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            visualCue.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            visualCue.SetActive(false);
        }
    }

    ////  /\ EXTERNAL TRIGGERS AND PLAYER INPUT /\  ////

    ////  \/ SCRIPT AND UI UPDATES \/  ////

	private void StartScript()
    {
        scriptIndex = 0;
        TurnOnDisplay();
        encountered = true;
	}

    private void AdvanceScript()
    {
        if (CanPlayerContinue(scriptIndex))
        {
            ForceAdvanceScript();
        }
    }

    private void ForceAdvanceScript()
    {
        scriptIndex++;

        if (scriptIndex >= script.Length)
        {
            TurnOffDisplay();
            return;
        }

        if (!ReadyToAdvanceTo(scriptIndex)) {
            TurnOffDisplay();
            waitingForReady = true;
        } else
        {
            TurnOnDisplay();
        }
    }

    private void TurnOnDisplay()
    {
		chatScript.text = script[scriptIndex];
        root.visible = true;
        inMenu = true;
        chatButton.visible = true;
    }

    private void TurnOffDisplay()
    {
        root.visible = false;
        inMenu = false;
        chatButton.visible = false;
    }

    ////  /\ SCRIPT AND UI UPDATES /\  ////
    
    ////  \/ OTHER VARIOUS HELPER FUNCTIONS \/  ////

    ////  /\ OTHER VARIOUS HELPER FUNCTIONS /\  ////

    ////  \/ BEHAVIOUR OVERRIDES \/  ////

    /**
     * Dictates if the "next" button should appear and if 
     * the advance text keybind are allowed to work
     */
    public virtual bool CanPlayerContinue(int index)
    {
        return true;
    }

    /**
     * Dictates if the player is allowed to move for this line
     *  >>> NOT IMPLEMENTED FOR NOW
     */
    public virtual bool CanPlayerMove(int index)
    {
        return true;
    }

    /**
     * Returning true will advance to the next line
     */
    public virtual bool AutoAdvanceConditionMet(int index)
    {
        return false;
    }

    /**
     * Returning false will prevent the next line from appearing until true is returned
     * (Ui will be hidden) (will be called every FixedUpdate until true)
     */
    public virtual bool ReadyToAdvanceTo(int index)
    {
        return true;
    }

    ////  /\ BEHAVIOUR OVERRIDES /\  ////

}