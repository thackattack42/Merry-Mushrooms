using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMusicandUI : MonoBehaviour
{
    GameObject boss;
    BossUI bossUI;
    // Start is called before the first frame update
    private void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss Enemy");
        //bossUI = boss.GetComponentInChildren<BossUI>;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.musicScript.BossState(true);

        }
    }
}
