using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsHandler : MonoBehaviour
{
    [SerializeField] private AudioSource Audio;
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Button BackButton;
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject SettingsMenu;
    [SerializeField] private Text MusicText;
    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        MusicSlider.onValueChanged.AddListener(delegate { ChangeMusic(); });
        BackButton.onClick.AddListener(delegate { ReturnClick(); });
    }

    void ReturnClick()
    {
        MainMenu.SetActive(true);
        SettingsMenu.SetActive(false);
    }

    void ChangeMusic()
    {
        Audio.volume = (MusicSlider.value)/100;
        MusicText.text = MusicSlider.value.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
