using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingBullets : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] int speed;
    [SerializeField] int timer;

    //gun stats
    public float timeBetmeenShooting, bounce, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerClick;
    public bool allowButtonHold;

    int bulletsRemaining;
    int bulletsShot;

    public void bouncingBullet()
    {

    }
}
