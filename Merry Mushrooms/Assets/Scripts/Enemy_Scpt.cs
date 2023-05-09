using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Scpt : MonoBehaviour, IDamage
{
    [Header("------ Enemy stats ------")]
    [Range (5,100)][SerializeField] int EnemyHP;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int dmg)
    {
        EnemyHP -= dmg;
        if(EnemyHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
