using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerBulletDamage : MonoBehaviour
{
    private void Update()
    {
        Destroy(gameObject, 1);
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (gameManager.instance.playerScript.staffList[gameManager.instance.playerScript.selectedStaff].earth)
        {
            IEarthDamage earthDamage = collision.gameObject.GetComponent<IEarthDamage>();
            IPhysics physicsable = collision.gameObject.GetComponent<IPhysics>();
            if (physicsable != null)
            {
                Vector3 dir = collision.transform.position - transform.position;
                physicsable.KnockBack(dir * gameManager.instance.playerScript.knockbackPower);
            }
            if (earthDamage != null)
                earthDamage.TakeEarthDamage(gameManager.instance.playerScript.shootDamage);
            Destroy(gameObject);
        }

        if (gameManager.instance.playerScript.staffList[gameManager.instance.playerScript.selectedStaff].ice)
        {
            IIceDamage iceDamage = collision.gameObject.GetComponent<IIceDamage>();

            if (iceDamage != null)
                iceDamage.TakeIceDamage(gameManager.instance.playerScript.shootDamage);
            Destroy(gameObject);
        }
        if (gameManager.instance.playerScript.staffList[gameManager.instance.playerScript.selectedStaff].fire)
        {
            IFireDamage fireDamge = collision.gameObject.GetComponent<IFireDamage>();

            if (fireDamge != null)
                fireDamge.TakeFireDamage(gameManager.instance.playerScript.shootDamage);
            Destroy(gameObject);
        }
        if (gameManager.instance.playerScript.staffList[gameManager.instance.playerScript.selectedStaff].baseStaff)
        {
            IDamage damageable = collision.gameObject.GetComponent<IDamage>();

            if (damageable != null)
                damageable.takeDamage(gameManager.instance.playerScript.shootDamage);
        Destroy(gameObject);
        }

        
        //IDamage damageable = collision.gameObject.GetComponent<IDamage>();

        //if (damageable != null)
        //    damageable.takeDamage(gameManager.instance.playerScript.shootDamage);

    }

   
}
