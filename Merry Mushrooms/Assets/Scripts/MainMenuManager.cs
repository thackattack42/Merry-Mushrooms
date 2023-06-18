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
    public GameObject Player;
    GameObject UI;
    public buttonFunctions buttons;
    public bool playerInScene;

    float loadTimer;

    // Awake is called before Start
    void Awake()
    {
        instance = this;
        Time.timeScale = 1;
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            UI = GameObject.FindGameObjectWithTag("UI");
            Player.SetActive(false);
            UI.SetActive(false);
            playerInScene = true;
        }
    }
}
