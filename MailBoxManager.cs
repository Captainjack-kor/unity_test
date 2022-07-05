using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using WebSocketSharp;
using TMPro;
using System;
using System.Threading;

public class Data 
{
    public string type;
    public string text;
    public string user_id;
}

// [Serializable]
public class MyAnswer
{
    public string user_id;
    public string text;
    public string comment_content;
}

public class MailBoxManager : MonoBehaviour
{
    private Thread thread;
    public Button sendBtn;
    public TMP_InputField input;

    public TMP_Text answerLog;

    private WebSocket m_WebSocket;
    public GameObject AIPanel; 
    public string saved = "답변내용";

    // public Queue<Action> queue_list = new Queue<Action>();

    public bool mailbox_popup = false;
    
    // public bool data_check = false;
    void Start()
    {
        AIPanel = GameObject.Find("AIPanel");
        // this.AIPanel = GameObject.Find("AIPanel").GetComponent<Text>();

        // input = GameObject.Find("AIInputField");
        // sendBtn = GameObject.Find("AIBtn");
    }

    void SendButtonOnClicked()
    {
        m_WebSocket = new WebSocket("ws://139.150.73.129:9998");
        m_WebSocket.Connect();

        if (input.text.Equals("")) { 
            return; 
        }
        
        Data data = new Data(); 
        data.type = "message"; 
        data.text = input.text;
        data.user_id = "nickname"; 
    
        string str = JsonUtility.ToJson(data); 

        // Debug.Log(myTable);

        if (m_WebSocket == null)
        {
            return;
        }

        m_WebSocket.Send(str);

        // Thread thread = new Thread(() => {
        //     queue_list.Enqueue(() => {
        //         m_WebSocket.OnMessage += (sender, e) =>
        //         {
        //             // Debug.Log($"{((WebSocket)sender).Url}에서 + 데이터 : {e.Data}가 옴. {e}");
        //             // Debug.Log("mesage received from " + ((WebSocket)sender).Url + "Data: " + e.Data);
        //             MyAnswer returnValue = JsonUtility.FromJson<MyAnswer>(e.Data);
        //             // Debug.Log("Data: " + returnValue.comment_content);

        //             answerLog.text = returnValue.comment_content;
        //         };
        //     });
        // });

        // Update if(bool == true){ 
        //     refresh bool = false
        // };

        m_WebSocket.OnMessage += (sender, e) =>
        {
            // Debug.Log($"{((WebSocket)sender).Url}에서 + 데이터 : {e.Data}가 옴. {e}");
            // Debug.Log("mesage received from " + ((WebSocket)sender).Url + "Data: " + e.Data);
            MyAnswer returnValue = JsonUtility.FromJson<MyAnswer>(e.Data);
            this.saved = returnValue.comment_content;
            // this.answerLog.text = returnValue.comment_content;
            // Debug.Log("Data: " + returnValue.comment_content);

            // answerLog.text = returnValue.comment_content;
        };

        // Debug.Log(saved);
        input.text = "";
        // m_WebSocket.Close();
    }


    void Update() 
    {
        // while (queue_list.Count > 0) {
        //     queue_list.Dequeue().Invoke();
        // }

        // MyAnswer returnValue = JsonUtility.FromJson<MyAnswer>(saved);
        if(saved == "") {
            saved = "답변 내용";
        }
        answerLog.text = saved;
    }
}
