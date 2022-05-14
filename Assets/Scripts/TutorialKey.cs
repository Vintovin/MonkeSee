using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialKey : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Sprite[] ButtonStates;

    [SerializeField] private int SwitchFrame;

    [SerializeField] private SpriteRenderer Sp;

    private int Cur_Frame = 0;
    private int count = 0;
    void Start()
    {
        
    }

    void ChangeKey()
    {
        Cur_Frame = Cur_Frame+1;
        if(Cur_Frame > ButtonStates.Length-1){
            Cur_Frame = 0;
        }
        Sp.sprite = ButtonStates[Cur_Frame];
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        count = count+1;
        if(count == SwitchFrame)
        {
            count = 0;
            ChangeKey();
        }
    }
}
