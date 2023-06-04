using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Punch : MonoBehaviour
{
    [Header("----- Boss Stats -----")]
    [SerializeField] int punchDamage;

    [Header("----- Boss Components -----")]
    [SerializeField] Transform punchPos;

    bool collisionTriggered;

    // Start is called before the first frame update
    void Start()
    {
        punchPos.GetComponent<SphereCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Punch()
    {
        punchPos.GetComponent <SphereCollider>().enabled = true;
    }

    public void StopPunch()
    {
        punchPos.GetComponent<SphereCollider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (collisionTriggered)
        //    return;

        IDamage damagable = other.GetComponent<IDamage>();

        if (damagable != null)
        {
            damagable.takeDamage(punchDamage);
            //collisionTriggered = true;
        }
    }
}
