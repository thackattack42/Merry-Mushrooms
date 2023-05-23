using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject reticle;
    public TextMeshProUGUI ammoCount;
    public TextMeshProUGUI ammoTotal;
    public TextMeshProUGUI healthPoints;
    public Image HPSlider;
    //public TextMeshProUGUI dashCooldownCounter;
    public Image dashCooldownSlider;
    public Image dashCooldownFinish;
    public AudioSource dashCooldownFinishPing;
    public GameObject lowHPFlash;
    public Transform minimapRotationLock;
    
    [SerializeField] public int ammoClip;
    [SerializeField] public int ammoReserves;
    public List<int> ammoReservesList = new List<int>();
    public List<int> ammoClipList = new List<int>();
    public int currArrayPos;
    int enemiesRemaining;
    public bool isPaused;
    float timeScaleOrig;
    float enemyViewDistOrig;
    public int ammoClipOrig;

    // Awake is called before Start
    void Awake()
    {
        
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerController>();
        playerHUD = player.GetComponent<PlayerHUD>();
        playerSpawnPos = GameObject.FindGameObjectWithTag("Player Spawn Pos");

        ammoCount.text = ammoClip.ToString();
        ammoTotal.text = ammoReserves.ToString();
        timeScaleOrig = Time.timeScale;
        ammoClipOrig = ammoClip;
        
        Debug.Log(ammoClipOrig);
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
    }

    public void PauseState()
    {
        Time.timeScale = 0;
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

        activeMenu.SetActive(false);
        activeMenu = null;
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
            StartCoroutine(YouWin());
    }

    IEnumerator YouWin()
    {
        yield return new WaitForSeconds(3);
        PauseState();
        activeMenu = winMenu;
        activeMenu.SetActive(true);
    }

    public void UpdateAmmoCount()
    {

        ammoCount.text = ammoClipList[currArrayPos].ToString();
        //ammoTotal.text = ammoReserves.ToString();
        //ammoTotal.text = ammoReservesList[currArrayPos].ToString();


        if (ammoClipList[currArrayPos] <= 0 || Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Reloading");
            StartCoroutine(Reload());

        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(2);
        ammoReservesList[currArrayPos] -= ammoClipOrig - ammoClipList[currArrayPos];
        ammoClipList[currArrayPos] = ammoClipOrig;
        ammoCount.text = ammoClipList[currArrayPos].ToString();
        ammoTotal.text = ammoReservesList[currArrayPos].ToString();
        Debug.Log("Finished Reloading");
    }
}
