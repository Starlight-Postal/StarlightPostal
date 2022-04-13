using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Conversation : Interractable
{

    [SerializeField] private GameObject visualCue;
	[SerializeField] public string[] script;
    private bool inMenu;

    protected int scriptIndex;
    protected bool isTalking;
    private bool canTalk = true;
    private bool waitingForReady;
    private bool isInRange;
    
    private VisualElement root;
    private Button chatButton;
    private Label chatScript;

    ////  \/ STARTUP FUNCTIONS \/ ////

    protected void Start()
    {
        inMenu = false;
        scriptIndex = 0;
        isTalking = false;
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

    protected void FixedUpdate()
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

    public override void OnPlayerInterract()
    {
        GetComponent<UiButtonSfx>().OnUiButtonClick();
        if (isTalking)
        {
            AdvanceScript();
        } else
        {
            StartScript();
        }
    }

    private void OnChatButtonClick()
    {
        AdvanceScript();        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            isInRange = true;
            visualCue.SetActive(!isTalking);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            isInRange = false;
            visualCue.SetActive(false);
            if (isTalking && ResetOnPlayerLeave(scriptIndex))
            {
                EndConversation();
            }
        }
    }

    ////  /\ EXTERNAL TRIGGERS AND PLAYER INPUT /\  ////

    ////  \/ SCRIPT AND UI UPDATES \/  ////

	private void StartScript()
    {
        if (!canTalk) { return; }
        OnConversationStart();
        scriptIndex = 0;
        TurnOnDisplay();
        isTalking = true;
        visualCue.SetActive(false);
	}

    protected void AdvanceScript()
    {
        if (!isTalking && canTalk)
        {
            StartScript();
            return;
        }
        if (CanPlayerContinue(scriptIndex) && !waitingForReady)
        {
            ForceAdvanceScript();
        }
    }

    private void ForceAdvanceScript()
    {
        scriptIndex++;

        if (scriptIndex >= script.Length)
        {
            EndConversation();
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

    private void EndConversation()
    {
        isTalking = false;
        TurnOffDisplay();
        if (!ResetOnComplete())
        {
            canTalk = false;
        }
        visualCue.SetActive(canTalk && isInRange);
        OnConversationEnd();
    }

    private void TurnOnDisplay()
    {
		chatScript.text = script[scriptIndex];
        inMenu = true;
        if (script[scriptIndex] != "")
        {
            root.visible = true;
            chatButton.visible = CanPlayerContinue(scriptIndex);
        }
        OnConversationLineUpdate(scriptIndex);
    }

    private void TurnOffDisplay()
    {
        root.visible = false;
        inMenu = false;
        chatButton.visible = false;
    }

    ////  /\ SCRIPT AND UI UPDATES /\  ////

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
     * If true, will reset conversation on player leaving range
     */
    public virtual bool ResetOnPlayerLeave(int index)
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

    /**
     * Should this conversation reset itself?
     */
    public virtual bool ResetOnComplete() {
        return true;
    }

    /**
     * Called when the conversation is started
     * (Before first line)
     */
    public virtual void OnConversationStart()
    {

    }

    /**
     * Called when the conversation is finished
     */
    public virtual void OnConversationEnd()
    {

    }

    /**
     * Called every time the current line updates
     */
    public virtual void OnConversationLineUpdate(int index)
    {

    }

    ////  /\ BEHAVIOUR OVERRIDES /\  ////

}
