using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D body;

    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;
    public float cameraSizeWhileWalking = 3.5f; //Default: 3.5f
    public float cameraSizeWhileRunning = 4.0f; //Default: 4.0f

    private KeyCode runKey;

    public Animator anim;
    public GameObject playerCamera;
    public GameObject texture;
    private Effects effects;

    public float runSpeed = 3.0f;
    public bool changeSpeed = false;
    public bool isRunning;
    private bool zoomDone = false;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        //camera.GetComponent<Camera>().orthographicSize = 3.5f;
        runKey = GetComponent<Player>().playerControls.RunKey;

        changeSpeed = true;
        playerCamera = GameObject.FindGameObjectWithTag("CM VCam");
        effects = GetComponent<Effects>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
       
        if (Input.GetKeyDown(runKey))
        {
        RunningAnimation();
        }
        else
        {
        WalkingAnimation();
        }
        //Debug.Log("Speed:  " + runSpeed);
    }

    void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0)
        {
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }

    void WalkingAnimation()
    {      
        if (horizontal != 0 || vertical != 0)
        {
            anim.SetTrigger("PlayerWalking");
            if (Input.GetKeyDown(runKey))
            {
                changeSpeed = false;
            }
            else if (Input.GetKeyUp(runKey))
            {
                changeSpeed = false;
            }
            if (Input.GetKey(runKey))
            {
                RunningAnimation();
            }
            else
            {
                isRunning = false;
                //To Update
                

                if(zoomDone == false)
                {
                    effects.StartCoroutine("CameraFadeIn");
                    zoomDone = true;
                }

                //playerCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = cameraSizeWhileWalking;
                if (changeSpeed == false)
                {
                    runSpeed = runSpeed / 2.0f;
                    changeSpeed = true;
                }
                anim.speed = 1;
            }
        }
        else
        {
            anim.SetTrigger("PlayerNotWalking");
            anim.Play("PlayerIdle");
            
        }

        if(horizontal == -1)
        {
            texture.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if(horizontal == 1)
        {
            texture.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
    
    void RunningAnimation()
    {
        isRunning = true;
        //To Update

        if(zoomDone == true)
        {
            effects.StartCoroutine("CameraFadeOut");
            zoomDone = false;
        }

        //playerCamera.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = cameraSizeWhileRunning;
        if(changeSpeed == true)
        {
            runSpeed = runSpeed * 2.0f;
            changeSpeed = false;
        }
        anim.speed = 2;
    }
}
