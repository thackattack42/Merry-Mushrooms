using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Range(0, 100)][SerializeField] public int damage;
    [SerializeField] protected int speed;
    [SerializeField] protected int timer;

    [SerializeField] protected Rigidbody rb;

    protected bool collisionTriggered;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        collisionTriggered = false;

        Destroy(gameObject, timer);
        rb.velocity = transform.forward * speed;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        // Collider triggering multiple times before we are destroyed...
        if (collisionTriggered)
            return;

        IDamage damagable = other.GetComponent<IDamage>();

        if (damagable != null)
        {
            damagable.takeDamage(damage);
            collisionTriggered = true;
        }

        Destroy(gameObject);
    }
}

