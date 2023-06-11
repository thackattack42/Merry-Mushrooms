using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{

    [Header("-----Enemy Stuff-----")]
    public GameObject boss;
    public Boss_Scpt bossScript;
    public Image bossHPSlider;

    public RectTransform floatingDamage;
    public Transform BossUITransform;

    //other useful stuff
    float currHP;


    // Start is called before the first frame update
    void Start()
    {
        //Enemy Stuff grabbers
        boss = transform.parent.gameObject;
        bossScript = boss.GetComponent<Boss_Scpt>();

        //Setup UI
        //enemyName.text = enemy.name;
        bossHPSlider.fillAmount = 1f;
        currHP = bossScript.GetMaxHP();
    }

    // Update is called once per frame
    void Update()
    {


        // I can't think of a better solution for this without adding a GetComponentInChildren
        // in the Enemy Script which *might* break stuff, but IDK, I'm too scared to try!
        // Also, I've tried calling EnemyUI.updateEnemyHealth in the Enemy Script under takeDamage,
        // but it only updates one UI (usually the highest enemy in the hierarchy list). :/
        if (bossScript.GetCurrHP() != currHP)
        {
            Instantiate(floatingDamage, BossUITransform);

            updateEnemyHealth();
        }

    }

    public void updateEnemyHealth()
    {
        currHP = bossScript.GetCurrHP();
        bossHPSlider.fillAmount = currHP / bossScript.GetMaxHP();
        if (currHP <= 0)
            gameObject.SetActive(false);
    }

    public float GetUIHPVal()
    {
        return currHP;
    }
}
