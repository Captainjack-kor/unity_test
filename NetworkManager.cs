using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    void Awake()
    {
        Screen.SetResolution(960, 540, false);
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster() => PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 6 }, null);
    
    public override void OnJoinedRoom() { }
}

/*

using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
  public Text StatusText;
  public InputField NickNameInput;
  public GameObject Cube;
  public string gameVersion = "1.0";


  void Awake()
  {
      PhotonNetwork.AutomaticallySyncScene = true;
  }

  void Start()
  {
      PhotonNetwork.GameVersion = this.gameVersion;
      PhotonNetwork.ConnectUsingSettings();
  }

  void Connect() => PhotonNetwork.ConnectUsingSettings();

  override void OnConnectedToMaster()
  {
      PhotonNetwork.JoinRandomRoom();
  }

  override void OnJoinedRoom()
  {
      PhotonNetwork.Instantiate("Player3", Cube.transform.position, Quaternion.identity);
  }

  override void OnJoinRandomFailed(short returnCode, string message)
  {
      this.CreateRoom();
  }

  void CreateRoom()
  {
      PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
  }
}

*/
