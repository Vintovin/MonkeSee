using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject World1;
    [SerializeField] private GameObject World2;
    [SerializeField] private float SwitchDebounce;
    [SerializeField] private GameObject Camera;
    private bool SecondWorld = false;

    private float C_Debounce = 0;
    private Camera Cam;




    void Awake()
    {
        Cam = Camera.GetComponent<Camera>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (C_Debounce <= 0 )
            {
                SecondWorld = !SecondWorld;
                World1.SetActive(!SecondWorld);
                World2.SetActive(SecondWorld);
                if(SecondWorld == true){
                    Cam.backgroundColor = new Color(103,0,142);
                }else{
                    Cam.backgroundColor = new Color(174,194,226);
                }
                
                C_Debounce = SwitchDebounce;
            }
        }

        if (C_Debounce > 0)
        {
            C_Debounce = C_Debounce - (1 * Time.deltaTime);
            Debug.Log(C_Debounce);

        }
    }
}
