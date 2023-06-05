using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fungil : MonoBehaviour
{
    public const string Currency = "FunGil";
    public static int funGil = 0;

    // Start is called before the first frame update
    void Start()
    {
        funGil = PlayerPrefs.GetInt("FunGil");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void UpdateFunGil()
    {
        PlayerPrefs.SetInt("FunGil", funGil);
        funGil = PlayerPrefs.GetInt("FunGil");
        PlayerPrefs.Save();
    }
}
