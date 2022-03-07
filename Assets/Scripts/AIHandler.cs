using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHandler : MonoBehaviour
{

    [SerializeField] private Rigidbody2D Bod;
    [SerializeField] private float Walkspeed;
    [SerializeField] private Transform Trans;
    [SerializeField] private LayerMask groundLayer;
    private bool IsPatroling = true;
    private bool AwaitingFlip;

    

    private void Awake()
    {
        AwaitingFlip = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (IsPatroling)
        {
            Vector3 Posi = new Vector2(Trans.position.x , Trans.position.y);
            AwaitingFlip = !Physics2D.OverlapCircle(Posi, 0.1f,groundLayer);
        }
    }

    void patrol()
    {
        if (AwaitingFlip == true)
        {
            flip();
        }
        Bod.velocity = new Vector2(Walkspeed, Bod.velocity.y);
    }

    void flip()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        Walkspeed *= -1;
        AwaitingFlip = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPatroling == true)
        {
            patrol();
        }
    }
}
