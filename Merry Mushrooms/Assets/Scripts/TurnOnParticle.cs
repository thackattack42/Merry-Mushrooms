using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnParticle : MonoBehaviour
{

    ParticleSystem _particleSystem;
    // Start is called before the first frame update
    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if(gameManager.instance.playerScript.SwordEquipped && gameObject.CompareTag("FireWeaponAffect") && gameManager.instance.playerScript.SwordList[gameManager.instance.playerScript.selectedSword].fire)
        {
        _particleSystem.Play();

        }
        else if (gameManager.instance.playerScript.SwordEquipped && gameObject.CompareTag("IceWeaponAffect") && gameManager.instance.playerScript.SwordList[gameManager.instance.playerScript.selectedSword].ice)
        {
            _particleSystem.Play();

        }
        else if (gameManager.instance.playerScript.SwordEquipped && gameObject.CompareTag("EarthWeaponAffect") && gameManager.instance.playerScript.SwordList[gameManager.instance.playerScript.selectedSword].earth)
        {
        _particleSystem.Play();
            

        }
        else
        {

            _particleSystem.Stop();
        }
    }
}
