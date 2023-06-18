using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnParticle : MonoBehaviour
{

    ParticleSystem _particleSystem;
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        _particleSystem = GetComponent<ParticleSystem>();

        if (gameManager.instance.playerScript.SwordEquipped)
        {
            if(gameObject.CompareTag("FireWeaponAffect") && gameManager.instance.playerScript.SwordList[gameManager.instance.playerScript.selectedSword].fire)
            {
             _particleSystem.Play();
            }
            else if (gameObject.CompareTag("IceWeaponAffect") && gameManager.instance.playerScript.SwordList[gameManager.instance.playerScript.selectedSword].ice)
            {
                _particleSystem.Play();
            }
            else if (gameObject.CompareTag("EarthWeaponAffect") && gameManager.instance.playerScript.SwordList[gameManager.instance.playerScript.selectedSword].earth)
            {
                _particleSystem.Play();

            }
            else
            {
                _particleSystem.Stop();
            }
        //else if (gameManager.instance.playerScript.SwordEquipped && gameObject.CompareTag("IceWeaponAffect") && gameManager.instance.playerScript.SwordList[gameManager.instance.playerScript.selectedSword].ice)
        //{
        //    _particleSystem.Play();

        //}
        //else if (gameManager.instance.playerScript.SwordEquipped && gameObject.CompareTag("EarthWeaponAffect") && gameManager.instance.playerScript.SwordList[gameManager.instance.playerScript.selectedSword].earth)
        //{
        //_particleSystem.Play();
            

        }
        else if (gameManager.instance.playerScript.StaffEquipped /*&& gameObject.CompareTag("DefaultStaff") && gameManager.instance.playerScript.staffList[gameManager.instance.playerScript.selectedStaff].baseStaff*/)
        {
            if (gameObject.CompareTag("DefaultStaff") && gameManager.instance.playerScript.staffList[gameManager.instance.playerScript.selectedStaff].baseStaff)
            {

            _particleSystem.Play();
            }
            else if (gameObject.CompareTag("FireStaffAffect") && gameManager.instance.playerScript.staffList[gameManager.instance.playerScript.selectedStaff].fire)
            {
                _particleSystem.Play();

            }
            else if (gameObject.CompareTag("IceStaffAffect") && gameManager.instance.playerScript.staffList[gameManager.instance.playerScript.selectedStaff].ice)
            {
                _particleSystem.Play();

            }
            else if (gameObject.CompareTag("EarthStaffAffect") && gameManager.instance.playerScript.staffList[gameManager.instance.playerScript.selectedStaff].earth)
            {
                _particleSystem.Play();

            }
            else
            {
                _particleSystem.Stop();
            }


        }
        
        else if (gameManager.instance.playerScript.BowEquipped )
        {
            if (gameObject.CompareTag("FireBowAffect") && gameManager.instance.playerScript.BowList[gameManager.instance.playerScript.selectedBow].fire)
            {
                _particleSystem.Play();

            }
            else if (gameObject.CompareTag("IceBowAffect") && gameManager.instance.playerScript.BowList[gameManager.instance.playerScript.selectedBow].ice)
            {
                _particleSystem.Play();

            }
            else if (gameObject.CompareTag("EarthBowAffect") && gameManager.instance.playerScript.BowList[gameManager.instance.playerScript.selectedBow].earth)
            {
                _particleSystem.Play();

            }
            else
            {
                _particleSystem.Stop();
            }


        }
       
        
        //else
        //{

        //    _particleSystem.Stop();
        //}
    }
}
