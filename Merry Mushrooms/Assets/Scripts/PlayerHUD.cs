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

    // Start is called before the first frame update
    void Start()
    {
        //Get Values
        maxPlayerHP = gameManager.instance.playerScript.HP;
        //Set Values to HUD
        gameManager.instance.healthPoints.text = maxPlayerHP.ToString();
        gameManager.instance.HPSlider.fillAmount = 1f;
        gameManager.instance.dashCooldownCounter.text = "";
        gameManager.instance.dashCooldownSlider.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
            gameManager.instance.dashCooldownCounter.text = dashCooldownTimer.ToString("0.0") + "s";
            gameManager.instance.dashCooldownSlider.fillAmount = dashCooldownTimer / gameManager.instance.playerScript.dashCoolDown;
        }
        else if (dashIsOnCooldown && dashCooldownTimer <= 0)
        {
            gameManager.instance.dashCooldownCounter.text = "";
            gameManager.instance.dashCooldownSlider.fillAmount = 0f;
        }
    }
    public void updatePlayerHealth(int currHP)
    {
        if (currHP < 0)
            currHP = 0;
        gameManager.instance.healthPoints.text = currHP.ToString();
        gameManager.instance.HPSlider.fillAmount = (float)currHP / maxPlayerHP;

    }
    public void dashCooldown(float dashCD)
    {
        dashCooldownTimer = dashCD;
        dashIsOnCooldown = true;
    }
}
