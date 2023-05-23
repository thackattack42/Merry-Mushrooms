using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;

    public GameObject optionScreen;
    public GameObject mainMenuScreen;

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
