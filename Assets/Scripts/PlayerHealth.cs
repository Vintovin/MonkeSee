using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private int DefaultHealth;
    [SerializeField] private GameObject HealthBar;

    private RectTransform HealthTrans;
    [SerializeField] private Image HealthImage;

    static int maxHealth;
    static float currentHealth;

    private void Awake()
    {
        currentHealth = DefaultHealth;
        maxHealth = DefaultHealth;
        HealthTrans = HealthBar.GetComponent<RectTransform>();
        //HealthImage = HealthBar.GetComponent<Image>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth < maxHealth)
        {
            float healthPerc = currentHealth/maxHealth;
            HealthTrans.localScale = new Vector3(healthPerc,HealthTrans.localScale.y,HealthTrans.localScale.z);

            //if (healthPerc >50)
            //{
            //    HealthImage.tintColor = new Color(healthPerc * 255,255,0);
            //}else if (healthPerc >0)
            //{
            //    HealthImage.tintColor = new Color(255, healthPerc * 255, 0);
            //}

        }

        currentHealth = currentHealth - (10 * Time.deltaTime);
        
    }
}
