using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinHandling : MonoBehaviour
{
    [SerializeField] private GameObject Coin;
    [SerializeField] private GameObject plr;
    [SerializeField] private Sprite[] Slist;
    [SerializeField] private WorldSwitch WSHandle;
    [SerializeField] private PlayerHealth PHHandle;

    ////////////////////////////////////////////
    public enum CoinTypes {Health,Mana};
    public CoinTypes CT;
    public int HealthIncriment;
    public int ManaIncriment;
    ////////////////////////////////////////////
    ////////////////////////////////////////////
    private SpriteRenderer SR;
    ////////////////////////////////////////////
    ////////////////////////////////////////////
    private int CoinWait = 20;
    private float CF = 0;
    private int SpriteCount = 0;
    ////////////////////////////////////////////
    ////////////////////////////////////////////
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        SpriteCount = Random.Range(0,Slist.Length);
    }
    ///////////////////////////////////////////
    void ChangeSprite()
    {
        SpriteCount = SpriteCount+ 1;
        if(SpriteCount == Slist.Length){
            SpriteCount = 0;
        }
        SR.sprite = Slist[SpriteCount]; 
    }
    ///////////////////////////////////////////
    void Add()
    {
        if(CT == CoinTypes.Health)
        {
            if(PHHandle.currentHealth != PHHandle.maxHealth)
            {
                float NewHealth = PHHandle.currentHealth+HealthIncriment;
                if(NewHealth > PHHandle.maxHealth)
                {
                    PHHandle.currentHealth = PHHandle.maxHealth;
                }else{
                    PHHandle.currentHealth = NewHealth;
                }
                Coin.SetActive(false);
            }
        }else if(CT == CoinTypes.Mana)
        {
            if(WSHandle.Cur_Charge != WSHandle.Def_Charge)
            {
                float NewMana = WSHandle.Cur_Charge + ManaIncriment;
                if(NewMana > WSHandle.Def_Charge)
                {
                    WSHandle.Cur_Charge = WSHandle.Def_Charge;
                }else
                {
                    WSHandle.Cur_Charge = NewMana;
                    WSHandle.UpdateUI();
                    
                }
                Coin.SetActive(false);
            }
        }
    }

    void OnTriggerEnter2D (Collider2D col) {
        var hitObject = col.gameObject;
        if(col.gameObject == plr)
        {
            Add();
        }
        
    }


    ///////////////////////////////////////////
    void Update()
    {
        CF = CF+1;
        if(CF == CoinWait){
            CF = 0;
            ChangeSprite();
            
        }

        
        
    }
}
