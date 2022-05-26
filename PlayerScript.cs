using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
public class PlayerScript : MonoBehaviourPunCallbacks
{
     //, IPunObservable

    public PhotonView PV;
    public Text NickNameText;
    public SpriteRenderer SpriteRenderer;
    public Animator AN;
    private Vector3 curPos;

    void Awake()
    {
        NickNameText.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName;
        NickNameText.color = PV.IsMine ? Color.green : Color.red;
    }

    void Update()
    {
        // <- -> 이동 위아래까지 (Moving Object를 뜯어야할듯 (위치도 저장필요))
        if(PV.IsMine)
        {
            float axis = Input.GetAxisRaw("Horizontal");
            // RB.velocity = new Vector3(4 * axis, RB.velocity.y, 0);
        } 
        else if ((transform.position - curPos).sqrMagnitude >= 100) transform.position = curPos;
        else transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 10);
    }
    
    // public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    // {
    //     if (stream.IsWriting)
    //     {
    //         stream.SendNext(transform.position);
    //     }
    //     else
    //     {
    //         curPos = (Vector3)stream.ReceiveNext();
    //     }
    // }

}


