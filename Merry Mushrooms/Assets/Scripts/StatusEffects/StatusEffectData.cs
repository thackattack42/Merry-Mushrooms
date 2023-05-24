using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Status Effect")]

public class StatusEffectData : ScriptableObject
{
    public string Name;
    public float DotAmount;
    public float movementEffects;
    public float statusTime;

    public GameObject EffectParticles;
}
