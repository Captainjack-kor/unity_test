using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using agora_gaming_rtc;

public class TestAgora : MonoBehaviour
{
    public string AppID;
    public string ChannelName;

    VideoSurface myView;
    VideoSurface remoteView;
    IRtcEngine mRtcEngine;

    void Awake()
    {
        // SetupUI();
    }

    void Start()
    {
        // SetupAgora();
    }

    void SetupUI() 
    {
        GameObject go = GameObject.Find("MyView");
        myView = go.AddComponent<VideoSurface>();

        go = GameObject.Find("LeaveButton");
    }

    void Join()
    {}

    void Leave()
    {}

    void OnJoinChannelSuccessHandler(string channelName, uint uid, int elapsed)
    {}

    void OnLeaveChannelHandler(RtcStats stats)
    {}
    
    void OnUserJoined(uint uid, int elapsed)
    {}

    void OnUserOffline(uint uid, USER_OFFLINE_REASON reason)
    {}

    void OnApplicationQuit()
    {}
}
