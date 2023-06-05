using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    ItemType type;
    public bool stackable = true;
    public Image img;
    
}

public enum ItemType
{
    Armor, 
    Weapon, 
    Consumable
}