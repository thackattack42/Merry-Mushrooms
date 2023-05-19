using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingBullets : Bullet
{ 
    public enum BulletType // your custom enumeration
    {
        Bouncing,
        ExplodingEarthProjectile,
        FlameThrower,
        PoisonBullet,
        WaterBullet,
        
    };
    [SerializeField]
    private BulletType bulletType = BulletType.Bouncing;

    [Header("-----Bullet Stats-----")]
    [SerializeField] int bounce;
    [SerializeField] int maxBounceCount;

    [SerializeField] GameObject explosion;

    public int bounceCount;

    protected override void Start()
    {
        switch (bulletType)
        {
            case BulletType.Bouncing:
                {
                    bounceCount = 0;
                    rb.velocity = transform.forward * speed;
                }
                break;
            default:
                base.Start();
                break;
        }
    }

    private void Update()
    {
        switch (bulletType)// switch case allows the ability to change bullet type in the same script
        {
            case BulletType.Bouncing:
            {
                if (bounceCount == maxBounceCount)
                {
                    // boom
                    if (explosion != null)
                        Instantiate(explosion, transform.position, transform.rotation);
                     Destroy(gameObject);
                }
            }
            break;
            case BulletType.ExplodingEarthProjectile:
            {
                    //if ()
                    //{
                    //    if (explosion != null)
                    //        Instantiate(explosion, transform.position, transform.rotation);
                    //    Destroy(gameObject);
                    //}
                        
            }
            break;
        }

    }

    protected override void OnTriggerEnter(Collider other)
    {
        IDamage damagable = other.GetComponent<IDamage>();

        switch (bulletType)
        {
            case BulletType.Bouncing:
            {
                if (bulletType == BulletType.Bouncing && other.name == "Terrain")
                {
                    rb.AddForce(transform.up * bounce, ForceMode.Impulse);
                    bounceCount += 1;
                }
                else
                {
                    base.OnTriggerEnter(other);
                }
            }
            break;
            default:
                base.OnTriggerEnter(other);
                break;
        }
    }

}
