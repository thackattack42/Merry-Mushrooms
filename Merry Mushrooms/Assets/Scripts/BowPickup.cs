using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowPickup : MonoBehaviour
{
    [SerializeField] BowStats bow;
    MeshFilter model;
    MeshRenderer mat;
    Texture texture;
    // Start is called before the first frame update
    void Start()
    {
        model = bow.model.GetComponent<MeshFilter>();
        mat = bow.model.GetComponent<MeshRenderer>();
       
        bow.ammoClip = bow.origAmmo;
        bow.ammoReserves = bow.origAmmoReserves;
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.playerScript.BowPickup(bow);
            Destroy(gameObject);

        }
    }
}
