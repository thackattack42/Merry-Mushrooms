using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NPCTextBubbles : MonoBehaviour
{
    [Header("-----NPC Type-----")]
    public NPCType type;
    [Header("-----Chat Bubble Grabbers-----")]
    public GameObject ChatBubble;
    public GameObject QuestBubble;
    public GameObject ShopBubble;
    

    public enum NPCType
    {
        Quest, Shop, Chat
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.rotation = gameManager.instance.player.transform.rotation;
            if (Input.GetKeyDown(KeyCode.F) && gameManager.instance.activeMenu == null)
            {
                gameManager.instance.isPaused = !gameManager.instance.isPaused;
                gameManager.instance.activeMenu = gameManager.instance.shopMenu;
                gameManager.instance.activeMenu.SetActive(gameManager.instance.isPaused);
                gameManager.instance.PauseState();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (type == NPCType.Quest)
                QuestBubble.SetActive(true);
            else if (type == NPCType.Shop)
                ShopBubble.SetActive(true);
            else
                ChatBubble.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            QuestBubble.SetActive(false);
            ShopBubble.SetActive(false);
            ChatBubble.SetActive(false);
        }
    }

}
