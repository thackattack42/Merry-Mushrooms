using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect__ICEslow : MonoBehaviour//, //IPhysics
{
    PlayerController player;
    [SerializeField] int pushAmount;
    private void Start()
    {
        player = GetComponent<PlayerController>();
    }
    public void TakeIceSlowEffect()
    {
        IPhysics physicable = GetComponent<IPhysics>();
        if(physicable != null)
        {
            Vector3 dir = player.transform.position - transform.position;
            physicable.KnockBack(dir * pushAmount);
        }
    }
    IEnumerable iceslow()
    {
        //player.
        yield return new WaitForSeconds(0.5f);
    }
}
