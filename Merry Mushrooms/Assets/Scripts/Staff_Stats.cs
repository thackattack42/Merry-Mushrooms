using System.Collections;
using System.Collections.Generic;
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
    public int startingAmmo;
    public int ammoReserves;
    public int ammoClip;
    public int origAmmo;
    public GameObject model;
    public GameObject hitEffect;
    
}
