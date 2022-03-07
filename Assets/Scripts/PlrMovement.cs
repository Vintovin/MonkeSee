using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlrMovement : MonoBehaviour
{

    private Rigidbody2D body;
    [SerializeField] private float SpeedMultiplier;
    [SerializeField] private float JumpSpeed;

    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private LayerMask wallLayer;

    [SerializeField] private float JumpDebounce;

    private float C_Debounce;
    private bool Hold_Jump;

    private int jumpCount;
    private BoxCollider2D boxcollider;


    

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        boxcollider = GetComponent<BoxCollider2D>();
        C_Debounce = 0;
    }

    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        body.velocity = new Vector2(Input.GetAxis("Horizontal")*SpeedMultiplier,body.velocity.y);


        if((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) ))
        {
            if((((onWall() || isJumpable()) && C_Debounce <= 0)&& Hold_Jump == false) && jumpCount < 2 ){
                body.velocity = new Vector2(body.velocity.x,JumpSpeed);
                C_Debounce = JumpDebounce;
                Hold_Jump = true;
                jumpCount = jumpCount+1;
            }
            
        }

        if(Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W))
        {
            Hold_Jump = false;
        }


        if(isGrounded())
        {
            jumpCount = 0;
        }

        if(onWall())
        {
            jumpCount = 0;
        }


        if(C_Debounce > 0)
        {
            C_Debounce = C_Debounce-(1*Time.deltaTime);
            
            
        }
    }

    private bool isGrounded()
    
    {

        RaycastHit2D raycastHit = Physics2D.BoxCast(boxcollider.bounds.center,boxcollider.bounds.size,0,Vector2.down,0.1f, groundLayer );
        return raycastHit.collider != null;
    }

    private bool isJumpable()
    {
        if(body.velocity.y <=0 ){
            return(true);

        }else{
            return(false);
        }
        
    }
    private bool onWall()
    
    {

        RaycastHit2D raycastHit = Physics2D.BoxCast(boxcollider.bounds.center,boxcollider.bounds.size,0,new Vector2(transform.localScale.x,0),0.1f, wallLayer );
        return raycastHit.collider != null;
    }

}
