using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]

public class BossSkillAoeDmg : MonoBehaviour
{
    [SerializeField] StatusEffectData data;
    public float moveSpeed = 2f;

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
        var effectable = other.GetComponent<IEffectable>();

        if (effectable != null)
        {
            effectable.ApplyEffect(data);
        }

        Destroy(this.gameObject);
    }
}
