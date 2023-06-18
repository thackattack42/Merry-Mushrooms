using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordSwing : MonoBehaviour
{

    [SerializeField] Animator animr;
    // Start is called before the first frame update


    private void Awake()
    {
        animr.speed += 1;
    }
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButtonDown("Shoot") && !gameManager.instance.playerScript.holdingShield && gameManager.instance.playerScript.SwordEquipped)
        {
            if (gameManager.instance.playerScript.MP > 10 || gameManager.instance.playerScript.SwordList[gameManager.instance.playerScript.selectedSword].baseStaff)
                animr.SetTrigger("Attacking");
           
            
        } 
        else
        {
            animr.ResetTrigger("Attacking");
        }

        //else if (Input.GetButtonUp("Shoot"))
        //{
        //    animr.SetBool("Attacking", false);
        //}

    }

    public void UseMana()
    {

        if (gameManager.instance.playerScript.SwordList[gameManager.instance.playerScript.selectedSword].fire || gameManager.instance.playerScript.SwordList[gameManager.instance.playerScript.selectedSword].ice || gameManager.instance.playerScript.SwordList[gameManager.instance.playerScript.selectedSword].earth)
        gameManager.instance.playerScript.MP -= 10;
        gameManager.instance.playerHUD.updatePlayerMana();
    }
    public void TurnOffSwing()
    {
       
        animr.ResetTrigger("Attacking");
    }
   
    public void PlaySwingSound()
    {
        gameManager.instance.playerScript.aud.PlayOneShot(gameManager.instance.playerScript.SwordList[gameManager.instance.playerScript.selectedSword].swingSound, gameManager.instance.playerScript.SwordList[gameManager.instance.playerScript.selectedSword].swingVol);
    }
}
