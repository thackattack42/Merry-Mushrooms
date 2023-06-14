using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEditor.ShaderKeywordFilter;

public class InventoryDraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Item item;
    public TextMeshProUGUI countText;
    public int stackCount = 1;
    public Image img;
    public Transform newParent;

    public void InitializeItem(Item newItem)
    {
        item = newItem;
        img.sprite = newItem.sprite;
        UpdateStack();
    }

    public void UpdateStack()
    {
        if (stackCount == 1)
            countText.text = "";
        else
            countText.text = stackCount.ToString();

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        newParent = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        img.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(newParent);
        img.raycastTarget = true;
    }
    public void OnMouseDown(1)
    {
        if (item.name == "Health Potion")
        {
            gameManager.instance.playerScript.takeDamage(-30);
            if (gameManager.instance.playerScript.HP > gameManager.instance.playerScript.maxHP)
            {
                gameManager.instance.playerScript.HP = gameManager.instance.playerScript.maxHP;
                gameManager.instance.playerHUD.updatePlayerHealth(0);
            }
        }
        else if (item.name == "Mana Potion")
        {
            gameManager.instance.playerScript.MP += 50;
            if (gameManager.instance.playerScript.MP > gameManager.instance.playerScript.maxMP)
            {

            }
        }
    }
}
