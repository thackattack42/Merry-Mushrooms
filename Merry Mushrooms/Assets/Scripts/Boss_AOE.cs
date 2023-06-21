using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_AOE : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] int pushAmount;

    private void Start()
    {
        Destroy(gameObject, 0.75f);
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamage damageable = other.GetComponent<IDamage>();
        if (damageable != null) 
        {
            damageable.takeDamage(damage);        
        }

        IPhysics physicsable = other.GetComponent<IPhysics>();
        if (physicsable != null)
        {
            Vector3 dir = other.transform.position - transform.position;
            physicsable.KnockBack(dir * pushAmount);
        }
    }
}
