using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunGilPickup_Script : MonoBehaviour
{
    [SerializeField] public AudioSource aud;
    [SerializeField] public AudioClip[] audPickup;
    [SerializeField] public float audPickupVol;
    [Range(1, 100)][SerializeField] public int amount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (aud != null && audPickup != null)
                aud.PlayOneShot(audPickup[Random.Range(0, audPickup.Length)], audPickupVol);

            gameManager.instance.playerHUD.addFunGil(amount);

            Destroy(gameObject);
        }
    }
}
