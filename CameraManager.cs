using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraManager : MonoBehaviour
{
    public GameObject target; // 카메라가 따라갈 대상.
    public float moveSpeed; // 카메라가 따라갈 속도
    private Vector3 targetPosition; // 대상의 현재 위치 값

    // private void Awake() {
    //   instance = this;
    // }

    // public Transform NetworkManager;
    // public GameObject _player;

    void Awake()
    {   
      // Debug.Log(target.gameObject);
      // target = Resources.Load<GameObject>("Player");
      // Debug.Log(target.transform.position.x);
      // Debug.Log(target.transform.position.y);

    }


    void Start() 
    {
      // GameObject _player = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
      // var test = NetworkManager.GetComponent<NetworkManager>().Spawn(); // 함수는 아니니까 
      // GameObject test = NetworkManager.GetComponent<GameObject>(); // 함수는 아니니까 
      // Debug.Log(test);
    }

    // Update is called once per frame
    void Update()
    {
      // Debug.Log(NetworkManager);
      
      // Debug.Log(target.transform.position.x);
      // Debug.Log(target.transform.position.y);

      // float x = GameObJect.Find("Player").GetComponent<NetworkManager>().init_Player_x;
      // Debug.Log(x);

      // target = NetworkManager.instance.player;
      // Debug.Log(NetworkManager.instance.player.transform.position);
      // Debug.Log(target);
      if(NetworkManager.instance.player != null) {
        targetPosition.Set(NetworkManager.instance.player.transform.position.x, NetworkManager.instance.player.transform.position.y, this.transform.position.z); //! this 생략가능 
        this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime); // 1초에 moveSpeed만큼 이동
      }	
      
      // if(target.gameObject != null) {
      //     targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z); //! this 생략가능 
      //     this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime); // 1초에 moveSpeed만큼 이동
      // }
    }
}


