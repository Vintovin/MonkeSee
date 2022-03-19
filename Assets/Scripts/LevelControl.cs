using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    [SerializeField]private Collider2D EndZone;
    [SerializeField] private GameStateHandler GameHandler;
    [SerializeField] private LayerMask PlayerLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EndZone.IsTouchingLayers(PlayerLayer))
        {
            GameHandler.EndGame();
        }
    }
}
