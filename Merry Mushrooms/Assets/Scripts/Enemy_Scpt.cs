using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Scpt : MonoBehaviour, IDamage
{
    [Header("------ Enemy Stats ------")]
    [Range(5, 100)][SerializeField] int EnemyHP;
    [Range(5, 100)][SerializeField] int playerFaceSpeed;

    [Header("------ Componets ------")]
    [SerializeField] Renderer model;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform shootPos;
    [SerializeField] Transform headPos;

    [Header("------ Enemy Weapon Stats ------")]
    [Range(5, 10)][SerializeField] int shootDist;
    [Range(5, 10)][SerializeField] float ShootRate;
    [SerializeField] GameObject bullet;
    //
    //Other Assets
    Color origColor;
    private bool isShooting;
    Vector3 playerDir;
    bool playerInRange;
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
        if (playerInRange)
        {

        playerDir = gameManager.instance.transform.position - headPos.position;

        if(agent.remainingDistance < agent.stoppingDistance)
        {
            FacePlayer();
        }

        agent.SetDestination(gameManager.instance.player.transform.position);
        if (!isShooting)
        {
            StartCoroutine(shoot());
        }

        }
    }

    public void takeDamage(int dmg) //this make it that enemy takes damage
    {
        EnemyHP -= dmg;
        StartCoroutine(FlashHitColor());
        if (EnemyHP <= 0)
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
    public void FacePlayer()
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x, 0, playerDir.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * playerFaceSpeed);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
