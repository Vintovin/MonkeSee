using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]public GameObject World1;
    [SerializeField]public GameObject World2;
    private bool SecondWorld = false;


    void Awake()
    {
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (SecondWorld == false) 
            {
                World1.SetActive(false);
                World2.SetActive(true);
                SecondWorld = true;
            }
            else
            {
                World1.SetActive(true);
                World2.SetActive(false);
                SecondWorld = false;
            }
        }
    }
}
