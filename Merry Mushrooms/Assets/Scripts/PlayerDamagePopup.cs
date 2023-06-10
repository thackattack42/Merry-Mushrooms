using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDamagePopup : MonoBehaviour
{
    public TextMeshProUGUI damageTaken;
    public Color textColor;

    float scrollSpeed = 20f;
    float fadeOutSpeed = 5f;
    float delTimer;
    void Awake()
    {

        if (gameManager.instance.playerHUD.GetDamageTaken() > 0)
        {
            textColor = Color.red;
            damageTaken.color = textColor;
            damageTaken.text = "-" + gameManager.instance.playerHUD.GetDamageTaken().ToString();
        }
        else
        {
            textColor = Color.green;
            damageTaken.color = textColor;
            damageTaken.text = "+" + gameManager.instance.playerHUD.GetDamageTaken().ToString();
        }
        delTimer = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, scrollSpeed) * Time.deltaTime;
        delTimer -= Time.deltaTime;
        if (delTimer <= 0)
        {
            textColor.a -= fadeOutSpeed * Time.deltaTime;
            damageTaken.color = textColor;
            if (textColor.a <= 0)
                Destroy(gameObject);
        }
    }
}
