using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlrMovement : MonoBehaviour
{

    public Collider2D coll;

    private Rigidbody2D body;
    [SerializeField] private float SpeedMultiplier;
    [SerializeField] private float JumpSpeed;

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private LayerMask wallLayer;

    [SerializeField] private float JumpDebounce;

    //How fast the player slides down walls
    public float wallSlideSpeed = 0.1f;

    private float C_Debounce;
    private bool Hold_Jump;

    public KeyCode SprintKey;
    private int jumpCount;
    private BoxCollider2D boxcollider;

    private Vector2 Prepause;
    private bool FirstPause = false;

    private GameStateHandler GSH;

    //private bool wasJumpable = false;

    //How long to prevent horizontal movement after the player wall jumps
    public float wallJumpMoveStopTime = 0.25f;
    float wallJumpMoveStopTimeRemaining;

    private float BaseSpeed = SpeedMultiplier;
    private float CurrentSpeed = BaseSpeed;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        boxcollider = GetComponent<BoxCollider2D>();
        C_Debounce = 0;

        GameObject Handlers = GameObject.FindGameObjectWithTag("GameController");
        GSH = Handlers.GetComponent<GameStateHandler>();
    }

    // Start is called before the first frame update

    void Start()
    {
        coll = GetComponent<Collider2D>();
        
    }

    
    private bool isGrounded()

    {

        RaycastHit2D raycastHit = Physics2D.BoxCast(boxcollider.bounds.center, boxcollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }


    private bool isJumpable()
    {
        if (body.velocity.y <= 0)
        {
            //coll.sharedMaterial.friction = statFriction;
            return (true);

        }
        else
        {
            //coll.sharedMaterial.friction = dynFriction;
            return (false);
        }
    }

    private bool onWall()

    {
        //Check for wall right
        RaycastHit2D raycastHitRight = Physics2D.BoxCast(boxcollider.bounds.center, boxcollider.bounds.size, 0, Vector2.right, 0.1f, wallLayer);

        //Check for wall on left
        RaycastHit2D raycastHitLeft = Physics2D.BoxCast(boxcollider.bounds.center, boxcollider.bounds.size, 0, Vector2.left, 0.1f, wallLayer);


        //Debug.Log(raycastHit.collider != null);
        return (raycastHitRight.collider != null || raycastHitLeft.collider != null);
    }


    private bool onWallRight()
    {
        //Check for wall right
        RaycastHit2D raycastHitRight = Physics2D.BoxCast(boxcollider.bounds.center, boxcollider.bounds.size, 0, Vector2.right, 0.1f, wallLayer);


        //Debug.Log(raycastHit.collider != null);
        return (raycastHitRight.collider != null);
    }

    private bool onWallLeft()
    {
        //Check for wall on left
        RaycastHit2D raycastHitLeft = Physics2D.BoxCast(boxcollider.bounds.center, boxcollider.bounds.size, 0, Vector2.left, 0.1f, wallLayer);


        //Debug.Log(raycastHit.collider != null);
        return (raycastHitLeft.collider != null);
    }
    
    
    
    // Update is called once per frame
    void Update()
    {
        if (GSH.Paused == false)
        {
            //Count down timer until the player can move after a wall jump
            if (wallJumpMoveStopTimeRemaining > 0.0f)
            {
                wallJumpMoveStopTimeRemaining -= Time.deltaTime;
            }


            if (FirstPause == true)
            {
                body.velocity = Prepause;
                body.gravityScale = 2.5f;
                FirstPause = false;
            }

            Vector2 velocity = body.velocity;

            //Only set x velocity when a key is input
            if ((Input.GetAxis("Horizontal") != 0.0f || isGrounded()) && wallJumpMoveStopTimeRemaining <= 0.0f)
            {
                velocity.x = Input.GetAxis("Horizontal") * SpeedMultiplier;

            }

            //wall slide
            if (onWall() && velocity.y <= 0.0f)
            {
                velocity.y = Mathf.Clamp(velocity.y, -wallSlideSpeed, 0.0f);
            }
            
            if(Input.GetKeyDown(SprintKey))
            {
                CurrentSpeed *= 2;
            }
            if(Input.GetKeyUp(SprintKey))
            {
                CurrentSpeed = BaseSpeed;
            }

            if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)))
            {
                if ((((onWall() || isJumpable()) && C_Debounce <= 0) && Hold_Jump == false) && jumpCount < 2)
                {

                    //Wall jump from left wall, jumps right away from wall
                    if (onWallLeft())
                    {
                        velocity.y = JumpSpeed;
                        velocity.x = JumpSpeed;
                        wallJumpMoveStopTimeRemaining = wallJumpMoveStopTime;

                    }
                    //Wall jump from right wall, jumps left away from wall
                    else if (onWallRight())
                    {
                        velocity.y = JumpSpeed;
                        velocity.x = -JumpSpeed;
                        wallJumpMoveStopTimeRemaining = wallJumpMoveStopTime;
                    }
                    //Jump up normally
                    else
                    {
                        velocity.y = JumpSpeed;
                    }


                    C_Debounce = JumpDebounce;
                    Hold_Jump = true;
                    jumpCount = jumpCount + 1;
                }
            }

            body.velocity = velocity;

            if ((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W)))
            {
                Hold_Jump = false;
            }

            if (isGrounded())
            {
                jumpCount = 0;
            }

            if (onWall())
            {
                jumpCount = 0;
            }


            if (C_Debounce > 0)
            {
                C_Debounce = C_Debounce - (1 * Time.deltaTime);


            }
        }
        else
        {
            //When paused Time.timeScale = 0.0f
            if (FirstPause == false)
            {
                Prepause = body.velocity;

                FirstPause = true;
            }
            body.velocity = new Vector2(0, 0);
            body.gravityScale = 0;
        }
    }


    
}