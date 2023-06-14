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

public class gameManager : MonoBehaviour
{
    public static gameManager instance;


    [Header("-----Player Stuff-----")]
    public GameObject player;
    public PlayerController playerScript;
    public PlayerHUD playerHUD;
    [SerializeField] public GameObject playerSpawnPos;
   // public StaffPickup staffPick;

    [Header("-----UI Stuff-----")]
    public GameObject activeMenu;
    public GameObject pauseMenu;
    public GameObject loseMenu;
    public GameObject optionsMenu;
    public GameObject winMenu;
    public GameObject weaponSelectMenu;
    public GameObject reticle;
    public Transform UICanvas;
    public TextMeshProUGUI ammoCount;
    public TextMeshProUGUI ammoTotal;
    public TextMeshProUGUI healthPoints;
    public TextMeshProUGUI manaPoints;
    public TextMeshProUGUI funGil;
    public Image HPSlider;
    public Image MPSlider;
    //public TextMeshProUGUI dashCooldownCounter;
    public Image dashCooldownSlider;
    public Image dashCooldownFinish;
    public AudioSource dashCooldownFinishPing;
    public GameObject lowHPFlash;
    public Transform minimapRotationLock;
    public AudioMixer SFXSlider;
    public AudioMixer MusicSlider;
    public Music musicScript;
    public Image dmgFlash;
    public GameObject Inventory;
    public InventoryManager invManager;
    public TextMeshProUGUI PlayerLevelCounter;
    public TextMeshProUGUI PlayerExpPercent;
    public TextMeshProUGUI PlayerExpNumber;
    public Image ExpBarSlider;
    public GameObject shopMenu;
    public buttonFunctions buttons;
    bool InvToggle;
    [Header("-----Player Pickups-----")]
    //public GameObject Sword;
    //public GameObject Bow;
    //public GameObject Staff;


    //[SerializeField] GameObject teleporter;
    public int enemiesRemaining;
    public bool isPaused;
    float timeScaleOrig;
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
        //teleporter = GameObject.FindGameObjectWithTag("Teleporter");

        //if (playerSpawnPos == null)
        //{
            playerSpawnPos = GameObject.FindGameObjectWithTag("Player Spawn Pos");
       // }

        timeScaleOrig = Time.timeScale;
        loadTimer = 3;

        if (playerScript.playerWeapon == 0)
        {
            StartCoroutine(StartSelection());
        }

        playerScript.SpawnOnLoad();
    }
    
    IEnumerator StartSelection()
    {
        yield return new WaitForSeconds(0.1f);
        isPaused = !isPaused;
        activeMenu = weaponSelectMenu;
        activeMenu.SetActive(isPaused);
        PauseState();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && activeMenu == null)
        {
            isPaused = !isPaused;
            activeMenu = pauseMenu;
            activeMenu.SetActive(isPaused);
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
    }
    public void SimiPauseState()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        reticle.SetActive(false);
    }
    public void UnpausedState()
    {
        Time.timeScale = timeScaleOrig;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
        InvToggle = false;

        activeMenu.SetActive(false);
        activeMenu = null;
        reticle.SetActive(true);
    }
    public void SimiUnpausedState()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        reticle.SetActive(true);
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
