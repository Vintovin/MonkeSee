using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealthBar : MonoBehaviour
{

    [SerializeField] private GameObject HealthBar;
    [SerializeField] private GameObject AI;
    
    private SpriteRenderer colourPanel;
    private Transform Block;
    private AIHandler aiHandler;
    private Transform aiTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        colourPanel = HealthBar.GetComponent<SpriteRenderer>();
        Block = HealthBar.GetComponent<Transform>();
        
        aiHandler = AI.GetComponent<AIHandler>();
        aiTransform = AI.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
        float HealthPerc = aiHandler.AIHealth / aiHandler.Def_AIHealth;
        if(HealthPerc < 0 ){
            HealthPerc = 0.0f;
        }
        Block.localScale = new Vector3(HealthPerc, 1,1);
        Vector3 NewPos = new Vector3(0 - ((HealthPerc - 1) / 2), 0, 0);
        if (aiTransform.localScale.x >0)
        {
            NewPos.x *= -1;
        }
        else
        {
            NewPos.x *= 1;
        }
        Block.localPosition = NewPos;
    }
}
