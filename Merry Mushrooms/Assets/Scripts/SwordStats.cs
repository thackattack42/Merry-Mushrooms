using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SwordStats : ScriptableObject
{
    [Header("----- Sword Stats -----")]
    //[Range(2, 300)] public int shootDistance;
    [Range(0.1f, 3)] public float swingtRate;
    [Range(1, 20)] public int swingtDamage;

    [Header("-----Ammo Stuff-----")]
    public GameObject model;
    public AudioClip swingSound;
    //public GameObject magicEffect;
    //public GameObject slashEffect;
    public float swingVol;
    public bool fire;
    public bool ice;
    public bool earth;
    public bool baseStaff;
}
