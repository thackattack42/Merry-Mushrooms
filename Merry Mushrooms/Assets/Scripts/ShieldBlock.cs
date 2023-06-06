using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBlock : MonoBehaviour
{
    Animator anim;
    float origSpeed;
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
       
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            origSpeed = anim.speed;
            anim.SetBool("Play", true);
            StartCoroutine(pauseShield());
           
        }
        else if (Input.GetMouseButtonUp(1))
        {
            anim.SetBool("Play", false);
            anim.speed = origSpeed;
        }
    }

    IEnumerator pauseShield()
    {
        yield return new WaitForSeconds(0.60f);
        Debug.Log("Did thing");
        anim.speed = 0;
    }
}
