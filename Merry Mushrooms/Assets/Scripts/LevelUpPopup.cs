using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUpPopup : MonoBehaviour
{
    public TextMeshProUGUI lvlUp;
    public Color textColor;

    float scrollSpeed = 20f;
    float fadeOutSpeed = 5f;
    float delTimer;
    void Awake()
    {
        textColor = lvlUp.color;
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
            lvlUp.color = textColor;
            if (textColor.a <= 0)
                Destroy(gameObject);
        }
    }
}
