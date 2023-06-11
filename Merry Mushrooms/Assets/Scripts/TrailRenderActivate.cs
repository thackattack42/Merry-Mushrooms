using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailRenderActivate : MonoBehaviour
{
    // Start is called before the first frame update
   

    // Update is called once per frame
    void Update()
    {
        if (gameManager.instance.playerScript.SwordEquipped)
        {


            if (Input.GetButtonDown("Shoot") )
            {


                GetComponent<TrailRenderer>().enabled = true;
                StartCoroutine(WaitForSlash());
            }
            
        }
    }

    IEnumerator WaitForSlash()
    {

        yield return new WaitForSeconds(1);
        GetComponent<TrailRenderer>().enabled = false;  
        Debug.Log("Did slash");
        // Instantiate(gameManager.instance.playerScript.SwordList[gameManager.instance.playerScript.selectedSword].slashEffect, slash.transform.position, Camera.main.transform.rotation);
    }
}
