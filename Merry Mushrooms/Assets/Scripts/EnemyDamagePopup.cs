using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyDamagePopup : MonoBehaviour
{
    // Start is called before the first frame update
    public EnemyUI parent;
    public TextMeshProUGUI damageTaken;
    public Color textColor;

    float scrollSpeed = 0.5f;
    float fadeOutSpeed = 5f;
    float delTimer;
    void Awake()
    {
        parent = gameObject.GetComponentInParent<EnemyUI>();

        if (parent.enemyScript.HP < parent.GetUIHPVal())
        {
            textColor = Color.red;
            damageTaken.color = textColor;
            damageTaken.text = "-" + (parent.GetUIHPVal() - parent.enemyScript.HP);
        }
        else
        {
            textColor = Color.green;
            damageTaken.color = textColor;
            damageTaken.text = "+" + (parent.GetUIHPVal() + parent.enemyScript.HP);
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
