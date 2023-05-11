using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingBullets : Bullet
{
    [SerializeField] int damage;
    [SerializeField] int speed;
    [SerializeField] int timer;
    [SerializeField] int bounce;
    [SerializeField] int maxBounceCount;

    [SerializeField] Rigidbody rb;
        
    [SerializeField] GameObject explosion;

    public float timeBetmeenShooting, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerClick;
    public bool allowButtonHold;

    int bulletsRemaining;
    int bulletsShot;

    bool shooting;
    bool readyToShoot;
    bool reloading;

    int bounceCount;

    void Start()
    {
        Destroy(gameObject, timer);

        bounceCount = 0;
        rb.velocity = transform.forward * speed;
    }

    private void Update()
    {
        if (bounceCount >= maxBounceCount)
        {
            // boom
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        IDamage damagable = other.GetComponent<IDamage>();

        if (other.tag == "Floor")
        {
            rb.AddForce(transform.up * bounce, ForceMode.Impulse);
            bounceCount += 1;
        }
        else
        {
            base.OnTriggerEnter(other);
        }
    }

}
