using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [Header("-----Mouse Sensitivity-----")]
    public float sensX;
    public float sensY;

    public Transform orientation;
    public GameObject obj;
    float xRotation;
    float yRotation;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
       // Get mouse input
       float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
       float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        
        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90f);

        //rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        //orientation.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.parent.Rotate(Vector3.up * mouseX);
        //obj.transform.parent.Rotate(Vector3.up * mouseX);

    }
}
