using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletEarth : MonoBehaviour
{
    // For damging Earth Enemies
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Earth"))
        {
            if (gameManager.instance.playerScript.staffList[gameManager.instance.playerScript.selectedStaff].fire)
            {
                collision.gameObject.GetComponent<Enemy___Earth>().TakeFireDamage(gameManager.instance.playerScript.shootDamage);

            }
            if (gameManager.instance.playerScript.staffList[gameManager.instance.playerScript.selectedStaff].earth)
            {

                collision.gameObject.GetComponent<Enemy___Earth>().TakeEarthDamage(gameManager.instance.playerScript.shootDamage);
            }

            if (gameManager.instance.playerScript.staffList[gameManager.instance.playerScript.selectedStaff].ice)
            {


                collision.gameObject.GetComponent<Enemy___Earth>().TakeIceDamage(gameManager.instance.playerScript.shootDamage);
            }


        }
    }
}
