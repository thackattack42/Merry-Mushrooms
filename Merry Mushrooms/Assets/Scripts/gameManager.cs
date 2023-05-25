using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
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
    
    //[SerializeField] public int ammoClip;
    //[SerializeField] public int ammoReserves;
    //public List<int> ammoReservesList = new List<int>();
    //public List<int> ammoClipList = new List<int>();
    //public int currArrayPos;
    int enemiesRemaining;
    public bool isPaused;
    float timeScaleOrig;
    //public int ammoClipOrig;

    // Awake is called before Start
    void Awake()
    {
        
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerController>();
        playerHUD = player.GetComponent<PlayerHUD>();
        playerSpawnPos = GameObject.FindGameObjectWithTag("Player Spawn Pos");

        //ammoCount.text = playerScript.staffList[playerScript.selectedStaff].ammoClip.ToString();
        //ammoTotal.text = playerScript.staffList[playerScript.selectedStaff].ammoReserves.ToString();
        timeScaleOrig = Time.timeScale;
        //ammoClipOrig = playerScript.staffList[playerScript.selectedStaff].ammoClip;
        
        //Debug.Log(ammoClipOrig);
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

        //if (Input.GetKeyDown(KeyCode.R) || playerScript.staffList[playerScript.selectedStaff].ammoClip <= 0)
        //{
        //    if (ammoClipList[playerScript.selectedStaff] != playerScript.staffList[playerScript.selectedStaff].origAmmo)
        //    {
        //        StartCoroutine(Reload());
        //        UpdateAmmoCount();
        //        ammoCount.text = ammoClipList[playerScript.selectedStaff].ToString();
        //    }
        //}
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
        ammoCount.text = playerScript.staffList[playerScript.selectedStaff].ammoClip.ToString();
        ammoTotal.text = playerScript.staffList[playerScript.selectedStaff].ammoReserves.ToString();









        //ammoReservesList[currArrayPos] -= ammoClipOrig - ammoClipList[currArrayPos];
        //ammoClipOrig = playerScript.staffList[playerScript.selectedStaff].ammoClip;
        //ammoCount.text = playerScript.staffList[playerScript.selectedStaff].ammoClip.ToString();
        //ammoTotal.text = playerScript.staffList[playerScript.selectedStaff].ammoReserves.ToString();
        
        //if (playerScript.isReloading)
        //{
        //    ammoClipList[currArrayPos] = ammoClipOrig;
        //    ammoCount.text = ammoClipList[currArrayPos].ToString();
        //    ammoTotal.text = ammoReservesList[currArrayPos].ToString();
        //}
        //ammoTotal.text = ammoReserves.ToString();
        //ammoTotal.text = ammoReservesList[currArrayPos].ToString();


        //if (ammoClipList[currArrayPos] <= 0 || Input.GetKeyDown(KeyCode.R))
        //{
        //    Debug.Log("Reloading");
        //    StartCoroutine(Reload());

        //}
    }

    public IEnumerator Reload()
    {
        playerScript.isReloading = true;

        playerScript.staffList[playerScript.selectedStaff].ammoReserves -= (playerScript.staffList[playerScript.selectedStaff].origAmmo - playerScript.staffList[playerScript.selectedStaff].ammoClip);
        playerScript.staffList[playerScript.selectedStaff].ammoClip = playerScript.staffList[playerScript.selectedStaff].origAmmo;

        yield return new WaitForSeconds(2);
        UpdateAmmoCount();
        playerScript.isReloading = false;









        //playerScript.isReloading = true;

        //Debug.Log("Reloading");
        //playerScript.staffList[playerScript.selectedStaff].ammoClip = playerScript.staffList[playerScript.selectedStaff].origAmmo;
        //playerScript.staffList[playerScript.selectedStaff].ammoReserves = (playerScript.staffList[playerScript.selectedStaff].origAmmo - playerScript.staffList[playerScript.selectedStaff].ammoClip);
        //ammoTotal.text = playerScript.staffList[playerScript.selectedStaff].ammoReserves.ToString();
        //ammoCount.text = playerScript.staffList[playerScript.selectedStaff].origAmmo.ToString();
        //UpdateAmmoCount();
        //yield return new WaitForSeconds(2);

        //playerScript.isReloading = false;
        //Debug.Log("Finished Reloading");

        //ammoReservesList[currArrayPos] -= ammoClipOrig - ammoClipList[currArrayPos];
        //ammoClipList[currArrayPos] = ammoClipOrig;
        //ammoCount.text = ammoClipList[currArrayPos].ToString();
        //ammoTotal.text = ammoReservesList[currArrayPos].ToString();
    }
    //IEnumerator WaitForReload()
    //{
    //    isShooting = false;
    //    yield return new WaitForSeconds(2);
    //    isShooting = false;
    //    isReloading = false;
    //    reloadOnce = 0;
    //}
}
