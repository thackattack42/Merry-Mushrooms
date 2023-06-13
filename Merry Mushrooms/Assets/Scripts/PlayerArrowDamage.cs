using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerArrowDamage : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if (gameManager.instance.playerScript.BowList[gameManager.instance.playerScript.selectedBow].earth)
        {
            IEarthDamage earthDamage = collision.gameObject.GetComponent<IEarthDamage>();

            if (earthDamage != null)
                earthDamage.TakeEarthDamage((gameManager.instance.playerScript.BowList[gameManager.instance.playerScript.selectedBow].bowShootDamage) * (int)(gameManager.instance.playerScript.timer + 1));
        }

        if (gameManager.instance.playerScript.BowList[gameManager.instance.playerScript.selectedBow].ice)
        {
            IIceDamage iceDamage = collision.gameObject.GetComponent<IIceDamage>();

            if (iceDamage != null)
                iceDamage.TakeIceDamage((gameManager.instance.playerScript.BowList[gameManager.instance.playerScript.selectedBow].bowShootDamage) * (int)(gameManager.instance.playerScript.timer + 1));
        }
        if (gameManager.instance.playerScript.BowList[gameManager.instance.playerScript.selectedBow].fire)
        {
            IFireDamage fireDamge = collision.gameObject.GetComponent<IFireDamage>();

            if (fireDamge != null)
                fireDamge.TakeFireDamage((gameManager.instance.playerScript.BowList[gameManager.instance.playerScript.selectedBow].bowShootDamage) * (int)(gameManager.instance.playerScript.timer + 1));
        }
        if (gameManager.instance.playerScript.BowList[gameManager.instance.playerScript.selectedBow].baseStaff)
        {
            IDamage damageable = collision.gameObject.GetComponent<IDamage>();

            if (damageable != null)
                damageable.takeDamage((gameManager.instance.playerScript.BowList[gameManager.instance.playerScript.selectedBow].bowShootDamage) * (int)(gameManager.instance.playerScript.timer + 1));
            //Debug.Log((gameManager.instance.playerScript.bowShootDamage) * (int)(Time.time - gameManager.instance.playerScript.timer));
           // Debug.Log((gameManager.instance.playerScript.bowShootDamage * (int)gameManager.instance.playerScript.timer) / Time.deltaTime);
        }

        Destroy(gameObject);    
        //IDamage damageable = collision.gameObject.GetComponent<IDamage>();

        //if (damageable != null)
        //    damageable.takeDamage(gameManager.instance.playerScript.shootDamage);

    }
}
