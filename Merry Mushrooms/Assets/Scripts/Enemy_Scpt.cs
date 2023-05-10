using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Scpt : MonoBehaviour, IDamage
{
    [Header("------ Enemy Stats ------")]
    [Range(5, 100)][SerializeField] int EnemyHP;

    [Header("------ Componets ------")]
    [SerializeField] Renderer model;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform shootPos;

    [Header("------ Enemy Weapon Stats ------")]
    [Range(5, 10)] [SerializeField] int shootDist;
    [Range(5, 10)][SerializeField] float ShootRate;
    [SerializeField] GameObject bullet;
    //
    //Other Assets
    Color origColor;
    private bool isShooting;
    // Start is called before the first frame update
    void Start()
    {
        //gets original color and sets it here
        gameManager.instance.UpdateGameGoal(1);
        origColor = model.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(gameManager.instance.player.transform.position);
        if (!isShooting)
        {
            StartCoroutine(shoot());
        }
    }

    public void takeDamage(int dmg) //this make it that enemy takes damage
    {
        EnemyHP -= dmg;
        StartCoroutine(FlashHitColor());
        if(EnemyHP <= 0)
        {
            gameManager.instance.UpdateGameGoal(-1);
            Destroy(gameObject);
        }
    }
    IEnumerator FlashHitColor() //flash when the enemy is hit
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = origColor;
    }
    IEnumerator shoot()
    {
        isShooting = true;
        GameObject bulletClone = Instantiate(bullet, shootPos.position, transform.rotation);
        yield return new WaitForSeconds(ShootRate);
        isShooting = false;
    }
}
