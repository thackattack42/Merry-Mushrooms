using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkillAoeDmg : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float DotAmount;
    public float tickSpeed;
    public float statusLasts;

    public GameObject effectParticles;


    public SphereCollider sphereCol;

    public void Awake()
    {
        sphereCol = GetComponent<SphereCollider>();
        sphereCol.isTrigger = true;
        sphereCol.radius = 1f;
    }

    void Update()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            return;
        }

        Destroy(this.gameObject);
    }
}
