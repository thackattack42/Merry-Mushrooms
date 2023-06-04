using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletIce : MonoBehaviour
{
    // For damging Ice Enemies
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ice"))
        {
            if (gameManager.instance.playerScript.staffList[gameManager.instance.playerScript.selectedStaff].fire)
            {
                collision.gameObject.GetComponent<IceEnemy_Scpt>().TakeFireDamage(gameManager.instance.playerScript.shootDamage);

            }
            if (gameManager.instance.playerScript.staffList[gameManager.instance.playerScript.selectedStaff].earth)
            {

                collision.gameObject.GetComponent<IceEnemy_Scpt>().TakeEarthDamage(gameManager.instance.playerScript.shootDamage);
            }

            if (gameManager.instance.playerScript.staffList[gameManager.instance.playerScript.selectedStaff].ice)
            {


                collision.gameObject.GetComponent<IceEnemy_Scpt>().TakeIceDamage(gameManager.instance.playerScript.shootDamage);
            }


        }
    }
}
