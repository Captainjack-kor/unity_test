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
    private BoxCollider2D boxCollider;
    private Animator animator;
    private Vector3 vector;

    public LayerMask layerMask; // 어떤 레이어와 충돌했는지 판단 (통과가 불가능한 레이어를 설정해줌) 
    public float speed;
    public float runSpeed;
    private float applyRunSpeed;
    private bool applyRunFlag = false;
    public int walkCount;
    private int currentWalkCount;
    private bool canMove = true;

    void Start()
    {
        // 컴포넌트 불러와서 저장
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

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
            // float axis = Input.GetAxisRaw("Horizontal");
            // RB.velocity = new Vector3(4 * axis, RB.velocity.y, 0);

            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime * 48, Input.GetAxisRaw("Vertical") * Time.deltaTime * 48, 0));

            // vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            // if(vector.x != 0) 
            // {
            //     vector.y = 0;
            // }
            // animator.SetFloat("DirX", vector.x);
            // animator.SetFloat("DirY", vector.y);

            // RaycastHit2D hit;
            // Vector2 start = transform.position; // A지점 - 캐릭터 현재 위치 값
            // Vector2 end = start + new Vector2(vector.x * Time.deltaTime * 48, vector.y * Time.deltaTime * 48); // B지점 - 캐릭터가 이동하고자 하는 위치 값

        } 
        // else if ((transform.position - curPos).sqrMagnitude >= 100) transform.position = curPos;
        // else transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 10);
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


