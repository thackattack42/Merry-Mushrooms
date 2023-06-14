using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if(gameManager.instance != null)
        {
            gameManager.instance.RefreshGameManager();  
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
