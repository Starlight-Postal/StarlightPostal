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

    private player player;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
        Debug.Log("Discord starting");
        discord = new Discord.Discord(974531282597969981, (UInt64)Discord.CreateFlags.Default);
        var activityManager = discord.GetActivityManager();
        var activity = new Discord.Activity
        {
            State = "Navigating Menus",
            Assets = new ActivityAssets
            {
                LargeImage = "starlight_logo",
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
        
        var activityManager = discord.GetActivityManager();
        var activity = new Discord.Activity
        {
            State = state,
            Details = "Out for delivery",
            Assets = new ActivityAssets
            {
                LargeImage = "starlight_logo",
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

    // Update is called once per frame
    void Update()
    {
        discord.RunCallbacks();
    }
    
}
