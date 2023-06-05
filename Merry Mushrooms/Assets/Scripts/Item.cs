using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    string iName;
    public Item()
    {
        iName = "";
    }
    public Item(string name)
    {
        iName = name;
    }
    public string GetName()
    {
        return iName;
    }
    public void SetName(string name)
    {
        iName = name;
    }
    
}
