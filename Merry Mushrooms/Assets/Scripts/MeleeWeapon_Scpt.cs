using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon_Scpt : MonoBehaviour
{
    [SerializeField] int dmg;
    //[SerializeField] int timer;

    //bool ColisionTriggered;
    // Start is called before the first frame update
    void Start()
    {
        //ColisionTriggered = false;

    }

    void OnTriggerEnter(Collider other)
    {
        IDamage damagable = other.GetComponent<IDamage>();

        if (damagable != null)
        {
            damagable.takeDamage(dmg);
            //collisionTriggered = true;
        }
    }
}
