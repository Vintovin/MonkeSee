using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private int DefaultHealth;
    [SerializeField] private GameObject HealthBar;

    private RectTransform HealthTrans;
    [SerializeField] private Image HealthImage;

    [HideInInspector] public int maxHealth;
    public float currentHealth;

    private void Awake()
    {
        currentHealth = DefaultHealth;
        maxHealth = DefaultHealth;
        HealthTrans = HealthBar.GetComponent<RectTransform>();
        HealthImage = HealthBar.GetComponent<Image>();
    }

    void Start()
    {
        
    }

    void Die()
    {
        SceneManager.LoadScene("Menu");
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth < maxHealth)
        {
            float healthPerc = currentHealth / maxHealth;
            HealthTrans.localScale = new Vector3(healthPerc, HealthTrans.localScale.y, HealthTrans.localScale.z);

            if ((healthPerc*100) >50)
            {
                HealthImage.color = new Color(1-((healthPerc-0.5f)*2), 1,0,1);
            }else if ((healthPerc * 100) >0)
            {
                HealthImage.color = new Color(1, (healthPerc*2), 0, 1);
            }

        }

        if (currentHealth <=0 )
        {
            Die();
        }

        //currentHealth = currentHealth - (10 * Time.deltaTime);

    }
}
