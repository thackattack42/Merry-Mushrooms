using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusEffectType
{
    Fire,
    Slow,
    Stone
}

public struct StatusEffectData
{
    public string name;
    public int modifier;
    public int modifierPerSecond;
    public float duration; // in seconds

    public StatusEffectType type;

    public GameObject effectParticles;
}
