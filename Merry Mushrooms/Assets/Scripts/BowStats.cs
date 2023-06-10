using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BowStats : ScriptableObject
{
    [Header("---- Bow Stats ----")]
    [Range(2, 300)] public int bowShootDistance;
    [Range(1, 20)] public int bowShootDamage;

    [Header("---- Bow Model things ----")]
    public GameObject model;        
    //public GameObject hitEffect;
    public AudioClip shootSound;
    public float shootVol;
    [Header("-----Ammo Stuff-----")]
    public int ammoClip;
    public int ammoReserves;
    public int origAmmo;
    public int origAmmoReserves;
    public bool fire;
    public bool ice;
    public bool earth;
    public bool baseStaff;
}
