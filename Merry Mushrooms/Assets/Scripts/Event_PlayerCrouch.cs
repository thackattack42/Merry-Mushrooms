using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Event_PlayerCrouch : MonoBehaviour
{
    [Header("When player crouches")]
    public UnityEvent crouch;

    private void Start()
    {
      if (crouch != null)
        {
            crouch = new UnityEvent();
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Crouch") && crouch != null)
        {
            crouch.Invoke();
        }
    }
}
