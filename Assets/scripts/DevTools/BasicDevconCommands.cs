using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IngameDebugConsole; // Make sure you import the console

public class BasicDevconCommands : MonoBehaviour {
    
    [ConsoleMethod("quit", "closes the game forcefully")] // command name and help text
    public static void QuitGame() { // The method must be static
        // Command code here
        Debug.Log("Goodbye!");
        Application.Quit();
    }

}
