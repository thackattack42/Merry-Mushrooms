using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NPCTextBubbles : MonoBehaviour
{
    public GameObject ChatBubble;
    public GameObject QuestBubble;
    public GameObject QuestReturnBubble;
    public GameObject ShopBubble;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = gameManager.instance.player.transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //if (has a quest to give)
                //QuestBubble.SetActive(true);
            //else if (player has quest ready to turn in)
                //QuestReturnBubble.SetActive(true);
            //else if (is shopkeeper)
                //ShopBubble.SetActive(true);
            //else
                ChatBubble.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            QuestBubble.SetActive(false);
            QuestReturnBubble.SetActive(false);
            ShopBubble.SetActive(false);
            ChatBubble.SetActive(false);
        }
    }
}
