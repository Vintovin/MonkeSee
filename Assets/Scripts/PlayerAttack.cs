using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] public float AttackDamage;
    //[SerializeField] public GameObject CurrentWeapon;
    [SerializeField] private float AttackDebounce;
    [SerializeField] private Transform ATrans;
    [SerializeField] private LayerMask AIlayer;

    private float C_Db =  0;
    private bool CanAttack = true;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Attack()
    {

        if (Physics2D.OverlapCircle(ATrans.position, 0.4f, AIlayer) == true)
        {
            if (CanAttack == true)
            {
                //
                //thing = Physics2D.OverlapCircle(ATrans.position, 0.4f,AIlayer);
                //
                CanAttack = false;
                C_Db = AttackDebounce;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) || Input.GetMouseButtonDown(0) )
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
