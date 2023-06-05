using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerHUD : MonoBehaviour
{
    //Player stuff
    int maxPlayerHP;
    int funGil;

    //Audio Stuff
    [Header("-----Audio Stuff-----")]
    [SerializeField] AudioClip dashCooldownFinishPing;
    [Range(0f,1f)][SerializeField] float dashCooldownFinishPingVol;

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
        funGil = 0;
        //Set Values to HUD
        gameManager.instance.healthPoints.text = maxPlayerHP.ToString();
        gameManager.instance.HPSlider.fillAmount = 1f;
        gameManager.instance.funGil.text = funGil.ToString();
        //gameManager.instance.dashCooldownCounter.text = "";
        gameManager.instance.dashCooldownSlider.fillAmount = 1f;
        constraint.sourceTransform = gameManager.instance.minimapRotationLock;
        constraint.weight = 1;
        minimapBGRot.AddSource(constraint);
        minimapCamRot.AddSource(constraint);
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
    public void updatePlayerHealth(int currHP)
    {
        if (currHP < 0) //to make sure the number in the UI doesn't show negative, or it will look weird.
            currHP = 0;
        if (((float)currHP / maxPlayerHP) < gameManager.instance.HPSlider.fillAmount)
            StartCoroutine(damageFlash());
        gameManager.instance.healthPoints.text = currHP.ToString();
        gameManager.instance.HPSlider.fillAmount = (float)currHP / maxPlayerHP;
        
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
        gameManager.instance.dashCooldownFinishPing.PlayOneShot(dashCooldownFinishPing, dashCooldownFinishPingVol);
        gameManager.instance.dashCooldownFinish.enabled = true;
        yield return new WaitForSeconds(0.1f);
        gameManager.instance.dashCooldownFinish.enabled = false;
    }
}
