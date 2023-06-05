using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletDamage : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if (gameManager.instance.playerScript.staffList[gameManager.instance.playerScript.selectedStaff].earth)
        {
            IEarthDamage earthDamage = collision.gameObject.GetComponent<IEarthDamage>();

            if (earthDamage != null)
                earthDamage.TakeEarthDamage(gameManager.instance.playerScript.shootDamage);
        }

        if (gameManager.instance.playerScript.staffList[gameManager.instance.playerScript.selectedStaff].ice)
        {
            IIceDamage iceDamage = collision.gameObject.GetComponent<IIceDamage>();

            if (iceDamage != null)
                iceDamage.TakeIceDamage(gameManager.instance.playerScript.shootDamage);
        }
        if (gameManager.instance.playerScript.staffList[gameManager.instance.playerScript.selectedStaff].fire)
        {
            IFireDamage fireDamge = collision.gameObject.GetComponent<IFireDamage>();

            if (fireDamge != null)
                fireDamge.TakeFireDamage(gameManager.instance.playerScript.shootDamage);
        }

        //IDamage damageable = collision.gameObject.GetComponent<IDamage>();

        //if (damageable != null)
        //    damageable.takeDamage(gameManager.instance.playerScript.shootDamage);
    
    }
}
