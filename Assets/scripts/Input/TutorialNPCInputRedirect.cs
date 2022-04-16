using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialNPCInputRedirect : MonoBehaviour
{

    GameObject npcs;

    void Start()
    {
        npcs = GameObject.Find("npcs");
    }

    void OnLeaveBalloon()
    {
        npcs.BroadcastMessage("OnLeaveBalloon");
    }

}
