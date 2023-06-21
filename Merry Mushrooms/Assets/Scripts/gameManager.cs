using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;

    public Transform UICanvas;
    [Header("-----Player Stuff-----")]
    public GameObject player;
    public PlayerController playerScript;
    public PlayerHUD playerHUD;
    [SerializeField] public GameObject playerSpawnPos;
    public Transform playerPrefab;
   // public StaffPickup staffPick;

    [Header("-----UI Menus-----")]
    public GameObject activeMenu;
    public GameObject gamePlayUI;
    public GameObject mainMenuUI;
    public GameObject pauseMenu;
    public GameObject loseMenu;
    public GameObject optionsMenu;
    public GameObject winMenu;
    public GameObject nextLevelMenu;
    public GameObject weaponSelectMenu;
    public GameObject mainMenuButtons;
    public GameObject credits;
    public bool isPaused;

    [Header("-----GamePlay-----")]
    public GameObject reticle;
    public Transform minimapRotationLock;
    public Image dashCooldownSlider;
    public Image dashCooldownFinish;

    [Header("-----Health-----")]
    public TextMeshProUGUI healthPoints;
    public Image HPSlider;
    public GameObject lowHPFlash;
    public Image dmgFlash;

    [Header("-----Ammo/MP-----")]
    public TextMeshProUGUI ammoCount;
    public TextMeshProUGUI ammoTotal;
    public TextMeshProUGUI manaPoints;
    public Image MPSlider;

    [Header("-----EXP/Level-----")]
    public TextMeshProUGUI PlayerLevelCounter;
    public TextMeshProUGUI PlayerExpPercent;
    public TextMeshProUGUI PlayerExpNumber;
    public Image ExpBarSlider;

    [Header("-----Inventory/Shop-----")]
    public TextMeshProUGUI funGil;
    public GameObject Inventory;
    public InventoryManager invManager;
    public GameObject shopMenu;
    bool InvToggle;
    [Header("-----Audio-----")]
    public AudioMixer SFXSlider;
    public AudioMixer MusicSlider;
    public Music musicScript;
    public AudioSource dashCooldownFinishPing;

    [Header("-----Menuing-----")]
    public buttonFunctions buttons;

    
    [Header("-----Other-----")]
    //public GameObject Sword;
    //public GameObject Bow;
    //public GameObject Staff;


    //[SerializeField] GameObject teleporter;
    public int enemiesRemaining;
    float timeScaleOrig;
    bool hasPlayed;
    public bool hasBeenOnLoadScreen;
    //float loadTimer;

    // Awake is called before Start
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
        else
        {
            instance = this;
            RefreshGameManager();
        }
        DontDestroyOnLoad(this.transform.parent);
    }

    public void RefreshGameManager()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerController>();
        playerHUD = player.GetComponent<PlayerHUD>();
        timeScaleOrig = 1;
        buttons.LoadSettings();
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
        {
            if (hasPlayed)
            {
                UnpausedState();
                StartCoroutine(ResetPlayer());
            }
            else
                player.SetActive(false);
            gamePlayUI.SetActive(false);
            mainMenuUI.SetActive(true);
            //StartCoroutine(MainMenu());
            isPaused = true;
            activeMenu = mainMenuButtons;
            activeMenu.SetActive(true);
            if (!hasPlayed)
            {
                PauseState();
                hasPlayed = false;
            }

        }
        else
        {
            mainMenuUI.SetActive(false);
            gamePlayUI.SetActive(true);
            playerSpawnPos = GameObject.FindGameObjectWithTag("Player Spawn Pos");
            if (hasBeenOnLoadScreen)
                UnpausedState();
            //if (playerScript.playerWeapon != 0 && SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
            //{
            //    playerScript.playerWeapon = 0;
            //    playerScript.swordMat.enabled = false;
            //    playerScript.staffMat.enabled = false;
            //    playerScript.bowMat.enabled = false;
            //    playerScript.shieldMat.enabled = false;
            //}

            if (playerScript.playerWeapon == 0)
            {
                StartCoroutine(StartSelection());
                
            }
            playerScript.enabled = true;
            if (SceneManager.GetActiveScene().buildIndex != 1)
                playerScript.SpawnOnNextLvl();
            else
                playerScript.Spawn();
            if (hasPlayed)
            {
                playerScript.ResetDash();
                playerHUD.ResetDash();
            }
            hasPlayed = true;
        }
        musicScript.HardSwitchMusic();
    }
    
    IEnumerator StartSelection()
    {
        yield return new WaitForSeconds(0.01f);
        isPaused = !isPaused;
        activeMenu = weaponSelectMenu;
        activeMenu.SetActive(true);
        PauseState();

    }
    IEnumerator ResetPlayer()
    {
        Destroy(player);
        yield return new WaitForSeconds(0.01f);

        Instantiate(playerPrefab);
        yield return new WaitForSeconds(0.01f);

        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerController>();
        playerHUD = player.GetComponent<PlayerHUD>();
        player.SetActive(false);
        PauseState();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && activeMenu == null && !playerScript.swordSwung && !playerScript.holdingShield && !playerScript.bowShot && !playerScript.isShooting)
        {
            isPaused = !isPaused;
            activeMenu = pauseMenu;
            activeMenu.SetActive(true);
            PauseState();
        }
        if (Input.GetButtonDown("Tab") && activeMenu == null)
        {
            InvToggle = !InvToggle;
            activeMenu = Inventory;
            Inventory.SetActive(InvToggle);
            if (InvToggle)
                PauseState();
            else
            {
                UnpausedState();
                invManager.updated = false;
            }
        }
    }

    public void PauseState()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        reticle.SetActive(false);
        playerScript.enabled = false;
    }
    public void UnpausedState()
    {
        Time.timeScale = timeScaleOrig;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
        InvToggle = false;
        dmgFlash.enabled = false;
        lowHPFlash.SetActive(false);
        if (activeMenu != null)
            activeMenu.SetActive(false);
        activeMenu = null;
        reticle.SetActive(true);
        playerScript.enabled = true;
    }
    public void GameOver()
    {
        PauseState();
        activeMenu = loseMenu;
        activeMenu.SetActive(true);
    }

    public void UpdateGameGoal(int amount)
    {
        enemiesRemaining += amount;

       if (enemiesRemaining <= 0) 
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
                StartCoroutine(GoToNextLevel());
            else
                StartCoroutine(YouWin());
        }
    }

    public IEnumerator YouWin()
    {
        yield return new WaitForSeconds(5);
        //teleporter.SetActive(true);
        PauseState();
        activeMenu = winMenu;
        activeMenu.SetActive(true);
    }
    public IEnumerator GoToNextLevel()
    {
        yield return new WaitForSeconds(5);
        //teleporter.SetActive(true);
        PauseState();
        activeMenu = nextLevelMenu;
        activeMenu.SetActive(true);
    }

    public void UpdateAmmoCount()
    {

        if (playerScript.StaffEquipped == true)
        {
        ammoCount.text = playerScript.staffList[playerScript.selectedStaff].ammoClip.ToString();
        ammoTotal.text = playerScript.staffList[playerScript.selectedStaff].ammoReserves.ToString();
        }
        if (playerScript.BowEquipped == true)
        {
            ammoCount.text = playerScript.BowList[playerScript.selectedBow].ammoClip.ToString();
            ammoTotal.text = playerScript.BowList[playerScript.selectedBow].ammoReserves.ToString();
        }
    }
}
