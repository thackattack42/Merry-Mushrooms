using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
  //  [SerializeField] float AttackRate;
    [SerializeField] BoxCollider MeleeObj;
   // private bool isAttacking;
    [SerializeField] int dmg;
   // Animator animator;
    public float animrTransSpeed;
    //[SerializeField] Animator animr;
    bool playerSwung;
    private GameObject slash;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<BoxCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (gameManager.instance.playerScript.SwordEquipped)
        {
           
            
            if (Input.GetButtonDown("Shoot") && !playerSwung && !gameManager.instance.playerScript.holdingShield)
            {
                //slash = GameObject.FindGameObjectWithTag("Slash");

                //gameManager.instance.playerScript.SwordList[gameManager.instance.playerScript.selectedSword].ps.Play();
                //animr.SetBool("Attacking", true);

                GetComponent<BoxCollider>().enabled = true;
                
               
            }
            else if (Input.GetButtonUp("Shoot"))
            {
               
                playerSwung = false; ;
               // animr.SetBool("Attacking", false);
            }
        }
    }

    public void playAudio()
    {
            gameManager.instance.playerScript.aud.PlayOneShot(gameManager.instance.playerScript.SwordList[gameManager.instance.playerScript.selectedSword].swingSound, gameManager.instance.playerScript.SwordList[gameManager.instance.playerScript.selectedSword].swingVol);
    }
    void OnCollisionEnter(Collision other)
    {
        if (gameManager.instance.playerScript.SwordList[gameManager.instance.playerScript.selectedSword].earth)
        {
            IEarthDamage earthDamage = other.gameObject.GetComponent<IEarthDamage>();
            IPhysics physicsable = other.gameObject.GetComponent<IPhysics>();
            if (physicsable != null)
            {
                Vector3 dir = other.transform.position - transform.position;
                physicsable.KnockBack(dir * gameManager.instance.playerScript.knockbackPower);
            }
            if (earthDamage != null)
                earthDamage.TakeEarthDamage(gameManager.instance.playerScript.SwordList[gameManager.instance.playerScript.selectedSword].swingtDamage);
        }
        if (gameManager.instance.playerScript.SwordList[gameManager.instance.playerScript.selectedSword].ice)
        {
            IIceDamage iceDamage = other.gameObject.GetComponent<IIceDamage>();

            if (iceDamage != null)
                iceDamage.TakeIceDamage(gameManager.instance.playerScript.SwordList[gameManager.instance.playerScript.selectedSword].swingtDamage);
        }
        if (gameManager.instance.playerScript.SwordList[gameManager.instance.playerScript.selectedSword].fire)
        {
            IFireDamage fireDamage = other.gameObject.GetComponent<IFireDamage>();

            if (fireDamage != null)
                fireDamage.TakeFireDamage(gameManager.instance.playerScript.SwordList[gameManager.instance.playerScript.selectedSword].swingtDamage);
        }
        if (gameManager.instance.playerScript.SwordList[gameManager.instance.playerScript.selectedSword].baseStaff)
        {
            IDamage damagable = other.gameObject.GetComponent<IDamage>();

            if (damagable != null)
            {
                damagable.takeDamage(gameManager.instance.playerScript.SwordList[gameManager.instance.playerScript.selectedSword].swingtDamage);
                //GetComponent<BoxCollider>().enabled = false;
            }
        }
        
    }

    IEnumerator WaitForSlash()
    {

        yield return new WaitForSeconds(0.5f);
        Debug.Log("Did slash");
       // Instantiate(gameManager.instance.playerScript.SwordList[gameManager.instance.playerScript.selectedSword].slashEffect, slash.transform.position, Camera.main.transform.rotation);
    }
    #region Attacking Functions
     
    
    #endregion

}
