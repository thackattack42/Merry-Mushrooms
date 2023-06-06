using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwordPickup : MonoBehaviour
{
    [SerializeField] SwordStats sword;
    MeshFilter model;
    MeshRenderer mat;
    Texture texture;
    // Start is called before the first frame update
    void Start()
    {
        model = sword.model.GetComponent<MeshFilter>();
        mat = sword.model.GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.playerScript.swordPickup(sword);
            Destroy(gameObject);

        }
    }
}
