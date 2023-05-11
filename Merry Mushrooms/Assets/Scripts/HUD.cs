using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameManager.instance.HPSlider.fillAmount = 1f;
        gameManager.instance.dashCooldownCounter.text = "";
        gameManager.instance.dashCooldownSlider.fillAmount = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
