using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrowDamage : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if (gameManager.instance.playerScript.BowList[gameManager.instance.playerScript.selectedBow].earth)
        {
            IEarthDamage earthDamage = collision.gameObject.GetComponent<IEarthDamage>();

            if (earthDamage != null)
                earthDamage.TakeEarthDamage(gameManager.instance.playerScript.shootDamage);
        }

        if (gameManager.instance.playerScript.BowList[gameManager.instance.playerScript.selectedBow].ice)
        {
            IIceDamage iceDamage = collision.gameObject.GetComponent<IIceDamage>();

            if (iceDamage != null)
                iceDamage.TakeIceDamage(gameManager.instance.playerScript.shootDamage);
        }
        if (gameManager.instance.playerScript.BowList[gameManager.instance.playerScript.selectedBow].fire)
        {
            IFireDamage fireDamge = collision.gameObject.GetComponent<IFireDamage>();

            if (fireDamge != null)
                fireDamge.TakeFireDamage(gameManager.instance.playerScript.shootDamage);
        }
        if (gameManager.instance.playerScript.BowList[gameManager.instance.playerScript.selectedBow].baseStaff)
        {
            IDamage damageable = collision.gameObject.GetComponent<IDamage>();

            if (damageable != null)
                damageable.takeDamage(gameManager.instance.playerScript.shootDamage);
        }

        //IDamage damageable = collision.gameObject.GetComponent<IDamage>();

        //if (damageable != null)
        //    damageable.takeDamage(gameManager.instance.playerScript.shootDamage);

    }
}
