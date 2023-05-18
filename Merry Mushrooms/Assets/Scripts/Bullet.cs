using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Range(0, 100)][SerializeField] protected int damage;
    [SerializeField] protected int speed;
    [SerializeField] protected int timer;

    [SerializeField] protected Rigidbody rb;

    bool damageTriggered;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        damageTriggered = false;

        Destroy(gameObject, timer);
        rb.velocity = transform.forward * speed;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        // Collider triggering multiple times before we are destroyed...
        if (damageTriggered)
            return;

        IDamage damagable = other.GetComponent<IDamage>();

        if (damagable != null)
        {
            damagable.takeDamage(damage);
            damageTriggered = true;
        }

        Destroy(gameObject);
    }
}

