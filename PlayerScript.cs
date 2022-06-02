using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
public class PlayerScript : MonoBehaviourPunCallbacks
{
     //, IPunObservable

    Rigidbody2D rigid;
    Animator anim;
    public PhotonView PV;
    public Text NickNameText;
    public SpriteRenderer SpriteRenderer; 
    private BoxCollider2D boxCollider;
    private Vector3 vector;
    public LayerMask layerMask; // 어떤 레이어와 충돌했는지 판단 (통과가 불가능한 레이어를 설정해줌) 
    public float Speed;
    float h;
    float v;
    bool isHorizonMove;
    // public float runSpeed;
    private float applyRunSpeed;
    private bool applyRunFlag = false;
    public int walkCount;
    private int currentWalkCount;
    private bool canMove = true;
    private Animator animator;

    // public float speed = 10.0f;
    // private Transform tr;

    void Start()
    {
        // CameraWork _cameraWork = this.gameObject.GetComponent<CameraWork>();
        // if (_cameraWork != null)
        // {
        //     if (photonView.IsMine)
        //     {
        //         _cameraWork.OnStartFollowing();
        //     }
        // }
        // else
        // {
        //     Debug.LogError("<Color=Red><a>Missing</a></Color> CameraWork Component on playerPrefab.", this);
        // }

        // 컴포넌트 불러와서 저장
        boxCollider = GetComponent<BoxCollider2D>();
        // anim = GetComponent<Animator>();
        // animator = GetComponent<Animator>();
        // tr = GetComponent<Transform>();
    }

    
    // IEnumerator MoveCoroutine() {
    //     while(Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0) {
    //         if(Input.GetKey(KeyCode.LeftShift)) {
    //             applyRunSpeed = runSpeed;
    //             applyRunFlag = true;
    //         }
    //         else {
    //             applyRunSpeed = 0;
    //             applyRunFlag = false;
    //         }
    //         vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);
    //         if(vector.x != 0) 
    //         {
    //             vector.y = 0;
    //         }
    //         animator.SetFloat("DirX", vector.x);
    //         animator.SetFloat("DirY", vector.y);


    //         animator.SetBool("Walking", true);

    //         while(currentWalkCount < walkCount) {
    //             if(vector.x != 0) 
    //             {
    //                 transform.Translate(vector.x * (speed + applyRunSpeed), 0, 0);
    //             }
    //             else if(vector.y != 0) 
    //             {
    //                 transform.Translate(0, vector.y * (speed + applyRunSpeed), 0);
    //             }
    //             if(applyRunFlag) {
    //                 currentWalkCount++;
    //             }

    //             currentWalkCount++;
    //             yield return new WaitForSeconds(0.10f);
    //         }
    //         currentWalkCount = 0;
    //     }
    //     animator.SetBool("Walking", false);
    //     canMove = true;
    // }


    IEnumerator MoveCoroutine() {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");

        //Check Horizontal Move
        if(hDown) {
            isHorizonMove = true;
        } else if (vDown) {
            isHorizonMove = false;
        } else if (hUp || vUp) {
            isHorizonMove = h != 0;
        } 

        //Animation
        if(anim.GetInteger("hAxisRaw") != h) {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
        } else if(anim.GetInteger("vAxisRaw") != v) {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        } else {
            anim.SetBool("isChange", false);
        }

        yield return new WaitForSeconds(0.01f);
    }
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        NickNameText.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName;
        NickNameText.color = PV.IsMine ? Color.green : Color.red;
    }
    

    void Update()
    {

        if(PV.IsMine)
        {
            StartCoroutine(MoveCoroutine());




            //TODO: 밑에 보류
            /*
            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);
            if(vector.x != 0) 
            {
                vector.y = 0;
            }
            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);
            // animator.SetBool("Walking", true);
            if(vector.x != 0) 
            {
                transform.Translate(vector.x, 0, 0);
            }
            else if(vector.y != 0) 
            {
                transform.Translate(0, vector.y, 0);
            } 

            if(canMove) {
            if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) 
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());
            }  
            */
        }
        // animator.SetBool("Walking", false);
        // else {

        // }
        // else if ((transform.position - curPos).sqrMagnitude >= 100) transform.position = curPos;
        // else transform.position = Vector3.Lerp(transform.position, curPos, Time.deltaTime * 10);
    }

    void FixedUpdate() 
    {
        //Move
        Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);

        transform.Translate(h, v, 0);
        // rigid.velocity = moveVec * Speed;
    }

    // [PunRPC]
    // void moveTest(float axis) 
    // {
    //     SpriteRenderer.transform = axis == -1;
    // }

    // public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    // {
    //     if (stream.IsWriting)
    //     {
    //         stream.SendNext(transform.position);
    //     }
    //     else
    //     {
    //         transform.position = (Vector3)stream.ReceiveNext();
    //     }
    // }
}


