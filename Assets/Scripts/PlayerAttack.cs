using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] public float AttackDamage;
    //[SerializeField] public GameObject CurrentWeapon;
    [SerializeField] private float AttackDebounce;
    [SerializeField]private Transform PlayerTrans;
    [SerializeField] private LayerMask AIlayer;
    [SerializeField] private float JumpEffect;
    [SerializeField] private KeyCode AttackKey;
    [SerializeField] private bool MouseAttack;
    [SerializeField] private WorldSwitch WS;

    private float C_Db =  0;
    private bool CanAttack = true;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Attack()
    {

        Collider2D[] Collision = Physics2D.OverlapCircleAll(PlayerTrans.position, 1f, AIlayer);
        
        if (CanAttack == true)
        {
            foreach (Collider2D enemy in Collision)
            {
                //
                //thing = Physics2D.OverlapCircle(ATrans.position, 0.4f,AIlayer);
                //
                AIHandler AI = enemy.GetComponent<AIHandler>();
                Rigidbody2D AITrans = enemy.GetComponent<Rigidbody2D>();

                AI.AIHealth -= AttackDamage;
                if (AI.AIHealth <=0)
                {
                    if (WS.Cur_Charge < WS.Def_Charge)
                    {
                        WS.Cur_Charge += 1;
                        WS.UpdateUI();
                    }
                    
                }
                AITrans.velocity = new Vector2(0f, JumpEffect);
                CanAttack = false;
                C_Db = AttackDebounce;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(AttackKey) || (MouseAttack == true &&Input.GetMouseButtonDown(0)) )
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
    }
}
