using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStateHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public bool Paused = false;

    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private KeyCode PauseKeybind;
    [SerializeField] private Canvas SettingsPanel;
    [SerializeField] private Canvas MainPanel;
    [SerializeField] private Button ResumeButton;
    [SerializeField] private Button SettingsButton;
    [SerializeField] private Button QuitButton;
    [SerializeField] private Button ReturnSettingsButton;

    [SerializeField] private AudioSource Audio;
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Text MusicText;

    private bool PauseDB = false;
    private float CDB = 0;

    void Start()
    {
        
    }

    private void Awake()
    {
        ResumeButton.onClick.AddListener(delegate { ResumeGame(); });
        SettingsButton.onClick.AddListener(delegate { OpenSettings(); });
        ReturnSettingsButton.onClick.AddListener(delegate { CloseSettings(); });
        QuitButton.onClick.AddListener(delegate { Quit(); });

        MusicSlider.onValueChanged.AddListener(delegate { ChangeMusic(); });
    }

    void pause()
    {
        if (PauseDB == false)
        {
            Paused = !Paused;
            PauseMenu.SetActive(Paused);
            CDB = 0.2f;
            PauseDB = true;

        }
    }

    private void Quit()
    {
        SceneManager.LoadScene("Menu");
    }

    void ChangeMusic()
    {
        Audio.volume = (MusicSlider.value) / 100;
        MusicText.text = MusicSlider.value.ToString();
    }


    void OpenSettings()
    {
        SettingsPanel.gameObject.SetActive(true);
        MainPanel.gameObject.SetActive(false);

    }
    void CloseSettings()
    {
        SettingsPanel.gameObject.SetActive(false);
        MainPanel.gameObject.SetActive(true);

    }


    void ResumeGame()
    {
        pause();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(PauseKeybind) && PauseDB == false)
        {
            pause();
        }


        if (PauseDB == true)
        {
            CDB -= 1 * Time.deltaTime;
            if (CDB <= 0)
            {
                PauseDB = false;
            }
        }
    }
}
