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
                collision.gameObject.GetComponent<Enemy___Ice>().TakeFireDamage(gameManager.instance.playerScript.shootDamage);

            }
            if (gameManager.instance.playerScript.staffList[gameManager.instance.playerScript.selectedStaff].earth)
            {

                collision.gameObject.GetComponent<Enemy___Ice>().TakeEarthDamage(gameManager.instance.playerScript.shootDamage);
            }

            if (gameManager.instance.playerScript.staffList[gameManager.instance.playerScript.selectedStaff].ice)
            {


                collision.gameObject.GetComponent<Enemy___Ice>().TakeIceDamage(gameManager.instance.playerScript.shootDamage);
            }


        }
    }
}
