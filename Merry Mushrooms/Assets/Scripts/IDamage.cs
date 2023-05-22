using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementType // your custom enumeration
{
    None,
    Fire,
    Water
};

public interface IDamage
{
    void takeDamage(int dmg);
}
