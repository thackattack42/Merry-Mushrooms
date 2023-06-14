using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttonFunctions : MonoBehaviour
{
    public AudioSource SFXTestSource;
    public AudioSource UIAudio;
    public AudioClip MenuButtonClick;
    public AudioClip MenuSelection;
    [SerializeField] GameObject skillTree;
    public GameObject fireButton;
    public GameObject iceButton;
    public GameObject earthButton;
    public SwordStats fireSword;
    public SwordStats iceSword;
    public SwordStats earthSword;
    public Staff_Stats fireStaff;
    public Staff_Stats iceStaff;
    public Staff_Stats earthStaff;
    public BowStats fireBow;
    public BowStats iceBow;
    public BowStats earthBow;
    public AudioClip SFXTest;
    public GameObject LoadingScreen;
    public Image LoadingBar;

    public void resume()
    {
        UIAudio.PlayOneShot(MenuButtonClick);
        gameManager.instance.UnpausedState();
    }
    public void restart()
    {
        UIAudio.PlayOneShot(MenuButtonClick);
        gameManager.instance.UnpausedState();
        StartCoroutine(LoadSceneAsync(SceneManager.GetActiveScene().buildIndex));
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
        MainMenuManager.instance.PlayAgain();
        StartCoroutine(LoadSceneAsync(1));
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
        gameManager.instance.activeMenu.SetActive(false);
        gameManager.instance.activeMenu = null;
        gameManager.instance.isPaused = false;
        gameManager.instance.reticle.SetActive(true);
        StartCoroutine(LoadSceneAsync(0));
    }
    public void PlaySelection()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || 
            Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            UIAudio.PlayOneShot(MenuSelection);
    }

    IEnumerator LoadSceneAsync(int id)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(id);
        LoadingScreen.SetActive(true);

        
        while (!op.isDone)
        {
            LoadingBar.fillAmount = Mathf.Clamp01(op.progress / 0.9f);
            yield return null;
        }
        LoadingScreen.SetActive(false);
        //if (id == 2)
        //{
        //    string sceneToLoad = SceneManager.GetSceneByBuildIndex(2).name;
        //    //SceneManager.MoveGameObjectToScene(gameManager.instance.player, SceneManager.GetSceneByBuildIndex(2));
        //    SceneManager.LoadScene(sceneToLoad);
        //}
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

    public void NextLevel()
    {
        UIAudio.PlayOneShot(MenuButtonClick);
        gameManager.instance.UnpausedState();
        StartCoroutine(LoadSceneAsync(2));
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
        //gameManager.instance.playerScript.bowMat
        gameManager.instance.playerScript.bowModel.mesh = gameManager.instance.playerScript.BowList[gameManager.instance.playerScript.selectedBow].model.GetComponent<MeshFilter>().sharedMesh;
        gameManager.instance.playerScript.bowMat.material = gameManager.instance.playerScript.BowList[gameManager.instance.playerScript.selectedBow].model.GetComponent<MeshRenderer>().sharedMaterial;
        gameManager.instance.playerScript.BowEquipped = true;
        //gameManager.instance.Bow.SetActive(true);
        gameManager.instance.UnpausedState();
    }
    public void SetPlayerSword()
    {
        gameManager.instance.playerScript.playerWeapon = PlayerController.weapon.Sword;
        //gameManager.instance.Sword.SetActive(true);
        gameManager.instance.playerScript.swordModel.mesh = gameManager.instance.playerScript.SwordList[gameManager.instance.playerScript.selectedSword].model.GetComponent<MeshFilter>().sharedMesh;
        gameManager.instance.playerScript.swordMat.material = gameManager.instance.playerScript.SwordList[gameManager.instance.playerScript.selectedSword].model.GetComponent<MeshRenderer>().sharedMaterial;
        gameManager.instance.playerScript.shieldModel.mesh = gameManager.instance.playerScript.ShieldList[0].model.GetComponent<MeshFilter>().sharedMesh;
        gameManager.instance.playerScript.shieldMat.material = gameManager.instance.playerScript.ShieldList[0].model.GetComponent<MeshRenderer>().sharedMaterial;
        
        gameManager.instance.playerScript.SwordEquipped = true;
        gameManager.instance.playerScript.ShieldEquipped = true;
        gameManager.instance.UnpausedState();
    }
    public void SetPlayerStaff()
    {
        gameManager.instance.playerScript.playerWeapon = PlayerController.weapon.Staff;
        //gameManager.instance.Staff.SetActive(true);
        gameManager.instance.playerScript.staffModel.mesh = gameManager.instance.playerScript.staffList[gameManager.instance.playerScript.selectedStaff].model.GetComponent<MeshFilter>().sharedMesh;
        gameManager.instance.playerScript.staffMat.material = gameManager.instance.playerScript.staffList[gameManager.instance.playerScript.selectedStaff].model.GetComponent<MeshRenderer>().sharedMaterial;
        gameManager.instance.playerScript.StaffEquipped = true;
        gameManager.instance.UnpausedState();
    }

    public void FireSkill()
    {
        fireButton = GameObject.FindGameObjectWithTag("Fire Button");
        if (gameManager.instance.playerScript.playerWeapon == PlayerController.weapon.Sword && gameManager.instance.playerScript.skillPoints > 0)
        {
            gameManager.instance.playerScript.SwordList.Add(fireSword);
            gameManager.instance.playerScript.skillPoints--;
            gameManager.instance.invManager.UpdateSkillPoints();
            fireButton.SetActive(false);
        }
        else if (gameManager.instance.playerScript.playerWeapon == PlayerController.weapon.Staff && gameManager.instance.playerScript.skillPoints > 0)
        {
            gameManager.instance.playerScript.staffList.Add(fireStaff);
            gameManager.instance.playerScript.skillPoints--;
            gameManager.instance.invManager.UpdateSkillPoints();
            fireButton.SetActive(false);
        }
        else if (gameManager.instance.playerScript.playerWeapon == PlayerController.weapon.Bow && gameManager.instance.playerScript.skillPoints > 0)
        {
            gameManager.instance.playerScript.BowList.Add(fireBow);
            gameManager.instance.playerScript.skillPoints--;
            gameManager.instance.invManager.UpdateSkillPoints();
            fireButton.SetActive(false);
        }
    }

    public void IceSkill()
    {
        iceButton = GameObject.FindGameObjectWithTag("Ice Button");
        if (gameManager.instance.playerScript.playerWeapon == PlayerController.weapon.Sword && gameManager.instance.playerScript.skillPoints > 0)
        {
            gameManager.instance.playerScript.SwordList.Add(iceSword);
            gameManager.instance.playerScript.skillPoints--;
            gameManager.instance.invManager.UpdateSkillPoints();
            iceButton.SetActive(false);
        }
        else if (gameManager.instance.playerScript.playerWeapon == PlayerController.weapon.Staff && gameManager.instance.playerScript.skillPoints > 0)
        {
            gameManager.instance.playerScript.staffList.Add(iceStaff);
            gameManager.instance.playerScript.skillPoints--;
            gameManager.instance.invManager.UpdateSkillPoints();
            iceButton.SetActive(false);

        }
        else if (gameManager.instance.playerScript.playerWeapon == PlayerController.weapon.Bow && gameManager.instance.playerScript.skillPoints > 0)
        {
            gameManager.instance.playerScript.BowList.Add(iceBow);
            gameManager.instance.playerScript.skillPoints--;
            gameManager.instance.invManager.UpdateSkillPoints();
            iceButton.SetActive(false);

        }
    }

    public void EarthSkill()
    {
        earthButton = GameObject.FindGameObjectWithTag("Earth Button");

        if (gameManager.instance.playerScript.playerWeapon == PlayerController.weapon.Sword && gameManager.instance.playerScript.skillPoints > 0)
        {
            gameManager.instance.playerScript.SwordList.Add(earthSword);
            gameManager.instance.playerScript.skillPoints--;
            gameManager.instance.invManager.UpdateSkillPoints();
            earthButton.SetActive(false);
        }
        else if (gameManager.instance.playerScript.playerWeapon == PlayerController.weapon.Staff && gameManager.instance.playerScript.skillPoints > 0)
        {
            gameManager.instance.playerScript.staffList.Add(earthStaff);
            gameManager.instance.playerScript.skillPoints--;
            gameManager.instance.invManager.UpdateSkillPoints();
            earthButton.SetActive(false);
        }
        else if (gameManager.instance.playerScript.playerWeapon == PlayerController.weapon.Bow && gameManager.instance.playerScript.skillPoints > 0)
        {
            gameManager.instance.playerScript.BowList.Add(earthBow);
            gameManager.instance.playerScript.skillPoints--;
            gameManager.instance.invManager.UpdateSkillPoints();
            earthButton.SetActive(false);
        }
    }
}
