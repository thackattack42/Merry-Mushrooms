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
    public void credits()
    {
        MainMenuManager.instance.creditsScreen.SetActive(true);
        MainMenuManager.instance.mainMenuScreen.SetActive(false);
    }
    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }

    //Option buttons
    public void optionsBack()
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
    public void minimapRotTottle(bool toggle)
    {
        if (toggle)
        {
            gameManager.instance.playerHUD.minimapCamRot.enabled = false; //disables the rotation lock
            gameManager.instance.playerHUD.minimapCam.transform.rotation = gameManager.instance.player.transform.rotation;
            gameManager.instance.playerHUD.minimapCam.transform.Rotate(90, 0, 0);
        }
        else
            gameManager.instance.playerHUD.minimapCamRot.enabled = true; //enables the rotation lock

    }
    public void SFXVol(float val)
    {
        gameManager.instance.SFXSlider.SetFloat("SFXParam", Mathf.Log10(val) * 20);
    }
    public void SFXVolTest()
    {
        gameManager.instance.dashCooldownFinishPing.PlayOneShot(gameManager.instance.playerHUD.dashCooldownFinishPing, 1);
    }
    public void MusicVol(float val)
    {
        gameManager.instance.MusicSlider.SetFloat("MusicParam", Mathf.Log10(val) * 20);
    }

    //Credits Buttons
    public void creditsBack()
    {
        MainMenuManager.instance.creditsScreen.SetActive(false);
        MainMenuManager.instance.mainMenuScreen.SetActive(true);
    }
}
