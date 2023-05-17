using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void PlayerCrouch();
    public static event PlayerCrouch Crouch;

    void OnPlayerCrouch()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            if (Crouch != null)
                Crouch();
        }
    }
}
