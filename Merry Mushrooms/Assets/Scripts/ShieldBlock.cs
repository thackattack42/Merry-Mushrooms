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
       
    }
    void Update()
    {
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
            anim.SetBool("Play", false);
            anim.SetBool("unPlay", true);
            anim.speed = origSpeed;
            amountClicked = 0;
        }
    }


    IEnumerator pauseShield()
    {
        yield return new WaitForSeconds(0.66f);
        Debug.Log("Did thing");
        anim.speed = 0;
    }
}