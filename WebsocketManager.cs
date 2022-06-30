using UnityEngine;
using WebSocketSharp;
using System.Collections.Generic;
using System.Collections;
using Newtonsoft.Json.Linq;
using System;
// using System.Threading;
// using System.Text;
// using System.Collections;

public class Data 
{
  public string type;
  public string text;
  public string user_id;
}

namespace Example
{
  public class WebsocketManager : MonoBehaviour
  {
    private WebSocket m_WebSocket;

    void Start()
    {
        m_WebSocket = new WebSocket("ws://localhost:8080");
        m_WebSocket.Connect();

        m_WebSocket.OnMessage += (sender, e) =>
        {
            // Debug.Log($"{((WebSocket)sender).Url}에서 + 데이터 : {e.Data}가 옴. {e}");
            Debug.Log("mesage received from " + ((WebSocket)sender).Url + "Data: " + e.Data);
            // Console.WriteLine(e.data);
        };
    }

    void Update()
    {
        // var myTable = new Dictionary<string, string>();
        // myTable.Add("type", "message");
        // myTable.Add("text", "아하하하하하");
        // myTable.Add("user_id", "nickname");
        Data data = new Data(); 
        data.type = "message"; 
        data.text = "건강관리 추천해주세요";
        data.user_id = "nickname"; 
 
        string str = JsonUtility.ToJson(data); 

        // Debug.Log(myTable);

        if (m_WebSocket == null)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
          // Debug.Log();
          // string json = JsonUtility.ToJson(myObject);
          m_WebSocket.Send(str);
        }
    }
  }
}