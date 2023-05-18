using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttonFunctions : MonoBehaviour
{
    
    public void resume()
    {
        gameManager.instance.UnpausedState();
    }
    public void restart()
    {
        gameManager.instance.UnpausedState();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void quit()
    {
        Application.Quit(); 
    }
    public void respawn()
    {
        gameManager.instance.UnpausedState();
        gameManager.instance.playerScript.Spawn();
    }
    public void startGame()
    {
        SceneManager.LoadScene(1);
    }
    public void options()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
        {
            MainMenuManager.instance.optionScreen.SetActive(true);
            MainMenuManager.instance.mainMenuScreen.SetActive(false);
        }
        else
        {
            gameManager.instance.activeMenu.SetActive(false);
            gameManager.instance.activeMenu = gameManager.instance.optionsMenu;
            gameManager.instance.activeMenu.SetActive(true);

        }
    }
    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }

    //Option buttons
    public void back()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
        {
            MainMenuManager.instance.optionScreen.SetActive(false);
            MainMenuManager.instance.mainMenuScreen.SetActive(true);
        }
        else
        {
            gameManager.instance.activeMenu.SetActive(false);
            gameManager.instance.activeMenu = gameManager.instance.pauseMenu;
            gameManager.instance.activeMenu.SetActive(true);
        }

    }
    public void apply()
    {

    }
}
