using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WorldSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject World1;
    [SerializeField] private GameObject World2;
    [SerializeField] private float SwitchDebounce;
    [SerializeField] private GameObject Camera;
    [SerializeField] private RectTransform ChargeBar;
    [SerializeField] private GameObject SwitchIcon;
    public float Def_Charge;

    public float Cur_Charge;

    private bool SecondWorld = false;

    private float C_Debounce = 0;
    private Camera Cam;
    private Image Icon;


    void Awake()
    {
        Cam = Camera.GetComponent<Camera>();
        Cur_Charge = Def_Charge;
        Icon = SwitchIcon.GetComponent<Image>();
    }
    void Start()
    {
        
    }

    public void UpdateUI()
    {
        float perc = Cur_Charge / Def_Charge;
        ChargeBar.localScale = new Vector3(perc, ChargeBar.localScale.y, ChargeBar.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && Cur_Charge >0)
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
                Cur_Charge -= 1;
                float perc = Cur_Charge / Def_Charge;
                ChargeBar.localScale = new Vector3(perc, ChargeBar.localScale.y,ChargeBar.localScale.z);

                C_Debounce = SwitchDebounce;
                
            }
        }
        
        if (C_Debounce > 0)
        {
            C_Debounce = C_Debounce - (1 * Time.deltaTime);
            Icon.color = new Color32(71,188,255,30);
        }else{
            Icon.color = new Color32(71,188,255,255);
            
        }
    }
}
