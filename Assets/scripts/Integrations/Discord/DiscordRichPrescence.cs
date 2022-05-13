using System;
using System.Collections;
using System.Collections.Generic;
using Discord;
using UnityEngine;

public class DiscordRichPrescence : MonoBehaviour
{
    private Discord.Discord discord;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Discord starting");
        discord = new Discord.Discord(974531282597969981, (UInt64)Discord.CreateFlags.Default);
        var activityManager = discord.GetActivityManager();
        var activity = new Discord.Activity
        {
            State = "Flying above Windswept Chalet",
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

    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        discord.RunCallbacks();
    }
    
}
