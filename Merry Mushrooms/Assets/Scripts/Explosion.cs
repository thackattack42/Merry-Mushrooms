using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] protected int damage;
    [SerializeField] protected int timer;
    [SerializeField] protected float radius;

    protected Vector3 scaleChange;
    protected bool collisionTriggered;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //scaleChange = new Vector3(0.01f, 0.01f, 0.01f);
        collisionTriggered = false;

        Destroy(gameObject, timer);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //gameObject.transform.localScale += scaleChange;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (collisionTriggered)
            return;

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyCollider in colliders)
        {
            IDamage nearbyDamagable = nearbyCollider.GetComponent<IDamage>();

            if (nearbyDamagable != null)
            {
                nearbyDamagable.takeDamage(damage);
            }
        }

        collisionTriggered = true;
    }
}
