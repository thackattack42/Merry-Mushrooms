using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon_Scpt : MonoBehaviour
{
    [SerializeField] public int dmg;
    void OnTriggerEnter(Collider other)
    {
        IDamage damagable = other.GetComponent<IDamage>();

        if (damagable != null)
        {
            damagable.takeDamage(dmg);
        }
    }
}
