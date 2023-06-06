using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;

    public GameObject optionScreen;
    public GameObject mainMenuScreen;
    public GameObject loadingScreen;
    public GameObject creditsScreen;
    public AudioMixer SFXSlider;
    public AudioMixer MusicSlider;

    float loadTimer;

    // Awake is called before Start
    void Awake()
    {
        instance = this;
        Time.timeScale = 1;
        loadTimer = 3;
        loadingScreen.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (loadingScreen.activeSelf == true)
        {
            loadTimer -= Time.deltaTime;
            if (loadTimer <= 0)
            {
                loadingScreen.SetActive(false);
            }
        }
    }

}
