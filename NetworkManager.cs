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

		public GameObject init_Player;
		float init_Player_x;
		float init_Player_y;

		public GameObject target; // 카메라가 따라갈 대상.
    public float moveSpeed; // 카메라가 따라갈 속도
    private Vector3 targetPosition; // 대상의 현재 위치 값


    void Awake()
    {
        // Screen.SetResolution(1200, 700, false, 60);
        Screen.SetResolution(1200, 700, false);
        // PhotonNetwork.SendRate = 60;
        // PhotonNetwork.SerializationRate = 30;
    }
    public void Connect() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 6 }, null);
    }

    public override void OnJoinedRoom()
    {
        // PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
        DisconnectPanel.SetActive(false);
        Spawn();
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape) && PhotonNetwork.IsConnected) {
            PhotonNetwork.Disconnect();
        }

				// if(init_Player != null) {
				// 	// Debug.Log(test.transform.position);
				// 	init_Player_x = init_Player.transform.position.x;
				// 	init_Player_y = init_Player.transform.position.y;
				// 	targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z); //! this 생략가능 
        //   this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime); // 1초에 moveSpeed만큼 이동
				// }

				// if(target.gameObject != null) {
        //   targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z); //! this 생략가능 
        //   this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime); // 1초에 moveSpeed만큼 이동
        // }

    }

    public void Spawn()
    {
        init_Player = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity); // as GameObject;
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
