using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingBullets : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] int speed;
    [SerializeField] int timer;

    [SerializeField] Rigidbody rb;

    public float timeBetmeenShooting, bounce, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerClick;
    public bool allowButtonHold;

    int bulletsRemaining;
    int bulletsShot;

    bool shooting;
    bool readyToShoot;
    bool reloading;


    void Start()
    {
        Destroy(gameObject, timer);
        rb.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        {
            IDamage damagable = other.GetComponent<IDamage>();

            if (damagable != null)
            {
                damagable.takeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
    
    

}
