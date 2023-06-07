using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
//using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public ItemType type;
    public bool stackable = true;
    public Sprite sprite;
    
}

public enum ItemType
{
    Armor, 
    Weapon, 
    Consumable
}