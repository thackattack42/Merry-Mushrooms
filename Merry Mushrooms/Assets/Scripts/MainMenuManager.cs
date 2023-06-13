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
    public GameObject creditsScreen;
    public AudioMixer SFXSlider;
    public AudioMixer MusicSlider;

    float loadTimer;

    // Awake is called before Start
    void Awake()
    {
        instance = this;
        Time.timeScale = 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
