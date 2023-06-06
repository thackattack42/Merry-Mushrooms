using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class buttonFunctions : MonoBehaviour
{
    public AudioSource SFXTestSource;
    public AudioSource UIAudio;
    public AudioClip MenuButtonClick;
    public AudioClip MenuSelection;
    [SerializeField] public AudioClip SFXTest;

    public void resume()
    {
        UIAudio.PlayOneShot(MenuButtonClick);
        gameManager.instance.UnpausedState();
    }
    public void restart()
    {
        UIAudio.PlayOneShot(MenuButtonClick);
        gameManager.instance.UnpausedState();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void quit()
    {
        UIAudio.PlayOneShot(MenuButtonClick);
        Application.Quit(); 
    }
    public void respawn()
    {
        UIAudio.PlayOneShot(MenuButtonClick);
        gameManager.instance.UnpausedState();
        gameManager.instance.playerScript.Spawn();
    }
    public void startGame()
    {
        UIAudio.PlayOneShot(MenuButtonClick);
        SceneManager.LoadScene(1);
    }
    public void options()
    {
        UIAudio.PlayOneShot(MenuButtonClick);
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
        {
            MainMenuManager.instance.mainMenuScreen.SetActive(false);
            MainMenuManager.instance.optionScreen.SetActive(true);
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
        UIAudio.PlayOneShot(MenuButtonClick);
        MainMenuManager.instance.mainMenuScreen.SetActive(false);
        MainMenuManager.instance.creditsScreen.SetActive(true);
    }
    public void mainMenu()
    {
        UIAudio.PlayOneShot(MenuButtonClick);
        SceneManager.LoadScene(0);
    }
    public void PlaySelection()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || 
            Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            UIAudio.PlayOneShot(MenuSelection);
    }

    //Option buttons
    public void optionsBack()
    {
        UIAudio.PlayOneShot(MenuButtonClick);
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
        UIAudio.PlayOneShot(MenuButtonClick);
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
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
            MainMenuManager.instance.SFXSlider.SetFloat("SFXParam", Mathf.Log10(val) * 20);
        else
            gameManager.instance.SFXSlider.SetFloat("SFXParam", Mathf.Log10(val) * 20);
    }
    public void SFXVolTest()
    {
        SFXTestSource.PlayOneShot(SFXTest);
    }
    public void MusicVol(float val)
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
            MainMenuManager.instance.MusicSlider.SetFloat("MusicParam", Mathf.Log10(val) * 20);
        else
            gameManager.instance.MusicSlider.SetFloat("MusicParam", Mathf.Log10(val) * 20);
    }

    //Credits Buttons
    public void creditsBack()
    {
        UIAudio.PlayOneShot(MenuButtonClick);
        MainMenuManager.instance.creditsScreen.SetActive(false);
        MainMenuManager.instance.mainMenuScreen.SetActive(true);
    }

    public void SetPlayerBow()
    {
        gameManager.instance.playerScript.playerWeapon = PlayerController.weapon.Bow;
    }
    public void SetPlayerSword()
    {
        gameManager.instance.playerScript.playerWeapon = PlayerController.weapon.Sword;
    }
    public void SetPlayerStaff()
    {
        gameManager.instance.playerScript.playerWeapon = PlayerController.weapon.Staff;
    }
}
