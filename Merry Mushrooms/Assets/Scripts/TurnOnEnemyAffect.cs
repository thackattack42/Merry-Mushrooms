using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnEnemyAffect : MonoBehaviour
{
    ParticleSystem _particleSystem;
    public bool onFire;
    public bool yes;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {

        onFire = gameManager.instance.playerScript.onFire;
       
        _particleSystem = GetComponent<ParticleSystem>();
        ChangeAffect();

       

        
        //else if (!gameManager.instance.playerScript.onFire)
        //{
        //    _particleSystem.Stop();
        //}



    }

    private void ChangeAffect()
    {
        if (onFire == true && gameObject.CompareTag("IceStatusAffect"))
        {
            _particleSystem.Stop();
        }
        else if(gameManager.instance.playerScript.onIce == true && gameObject.CompareTag("EarthStatusAffect"))
        {
            _particleSystem.Stop();
        }
        else
        {
            _particleSystem.Stop();
        }
    }
}
