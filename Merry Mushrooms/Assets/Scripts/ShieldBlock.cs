using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBlock : MonoBehaviour
{
    Animator anim;
    float origSpeed;
    int amountClicked;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        GetComponent<BoxCollider>().enabled = false;
    }
    void Update()
    {
        if (gameManager.instance.playerScript.ShieldEquipped)
        {
            //GetComponent<BoxCollider>().enabled = true;
            
            ActivateShield();
            if (Input.GetMouseButtonDown(1) && amountClicked == 0)
            {
                
                amountClicked++;
                origSpeed = anim.speed;
                anim.SetBool("unPlay", false);
                anim.SetBool("Play", true);
                StartCoroutine(pauseShield());

            }
            else if (Input.GetMouseButtonUp(1) && amountClicked > 0)
            {
                StopAllCoroutines();
                
                amountClicked++;
                anim.SetBool("Play", false);
                anim.SetBool("unPlay", true);
                anim.speed = origSpeed;
                amountClicked = 0;
            }
        }
    }


    private void ActivateShield()
    {
        if(Input.GetMouseButtonDown(1))
        {
            GetComponent<BoxCollider>().enabled = true ;
        }
        else if(Input.GetMouseButtonUp(1))
        {
            GetComponent<BoxCollider>().enabled = false ;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Shiled did thing");
    }

    IEnumerator pauseShield()
    {
        yield return new WaitForSeconds(0.66f);
        Debug.Log("Did thing");
        anim.speed = 0;
    }
}
