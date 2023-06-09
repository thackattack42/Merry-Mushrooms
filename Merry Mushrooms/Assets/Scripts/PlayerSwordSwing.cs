using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordSwing : MonoBehaviour
{

    [SerializeField] Animator animr;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Shoot") && !gameManager.instance.playerScript.holdingShield)
        {
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

    public void TurnOffSwing()
    {
        animr.ResetTrigger("Attacking");
    }
}
