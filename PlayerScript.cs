using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using WebSocketSharp;
using TMPro;

public class PlayerScript : MonoBehaviourPunCallbacks
{
     //, IPunObservable
    public VariableJoystick joy;
    Rigidbody2D rigid;
    Animator anim;
    Vector3 dirVec;
    Vector3 moveVec;
    public PhotonView PV;
    public Text NickNameText;
    public SpriteRenderer SpriteRenderer; 
    private BoxCollider2D boxCollider;
    private Vector3 vector;
    public LayerMask layerMask; // 어떤 레이어와 충돌했는지 판단 (통과가 불가능한 레이어를 설정해줌) 
    public float Speed;
    GameObject scanObject;
    bool isHorizonMove;
    // public float runSpeed;
    private float applyRunSpeed;
    private bool applyRunFlag = false;
    public int walkCount;
    private int currentWalkCount;
    private bool canMove = true;
    private Animator animator;

    // public float speed = 10.0f;
    private Transform tr;
    private WebSocket m_WebSocket;
    private GameObject AIPanel;
    public bool mailbox_popup = false;
    public bool popup_result = false;
    public Image msgCount;
    private GameObject DisconnectPanel;
    float h;
    float v;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        tr = GetComponent<Transform>();
        AIPanel = GameObject.Find("AIPanel");
        DisconnectPanel = GameObject.Find("DisconnectPanel");
    }

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
        if( PV != null ){
            NickNameText.text = PV.IsMine ? PhotonNetwork.NickName : PV.Owner.NickName;
            NickNameText.color = PV.IsMine ? Color.white : Color.red;
        }
    }  
    
    void Update()
    {
      if( PV == null ){
        return;
      }

      if(PV.IsMine)
      {
        // StartCoroutine(MoveCoroutine());
        // Debug.Log(canMove);

        // float x = joy.Horizontal;
        // float z = joy.Vertical;
        // moveVec = new Vector2(x, z) * Speed * Time.deltaTime;

        // if(DisconnectPanel.transform.localScale.x == 0) {

        // }

        if(joy.stillDown) {
          h = joy.Horizontal;
          v = joy.Vertical;
          // if((h <= 0.7 && v >= 0.7) && (h >= -0.7 && v >= 0.7)) {
          //   h = 0;
          //   v = 1;
          // } else if((h >= 0.7 && v <= 0.7) && (h >= 0.7 && v >= -0.7)) {
          //   h = 1;
          //   v = 0;
          // } else if((h <= 0.7 && v <= -0.7) && (h >= -0.7 && v <= -0.7)) {
          //   h = 0;
          //   v = -1;
          // } else if((h <= -0.7 && v >= -0.7) && (h <= -0.7 && v <= 0.7)) {
          //   h = -1;
          //   v = 0;
          // }

          if(h == 1 && v == 0) {
            h = 1;
            v = 0;
          } else if(h == -1 && v == 0) {
            h = -1;
            v = 0;
          } else if(v == 1 && h == 0) {
            v = 1;
            h = 0;
          } else if(v == -1 && h == 0) {
            v = -1;
            h = 0;
          }
          // else if((h <= 0.7 && v >= 0.7) && (h >= -0.7 && v >= 0.7)) {
          //   h = 0;
          //   v = 1;
          // } else if((h >= 0.7 && v <= 0.7) && (h >= 0.7 && v >= -0.7)) {
          //   h = 1;
          //   v = 0;
          // } else if((h <= 0.7 && v <= -0.7) && (h >= -0.7 && v <= -0.7)) {
          //   h = 0;
          //   v = -1;
          // } else if((h <= -0.7 && v >= -0.7) && (h <= -0.7 && v <= 0.7)) {
          //   h = -1;
          //   v = 0;
          // }

          // Debug.Log("h는: " + h);
          // Debug.Log("v는: " + v);
        } else {
          h = Input.GetAxisRaw("Horizontal");
          v = Input.GetAxisRaw("Vertical");
        }

        //Animation
        // if (anim.GetInteger("hAxisRaw") != h)
        // {
        //   anim.SetBool("isChange", true);
        //   anim.SetInteger("hAxisRaw", (int)h);
        // }
        // else if (anim.GetInteger("vAxisRaw") != v)
        // {
        //   anim.SetBool("isChange", true);
        //   anim.SetInteger("vAxisRaw", (int)v);
        // }
        // else
        // {
        //   anim.SetBool("isChange", false);
        // }

        //Animation
        if (anim.GetInteger("hAxisRaw") != h)
        {
          anim.SetBool("isChange", true);
          anim.SetInteger("hAxisRaw", (int)h);
        }
        else if (anim.GetInteger("vAxisRaw") != v)
        {
          anim.SetBool("isChange", true);
          anim.SetInteger("vAxisRaw", (int)v);
        }
        else
        {
          anim.SetBool("isChange", false);
        }
      }
    }

    void FixedUpdate() 
    {
        //Move
        // rigid.velocity = Vector3.zero;
        // Debug.DrawRay(NetworkManager.instance.player.transform.position, new Vector3(0, -1, 0) * 3f, new Color(0,1,0));

        RaycastHit2D hit;
        // A지점에서 B지점까지 레이저를 쏘는데 무사히 도착하면 
        // hit = null


        // vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);
        // if(vector.x != 0) {
        //     transform.Translate(vector.x, 0, 0);
        // } else if(vector.y != 0) {
        //     transform.Translate(0, vector.y, 0);
        // }
        // transform.Translate(h, v, 0);
        // transform.rotation = Quaternion.Euler(0f, rotY, 0f);



        // if(canMove) {
        // if(AIPanel.transform.localScale.x == 0) {

            // TODO 2022.07.19 Desigmer : 대각선 움직임 추가
            if ((h != 0) && (v != 0))
            {
                transform.Translate(h * Mathf.Sqrt(h * h * h * h / 2) * Speed, v * Mathf.Sqrt(v * v * v * v / 2) * Speed, 0);
            }
            else if (h != 0 && v <= 0) {
                transform.Translate(h * Speed, 0, 0);
            } else if(v != 0 && h <= 0) {
                transform.Translate(0, v * Speed, 0);
            }

            // transform.Translate(h * Speed, v * Speed, 0);


                // Vector2 moveVec = isHorizonMove ? new Vector2(h, 0) : new Vector2(0, v);
                // rigid.velocity = moveVec * Speed;
        // }
        // }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if( other.tag == "MailBox" ) {
            // Debug.Log("in");
            AIPanel.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if( other.tag == "MailBox" ) {
            // Debug.Log("out");
            AIPanel.transform.localScale = new Vector3(0, 0, 0);
        }
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
