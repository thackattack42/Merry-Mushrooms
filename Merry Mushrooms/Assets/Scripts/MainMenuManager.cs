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
    GameObject Player;
    GameObject UI;
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

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayAgain()
    {
        if (playerInScene)
        {
            Player.SetActive(true);
            UI.SetActive(true);
            gameManager.instance.buttons.LoadingScreen.SetActive(false);
        }
    }
}
