using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpriteSwitch : MonoBehaviour
{
    public Sprite Terraformed;
    public Sprite Jungle;
    private bool WorldState;
    private float C_Debounce = 0;
    public WorldSwitch Mana;

    void Start()
    {
        Mana = FindObjectOfType<WorldSwitch>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if ((C_Debounce <= 0) && (Mana.Cur_Charge > 0))
            {
              WorldState = !WorldState;
              C_Debounce = 1;

              if (WorldState == true)
              {
                  this.gameObject.GetComponent<SpriteRenderer>().sprite = Terraformed;
              }
              else
              {
                  this.gameObject.GetComponent<SpriteRenderer>().sprite = Jungle;
              }
            }
        }
        if (C_Debounce > 0)
        {
            C_Debounce = C_Debounce - (1 * Time.deltaTime);
        }
    }
}
