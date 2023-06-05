using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletFire : MonoBehaviour
{
    // For damging Fire Enemies

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fire"))
        {
            if (gameManager.instance.playerScript.staffList[gameManager.instance.playerScript.selectedStaff].fire)
            {
                collision.gameObject.GetComponent<Enemy___Fire>().TakeFireDamage(gameManager.instance.playerScript.shootDamage);

            }
            if (gameManager.instance.playerScript.staffList[gameManager.instance.playerScript.selectedStaff].earth)
            {

                collision.gameObject.GetComponent<Enemy___Fire>().TakeEarthDamage(gameManager.instance.playerScript.shootDamage);
            }

            if (gameManager.instance.playerScript.staffList[gameManager.instance.playerScript.selectedStaff].ice)
            {


                collision.gameObject.GetComponent<Enemy___Fire>().TakeIceDamage(gameManager.instance.playerScript.shootDamage);
            }


        }
    }
}

