using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerHUD : MonoBehaviour
{
    //Player stuff
    int maxPlayerHP;
    int maxPlayerMP;
    [SerializeField ]int funGil;
    int damageTaken;
    public Transform damagePopup;
    public Transform expPopup;
    public Transform LevelUpPopup;
    int expGained;

    //Audio Stuff
    [Header("-----Audio Stuff-----")]
    [SerializeField] public AudioClip dashCooldownFinishPing;
    //[Range(0f,1f)][SerializeField] public float dashCooldownFinishPingVol;

    //Hotbar stuff
    float dashCooldownTimer;
    bool dashIsOnCooldown;
    bool lowHP;
    bool isDashing;

    //minimap stuff
    [Header("-----Minimap Stuff-----")]
    public RotationConstraint minimapBGRot;
    public RotationConstraint minimapCamRot;
    public GameObject minimapCam;
    ConstraintSource constraint;

    // Start is called before the first frame update
    void Start()
    {
        //Get Values
        maxPlayerHP = gameManager.instance.playerScript.maxHP;
        maxPlayerMP = gameManager.instance.playerScript.maxMP;
        funGil = 0;
        //Set Values to HUD
        gameManager.instance.healthPoints.text = maxPlayerHP.ToString();
        gameManager.instance.HPSlider.fillAmount = 1f;
        updatePlayerMana();
        gameManager.instance.funGil.text = funGil.ToString();
        //gameManager.instance.dashCooldownCounter.text = "";
        //gameManager.instance.dashCooldownSlider.fillAmount = 1f;
        constraint.sourceTransform = gameManager.instance.minimapRotationLock;
        constraint.weight = 1;
        minimapBGRot.AddSource(constraint);
        minimapCamRot.AddSource(constraint);
        UpdatePlayerLevel();
        UpdatePlayerEXP(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (dashIsOnCooldown)
        {
            dashCooldownTimer += Time.deltaTime;
            //gameManager.instance.dashCooldownCounter.text = dashCooldownTimer.ToString("0.0") + "s";
            gameManager.instance.dashCooldownSlider.fillAmount = dashCooldownTimer / (gameManager.instance.playerScript.dashCoolDown - 0.3f); // divides the time left by the set time to get a percentage

            if (dashCooldownTimer >= gameManager.instance.playerScript.dashCoolDown - 0.3f)
            {
                dashIsOnCooldown = false;
                //gameManager.instance.dashCooldownCounter.text = "";
                gameManager.instance.dashCooldownSlider.fillAmount = 1f;
                StartCoroutine(dashCooldownEnd());
            }
        }
        if (isDashing)
        {
            gameManager.instance.dashCooldownSlider.fillAmount -= Time.deltaTime * 5;
        }

        if (gameManager.instance.HPSlider.fillAmount <= 0.2f && !lowHP)
        {
            lowHP = true;
            StartCoroutine(HPFlash());
        }
        else if (gameManager.instance.HPSlider.fillAmount > 0.2f && lowHP)
        {
            lowHP = false;
        }
    }
    public void updatePlayerHealth(int amount)
    {
        damageTaken = amount;
        Instantiate(damagePopup, gameManager.instance.healthPoints.transform);
        if (gameManager.instance.playerScript.HP < 0) //to make sure the number in the UI doesn't show negative, or it will look weird.
            gameManager.instance.playerScript.HP = 0;
        if (((float)gameManager.instance.playerScript.HP / maxPlayerHP) < gameManager.instance.HPSlider.fillAmount)
            StartCoroutine(damageFlash());
        else
            gameManager.instance.lowHPFlash.SetActive(false);
        gameManager.instance.healthPoints.text = gameManager.instance.playerScript.HP.ToString();
        gameManager.instance.HPSlider.fillAmount = (float)gameManager.instance.playerScript.HP / maxPlayerHP;
        
    }
    public void updatePlayerMana()
    {
        float currMP = gameManager.instance.playerScript.MP;
        if (currMP < 0) 
            currMP = 0;
        gameManager.instance.manaPoints.text = Mathf.RoundToInt(currMP).ToString();
        gameManager.instance.MPSlider.fillAmount = (float)currMP / maxPlayerMP;
        
    }
    public void addFunGil(int amount)
    {
        funGil += amount;
        gameManager.instance.funGil.text = funGil.ToString();
    }
    IEnumerator damageFlash()
    {
        gameManager.instance.dmgFlash.enabled = true;
        yield return new WaitForSeconds(0.05f);
        gameManager.instance.dmgFlash.enabled = false;
    }
    IEnumerator HPFlash()
    {
        bool toggle = false;
        while (lowHP)
        {
            toggle = !toggle;
            gameManager.instance.lowHPFlash.SetActive(toggle);
            yield return new WaitForSeconds(0.2f);

        }
        gameManager.instance.lowHPFlash.SetActive(false);
    }
    public void dashCooldown()
    {
        dashCooldownTimer = 0;
        StartCoroutine(dashCooldownStart());
    }

    IEnumerator dashCooldownStart()
    {
        isDashing = true;
        yield return new WaitForSeconds(0.3f);
        isDashing = false;
        dashIsOnCooldown = true;
    }
    IEnumerator dashCooldownEnd()
    {
        gameManager.instance.dashCooldownFinishPing.PlayOneShot(dashCooldownFinishPing);
        gameManager.instance.dashCooldownFinish.enabled = true;
        yield return new WaitForSeconds(0.1f);
        gameManager.instance.dashCooldownFinish.enabled = false;
    }
    public void ResetDash()
    {
        StopCoroutine(dashCooldownStart());
        StopCoroutine(dashCooldownEnd());
        isDashing = false;
        dashIsOnCooldown = false;
        dashCooldownTimer = gameManager.instance.playerScript.dashCoolDown - 0.3f;
        gameManager.instance.dashCooldownFinish.enabled = false;
        gameManager.instance.dashCooldownSlider.fillAmount = 1f;

    }
    public void UpdatePlayerLevel()
    {
        gameManager.instance.PlayerLevelCounter.text = gameManager.instance.playerScript.level.ToString();
        if (gameManager.instance.playerScript.level > 0)
            Instantiate(LevelUpPopup, gameManager.instance.PlayerLevelCounter.transform);
        UpdatePlayerEXP(0);
    }
    public void UpdatePlayerEXP(int amount)
    {
        expGained = amount;
        if (amount > 0)
            Instantiate(expPopup, gameManager.instance.PlayerExpNumber.transform);
        float expPercent = ((float)gameManager.instance.playerScript.currExp / gameManager.instance.playerScript.expToNextLevel) * 100;
        gameManager.instance.PlayerExpNumber.text = gameManager.instance.playerScript.currExp.ToString() + " / " + gameManager.instance.playerScript.expToNextLevel.ToString();
        gameManager.instance.PlayerExpPercent.text = expPercent.ToString("0") + "%";
        gameManager.instance.ExpBarSlider.fillAmount = expPercent / 100;
    }
    public int GetDamageTaken()
    {
        return damageTaken;
    }
    public int GetExpGained()
    {
        return expGained;
    }
    public int GetFunGil()
    {
        return funGil;
    }
}
