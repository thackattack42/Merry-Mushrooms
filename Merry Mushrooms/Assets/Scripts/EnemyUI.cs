using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;


public class EnemyUI : MonoBehaviour
{
    public static EnemyUI instance;

    [Header("-----Enemy Stuff-----")]
    public GameObject enemy;
    public Enemy_Scpt enemyScript;
    public Image enemyHPSlider;
    public TextMeshProUGUI enemyName;

    //other useful stuff
    int enemyMaxHP;
    float currHP;


    // Start is called before the first frame update
    void Start()
    {
        //Enemy Stuff grabbers
        instance = this;
        enemy = transform.parent.gameObject;
        enemyScript = enemy.GetComponent<Enemy_Scpt>();

        //Setup UI
        //enemyName.text = enemy.name;
        enemyHPSlider.fillAmount = 1f;
        enemyMaxHP = enemyScript.HP;
        currHP = enemyMaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        // WHY DOES THE UI NEED TO FACE *AWAY* FROM THE PLAYER, NOT TOWARDS FOR IT TO LOOK NORMAL?????? >:(
        transform.rotation = gameManager.instance.player.transform.rotation;


        // I can't think of a better solution for this without adding a GetComponentInChildren
        // in the Enemy Script which *might* break stuff, but IDK, I'm too scared to try!
        // Also, I've tried calling EnemyUI.updateEnemyHealth in the Enemy Script under takeDamage,
        // but it only updates one UI (usually the highest enemy in the hierarchy list). :/
        if (enemyScript.HP != currHP)
            updateEnemyHealth();
    }

    public void updateEnemyHealth()
    {
        currHP = enemyScript.HP;
        enemyHPSlider.fillAmount = currHP / enemyMaxHP;
    }
}
