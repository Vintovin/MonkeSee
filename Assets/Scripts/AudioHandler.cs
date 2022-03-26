using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private AudioClip BossMusic;
    [SerializeField] private AudioClip BackgroundMusic;
    [SerializeField] private AudioSource ActiveAudio;
    [SerializeField] private Transform BossMusicTransition;
    [SerializeField] private Transform PlayerTrans;
    [SerializeField] private GameStateHandler GameHandler;


    void Start()
    {
        
    }

    void ChangeAudio(AudioClip New)
    {
        float PrevVolume = ActiveAudio.volume;
        for (float i = ActiveAudio.volume; i > 0; i -= 0.01f)
        {
            ActiveAudio.volume = i;
        }
        ActiveAudio.clip = New;
        ActiveAudio.Play();
        ActiveAudio.loop = true;
        for (float i = ActiveAudio.volume; i < PrevVolume; i += 0.01f)
        {
            ActiveAudio.volume = i;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(PlayerTrans.position.x >= BossMusicTransition.position.x)
        {
            
            if (ActiveAudio.clip != BossMusic)
            {
                
                ChangeAudio(BossMusic);
            }
        }
        else
        {
           
            if(ActiveAudio.clip != BackgroundMusic)
            {
                
                ChangeAudio(BackgroundMusic);
            }
        }
    }
}
