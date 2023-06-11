using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailRenderActivate : MonoBehaviour
{

    bool inSlash;
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        if (gameManager.instance.playerScript.SwordEquipped )
        {


            if (Input.GetButtonDown("Shoot") && !inSlash && !gameManager.instance.playerScript.holdingShield)
            {

                
                
                StartCoroutine(WaitForSlash());
            }
            
        }
    }

    IEnumerator WaitForSlash()
    {
        GetComponent<TrailRenderer>().enabled = true;
        inSlash = true;
        yield return new WaitForSeconds(1);
        GetComponent<TrailRenderer>().enabled = false;  
        inSlash = false;
        Debug.Log("Did slash");
        // Instantiate(gameManager.instance.playerScript.SwordList[gameManager.instance.playerScript.selectedSword].slashEffect, slash.transform.position, Camera.main.transform.rotation);
    }
}
