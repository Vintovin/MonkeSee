using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{

    [SerializeField] private Button ExitButton;
    [SerializeField] private Button SettingsButton;
    [SerializeField] private Button PlayButton;
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject SettingsMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {

        PlayButton.onClick.AddListener(delegate { OnPlayClick(); });
        SettingsButton.onClick.AddListener(delegate { SettingsClick(); });
        ExitButton.onClick.AddListener(delegate { Application.Quit(); });
    }

    void OnPlayClick()
    {
        SceneManager.LoadScene("DemoLevel");
    }

    void SettingsClick()
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
