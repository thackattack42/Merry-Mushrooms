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
                collision.gameObject.GetComponent<FireEnemy_Scpt>().TakeFireDamage(gameManager.instance.playerScript.shootDamage);

            }
            if (gameManager.instance.playerScript.staffList[gameManager.instance.playerScript.selectedStaff].earth)
            {

                collision.gameObject.GetComponent<FireEnemy_Scpt>().TakeEarthDamage(gameManager.instance.playerScript.shootDamage);
            }

            if (gameManager.instance.playerScript.staffList[gameManager.instance.playerScript.selectedStaff].ice)
            {


                collision.gameObject.GetComponent<FireEnemy_Scpt>().TakeIceDamage(gameManager.instance.playerScript.shootDamage);
            }


        }
    }
}

