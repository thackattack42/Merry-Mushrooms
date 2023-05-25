using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkillAoeDmg : MonoBehaviour
{
    [SerializeField] StatusEffectData data;

    public void Start()
    {
    }

    void Update()
    {
        // This would move it away from boss
        //transform.position += transform.forward * moveSpeed * Time.deltaTime;
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
