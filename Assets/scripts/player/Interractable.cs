using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interractable : MonoBehaviour
{
    
    /**
     * Called when the player interracts with this interractable
     */
    public virtual void OnPlayerInterract()
    {
        
    }

    /**
     * Called while finding interractables, returning false will cause search to ignore this interractable
     */
    public virtual bool CanPlayerInterract()
    {
        return true;
    }

    /**
     * ===== UNUSED =====
     * Called when the player moves within range of this interractable
     * ===== Thinking about replacing the trigger on NPCs with this -Jacob (Apr 13)
     */
    public virtual void OnPlayerEnterRange()
    {

    }

    /**
     * ===== UNUSED =====
     * Called when the player moves out of range of this interractable
     * ===== Thinking about replacing the trigger on NPCs with this -Jacob (Apr 13)
     */
    public virtual void OnPlayerLeaveRange()
    {

    }

}
