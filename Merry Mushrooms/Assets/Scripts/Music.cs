using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour
{
    public AudioSource MusicSource;
    public AudioClip MainMenuMusic;
    public AudioClip GameplayMusic;
    public AudioClip Boss1Music;
    public AudioClip Boss2Music;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
        {
            MusicSource.PlayOneShot(MainMenuMusic);
        }
        else
        {
            MusicSource.PlayOneShot(GameplayMusic);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
