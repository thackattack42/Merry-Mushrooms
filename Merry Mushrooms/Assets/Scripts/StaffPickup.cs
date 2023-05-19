using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffPickup : MonoBehaviour
{

    [SerializeField] Staff_Stats staff;
    MeshFilter model;
    MeshRenderer mat;
    Texture texture;
    // Start is called before the first frame update
    void Start()
    {
        model = staff.model.GetComponent<MeshFilter>();
        mat = staff.model.GetComponent<MeshRenderer>();
       // texture = staff.model.GetComponent<Texture>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.playerScript.staffPickup(staff);
            Destroy(gameObject);

        }
    }
}
