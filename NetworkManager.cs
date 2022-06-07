// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public InputField NickNameInput;
    public GameObject DisconnectPanel;
    public GameObject RespawnPanel;

    void Awake()
    {
        Screen.SetResolution(1200, 700, false);
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 30;
        // PhotonNetwork.SendRate = 60;
        /*
        Defines how many times per second the PhotonHandler should send data, if any is queued. Default: 30.

        This value defines how often PUN will call the low level PhotonPeer to put queued outgoing messages into a datagram to be sent. This is implemented in the PhotonHandler component, which integrates PUN into the Unity game loop. The PhotonHandler.MaxDatagrams value defines how many datagrams can be sent in one iteration.

        This value does not affect how often updates are written by PhotonViews. That is controlled by the SerializationRate. To avoid send-delays for PhotonView updates, PUN will also send data at the end of frames that wrote data in OnPhotonSerializeView, so sending may actually be more frequent than the SendRate.

        Messages queued due to RPCs and RaiseEvent, will be sent with at least SendRate frequency. They are included, when OnPhotonSerialize wrote updates and triggers early sending.

        Setting this value does not adjust the SerializationRate anymore (as of PUN 2.24).

        Sending less often will aggregate messages in datagrams, which avoids overhead on the network. It is also important to not push too many datagrams per frame. Three to five seem to be the sweet spot.

        Keep your target platform in mind: mobile networks are usually slower. WiFi is slower with more variance and bursts of loss.

        A low framerate (as in Update calls) will affect sending of messages.
        */
        // PhotonNetwork.SerializationRate = 30;

        /*
        Defines how many times per second OnPhotonSerialize should be called on PhotonViews for controlled objects.

        This value defines how often PUN will call OnPhotonSerialize on controlled network objects. This is implemented in the PhotonHandler component, which integrates PUN into the Unity game loop.

        The updates written in OnPhotonSerialize will be queued temporarily and sent in the next LateUpdate, so a high SerializationRate also causes more sends. The idea is to keep the delay short during which written updates are queued.

        Calling RPCs will not trigger a send.

        A low framerate will affect how frequent updates are written and how "on time" they are.

        A lower rate takes up less performance but the receiving side needs to interpolate longer times between updates.
        */
    }
    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 6 }, null);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
        DisconnectPanel.SetActive(false);
        // Spawn();
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape) && PhotonNetwork.IsConnected) {
            PhotonNetwork.Disconnect();
        }
    }

    public void Spawn()
    {
        // PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
        // PhotonNetwork.Instantiate("Player", new Vector3(Random.Range(-6f, 19f), 4, 0), Quaternion.identity);
        // PhotonNetwork.Instantiate("Player", new Vector3(Random.Range(-60f, 190f), Random.Range(-10f, 30f), 0), Quaternion.identity);
        RespawnPanel.SetActive(false);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        DisconnectPanel.SetActive(true);
        RespawnPanel.SetActive(false);    
    }
}
