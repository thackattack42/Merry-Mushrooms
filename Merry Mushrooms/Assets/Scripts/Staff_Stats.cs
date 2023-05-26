using System.Collections;
using System.Collections.Generic;
using TMPro;
//using UnityEditor.ShaderKeywordFilter;
using UnityEngine.UIElements;
using UnityEngine;

// Be able to right click and create a staff
[CreateAssetMenu]

public class Staff_Stats : ScriptableObject
{
    [Header("----- Staff Stats -----")]
    [Range(2, 300)] public int shootDistance;
    [Range(0.1f, 3)] public float shootRate;
    [Range(1, 20)] public int shootDamage;

    [Header("-----Ammo Stuff-----")]
    public GameObject model;
    public GameObject hitEffect;
    public GameObject muzzleFlash;
    public GameObject muzzleEffect;
    public AudioClip shootSound;
    public float shootVol;
    public int ammoClip;
    public int ammoReserves;
    public int origAmmo;
    public int origAmmoReserves;
    public bool fire;
    public bool ice;
    public bool earth;
    
}
