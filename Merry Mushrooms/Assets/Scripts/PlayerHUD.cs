using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    //Player stuff
    int maxPlayerHP;

    //Hotbar stuff
    float dashCooldownTimer;
    bool dashIsOnCooldown;
    bool lowHP;

    // Start is called before the first frame update
    void Start()
    {
        //Get Values
        maxPlayerHP = gameManager.instance.playerScript.maxHP;
        //Set Values to HUD
        gameManager.instance.healthPoints.text = maxPlayerHP.ToString();
        gameManager.instance.HPSlider.fillAmount = 1f;
        gameManager.instance.dashCooldownCounter.text = "";
        gameManager.instance.dashCooldownSlider.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (dashIsOnCooldown) //This can probably be optimized, but I'm not smart enough to know how.
        {
            dashCooldownTimer -= Time.deltaTime;
            gameManager.instance.dashCooldownCounter.text = dashCooldownTimer.ToString("0.0") + "s";
            gameManager.instance.dashCooldownSlider.fillAmount = dashCooldownTimer / gameManager.instance.playerScript.dashCoolDown; // divides the time left by the set time to get a percentage

            if (dashCooldownTimer <= 0)
            {
                dashIsOnCooldown = false;
                gameManager.instance.dashCooldownCounter.text = "";
                gameManager.instance.dashCooldownSlider.fillAmount = 0f;
            }
        }
    }
    public void updatePlayerHealth(int currHP)
    {
        if (currHP < 0) //to make sure the number in the UI doesn't show negative, or it will look weird.
            currHP = 0;
        gameManager.instance.healthPoints.text = currHP.ToString();
        gameManager.instance.HPSlider.fillAmount = (float)currHP / maxPlayerHP;
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
    IEnumerator HPFlash()
    {
        bool toggle = false;
        while (lowHP)
        {
            toggle = !toggle;
            gameManager.instance.lowHPFlash.SetActive(toggle);
            yield return new WaitForSeconds(0.2f);

        }
    }
    public void dashCooldown(float dashCD)
    {
        dashCooldownTimer = dashCD;
        dashIsOnCooldown = true;
    }
}
