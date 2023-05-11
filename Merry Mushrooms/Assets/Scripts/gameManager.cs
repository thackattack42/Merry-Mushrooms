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
    public GameObject playerSpawnPos;

    [Header("-----Enemy Stuff-----")]
    public GameObject enemy;

    [Header("-----UI Stuff-----")]
    public GameObject activeMenu;
    public GameObject pauseMenu;
    public GameObject loseMenu;
    public GameObject winMenu;
    public GameObject reticle;
    public TextMeshProUGUI ammoCount;
    public TextMeshProUGUI ammoTotal;
    public TextMeshProUGUI healthPoints;
    public Image HPSlider;
    public TextMeshProUGUI dashCooldownCounter;
    public Image dashCooldownSlider;
    public Image enemyHPSliderBG;
    public Image enemyHPSlider;


    [SerializeField] public int ammoClip;
    [SerializeField] public int ammoReserves;
    int enemiesRemaining;
    public bool isPaused;
    float timeScaleOrig;
    int ammoClipOrig;

    // Awake is called before Start
    void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerController>();
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        enemyHPSliderBG = enemy.GetComponentInChildren<Image>();
        enemyHPSlider = enemyHPSliderBG.transform.GetChild(0).GetComponent<Image>();

        playerSpawnPos = GameObject.FindGameObjectWithTag("Player Spawn Pos");
        ammoCount.text = ammoClip.ToString();
        ammoTotal.text = ammoReserves.ToString();
        timeScaleOrig = Time.timeScale;
        ammoClipOrig = ammoClip;
        healthPoints.text = playerScript.maxHP.ToString();
        HPSlider.fillAmount = 1f;
        dashCooldownCounter.text = "";
        dashCooldownSlider.fillAmount = 0f;
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
        isPaused = !isPaused;

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
        ammoCount.text = ammoClip.ToString();

        if (ammoClip <= 0 || Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(2);
        ammoReserves -= ammoClipOrig - ammoClip;
        ammoClip = ammoClipOrig;
        ammoCount.text = ammoClip.ToString();
        ammoTotal.text = ammoReserves.ToString();
    }
}
