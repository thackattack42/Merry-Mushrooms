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
        if (Input.GetButton("Shoot"))
        {
            animr.SetBool("Attacking", true);

            
        }

        //else if (Input.GetButtonUp("Shoot"))
        //{
        //    animr.SetBool("Attacking", false);
        //}

    }

    public void TurnOffSwing()
    {
        animr.SetBool("Attacking", false);
    }
}
