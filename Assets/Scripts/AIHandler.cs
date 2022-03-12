using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHandler : MonoBehaviour
{

    [SerializeField] private GameObject AI;
    [SerializeField] private Rigidbody2D Bod;
    [SerializeField] private float Walkspeed;
    [SerializeField] private Transform Trans;
    [SerializeField] private Transform ATrans;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Collider2D BodyCol;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float HitDebounce;
    [SerializeField] private GameObject Player;
    [SerializeField] private float attackDamage;
    [SerializeField] private SpriteRenderer Sprite;
    [SerializeField] private Sprite NormalSpriteImage;
    [SerializeField] private Sprite AttackSpriteImage;
    [SerializeField] public float JumpFactor;
    [SerializeField] public float Def_AIHealth;

    public float AIHealth;

    private bool IsPatroling = true;
    private bool AwaitingFlip;

    private PlayerHealth Health;
    private Vector2 APosi;
    private Rigidbody2D PlayerTrans;

    private bool CanAttack = true;
    private float C_Db = 0;
    private float ColourTime;
    private float ColourCount = 0;

    private bool flipDb = false;
    private float flip_Def = 0.2f;
    private float flip_C = 0f;

    private bool FirstPause = false;
    private GameStateHandler GSH;

    private void Awake()
    {
        AwaitingFlip = false;
        Health = Player.GetComponent<PlayerHealth>();
        ColourTime = HitDebounce / 10;
        PlayerTrans = Player.GetComponent<Rigidbody2D>();
        AIHealth = Def_AIHealth;
        GameObject Handlers = GameObject.FindGameObjectWithTag("GameController");
        GSH = Handlers.GetComponent<GameStateHandler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (IsPatroling)
        {
            Vector2 Posi = new Vector2(Trans.position.x , Trans.position.y);

            AwaitingFlip = !Physics2D.OverlapCircle(Posi, 0.1f,groundLayer);
        }
        APosi = new Vector2(ATrans.position.x, ATrans.position.y);
    }

    void patrol()
    {
        if (AwaitingFlip == true || BodyCol.IsTouchingLayers(wallLayer))
        {
            if (flipDb == false)
            {
                flip();
            }
            
        }



        Bod.velocity = new Vector2(Walkspeed, Bod.velocity.y);
    }

    void flip()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        Walkspeed *= -1;
        AwaitingFlip = false;
        flipDb = true;
        flip_C = flip_Def;
    }

    void Attack()
    {
        if(CanAttack == true)
        {
            float HC = Health.currentHealth - attackDamage;
            float finalH;
            if (HC> 0)
            {
                finalH = HC;
            }
            else
            {
                finalH = 0;
            }
            Health.currentHealth = finalH;
            CanAttack = false;
            C_Db = HitDebounce;

            ColourCount = ColourTime;
            Sprite.sprite = AttackSpriteImage;
            PlayerTrans.velocity = new Vector2(0f, JumpFactor);
        }
        else
        {
            if (ColourCount > 0)
            {
                ColourCount -= (1 * Time.deltaTime);
            }
            else
            {
                Sprite.sprite = NormalSpriteImage;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (GSH.Paused == false)
        {
            if (FirstPause == true)
            {
                Bod.gravityScale = 2.5f;
                FirstPause = false;
            }
            if (IsPatroling == true)
            {
                patrol();
            }


            if (Physics2D.OverlapCircle(APosi, 0.4f, playerLayer) == true)
            {

                Attack();
            }

            if (C_Db > 0)
            {
                C_Db -= (1 * Time.deltaTime);
            }
            else
            {
                CanAttack = true;
            }

            if (flip_C > 0)
            {
                flip_C -= (1 * Time.deltaTime);
            }
            else
            {
                flipDb = false;
            }

            if (AIHealth <= 0)
            {
                AI.SetActive(false);
            }
        }
        else
        {
            if (FirstPause == false)
            {
                Bod.gravityScale = 0;
                Bod.velocity = new Vector2(0, 0);
                FirstPause = true;
            }
            
        }
    }
}
