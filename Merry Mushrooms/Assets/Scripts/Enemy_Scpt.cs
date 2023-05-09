using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Scpt : MonoBehaviour, IDamage
{
    [Header("------ Enemy stats ------")]
    [Range (5,100)][SerializeField] int EnemyHP;

    [Header("------ Componets ------")]
    [SerializeField] Renderer model;

    //Other Assets
    Color origColor;

    // Start is called before the first frame update
    void Start()
    {
        //gets original color and sets it here
        origColor = model.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int dmg) //this make it that enemy takes damage
    {
        EnemyHP -= dmg;
        FlashHitColor();
        if(EnemyHP <= 0)
        {
            Destroy(gameObject);
        }
    }
    IEnumerator FlashHitColor() //flash when the enemy is hit
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = origColor;
    }
}
