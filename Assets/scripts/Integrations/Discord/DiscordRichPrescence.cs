#if !PLATFORM_WEBGL
#if !PLATFORM_ANDROID

using System;
using System.Collections;
using System.Collections.Generic;
using Discord;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DiscordRichPrescence : MonoBehaviour
{
    private Discord.Discord discord;

    private bool prevInBalloon;
    private string prevSceneName;
    private int prevCoins;

    private player player;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
        Debug.Log("Discord starting");
        discord = new Discord.Discord(974531282597969981, (UInt64)Discord.CreateFlags.NoRequireDiscord);
        var activityManager = discord.GetActivityManager();
        var activity = new Discord.Activity
        {
            State = "Navigating Menus",
            Assets = new ActivityAssets
            {
                LargeImage = "pilottrans",
                LargeText = "Starlight Postal"
            }
        };
        activityManager.UpdateActivity(activity, (res) =>
        {
            if (res == Discord.Result.Ok)
            {
                Debug.Log("Everything is fine!");
            }
        });
    }

    private void FixedUpdate()
    {
        if (player == null)
        {
            player = GameObject.FindObjectOfType<player>();
            return;
        }

        if (GameObject.FindObjectOfType<SaveFileManager>().saveData.coins != prevCoins)
        {
            prevCoins = GameObject.FindObjectOfType<SaveFileManager>().saveData.coins;
            
            UpdateRpi();
        }

        if (player.inBalloon != prevInBalloon || SceneManager.GetActiveScene().name != prevSceneName)
        {
            prevInBalloon = player.inBalloon;
            prevSceneName = SceneManager.GetActiveScene().name;

            UpdateRpi();
        }
    }
    
    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        UpdateRpi();
    }

    private void UpdateRpi()
    {
        string state = "???";
                    
        //Determine state
        if (player != null && player.inBalloon)
        {
            state = "Flying above ";
        }
        else
        {
            state = "Roaming ";
        }

        switch (SceneManager.GetActiveScene().name)
        {
            case "level 1":
                state += "Autumn Foothills";
                break;
            case "level 2":
                state += "Crystal Caverns";
                break;
            case "level 3":
                state += "Windswept Chalet";
                break;
            case "Main Menu":
                state = "Navigating Menus";
                break;
            case "Credits":
                state = "Watching Credits";
                break;
            default:
                state = "Has broken the game :)";
                break;
        }

        string details;
        switch (SceneManager.GetActiveScene().name)
        {
            case "level 1":
            case "level 2":
            case "level 3":
                //details should be coins
                var coins = GameObject.FindObjectOfType<SaveFileManager>().saveData.coins;
                details = "Collected " + coins + " stars";
                break;
            default:
                details = null;
                break;
        }

        string smallImage;
        string smallImageText;
        switch (SceneManager.GetActiveScene().name)
        {
            case "level 1":
                smallImage = "leaf";
                smallImageText = "Level 1";
                break;
            case "level 2":
                smallImage = "sparkle";
                smallImageText = "Level 2";
                break;
            case "level 3":
                smallImage = "snow";
                smallImageText = "Level 3";
                break;
            default:
                smallImage = "logo";
                smallImageText = "Starlight Postal";
                break;
        }
        
        var activityManager = discord.GetActivityManager();
        var activity = new Discord.Activity
        {
            State = state,
            Details = details,
            Assets = new ActivityAssets
            {
                LargeImage = "pilottrans",
                LargeText = "Starlight Postal",
                SmallImage = smallImage,
                SmallText = smallImageText
            }
        };
        activityManager.UpdateActivity(activity, (res) =>
        {
            if (res == Discord.Result.Ok)
            {
                Debug.Log("Everything is fine!");
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        discord.RunCallbacks();
    }
    
}

#endif
#endif