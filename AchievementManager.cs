using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using Steamworks.Data;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        try
        {
            SteamClient.Init(1552250);
        }
        catch (System.Exception)
        {
            // Something went wrong - it's one of these:
            //
            //     Steam is closed?
            //     Can't find steam_api dll?
            //     Don't have permission to play app?
            //
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Unlock(string str)
    {
        //Debug.Log(1);
        try
        {
            var ach = new Achievement(str);
            ach.Trigger();
        }
        catch (System.Exception)
        {

        }
    }
}
