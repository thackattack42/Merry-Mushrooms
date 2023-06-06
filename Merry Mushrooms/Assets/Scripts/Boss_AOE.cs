using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_AOE : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] int pushAmount;

    private void Start()
    {
        Destroy(gameObject, 0.15f);
    }

    private void OnTriggerEnter(Collider other)
    {
        IPhysics physicsable = other.GetComponent<IPhysics>();
        if (physicsable != null)
        {
            Vector3 dir = other.transform.position - transform.position;
            physicsable.KnockBack(dir * pushAmount);
        }
    }
}
