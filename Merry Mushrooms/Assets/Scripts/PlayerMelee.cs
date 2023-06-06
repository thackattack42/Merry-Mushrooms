using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMelee : MonoBehaviour
{
  //  [SerializeField] float AttackRate;
    [SerializeField] BoxCollider MeleeObj;
   // private bool isAttacking;
    [SerializeField] int dmg;
   // Animator animator;
    public float animrTransSpeed;
    [SerializeField] Animator animr;
    // Start is called before the first frame update
    //void Start()
    //{
    //    animator = GetComponent<Animator>();
    //}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Shoot"))
        {

            animr.SetBool("Attacking", true);
        }
        else if (Input.GetButtonUp("Shoot"))
        {

           animr.SetBool("Attacking", false);  
        }
    }

    void OnCollisionEnter(Collision other)
    {
        IDamage damagable = other.gameObject.GetComponent<IDamage>();

        if (damagable != null)
        {
            damagable.takeDamage(dmg);
        }
    }
    #region Attacking Functions

    public void AttackingOn()
    {
        MeleeObj.enabled = true;
    }
    public void AttackingOff()
    {
        MeleeObj.enabled = false;
    }
    #endregion

}
