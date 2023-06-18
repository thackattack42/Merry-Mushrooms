using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour
{
    public AudioSource MusicSource;
    public AudioClip MainMenuMusic;
    public AudioClip GameplayMusic;
    public AudioClip BossMusic;
    bool switchingMusic;
    bool hardSwitch;
    bool bossIsAlive;
    float fadeoutSpeed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        PlayMusic();
    }

    // Update is called once per frame
    void Update()
    {
        if (hardSwitch)
        {
            MusicSource.Stop();
            hardSwitch = false;
        }
        if (!MusicSource.isPlaying)
            PlayMusic();
        if (switchingMusic)
        {
            MusicSource.volume -= fadeoutSpeed * Time.deltaTime;
            if (MusicSource.volume <= 0.001)
            {
                MusicSource.Stop();
                MusicSource.volume = 1f;
                switchingMusic = false;
            }
        }
    }

    void PlayMusic()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
        {
            MusicSource.PlayOneShot(MainMenuMusic);
        }
        else
        {
            if (bossIsAlive)
                MusicSource.PlayOneShot(BossMusic);
            else
                MusicSource.PlayOneShot(GameplayMusic);
        }
    }
    public void BossState(bool state)
    {
        switchingMusic = true;
        bossIsAlive = state;
    }

    public void HardSwitchMusic()
    {
        hardSwitch = true;
    }
}
