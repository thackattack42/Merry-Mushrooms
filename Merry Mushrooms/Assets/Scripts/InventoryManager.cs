using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] public int maxItemStack;
    public InventorySlot[] InventorySlots;
    public GameObject invItemPrefab;
    public bool AddItem(Item item)
    {
        //find existing item if stackable
        if (item.stackable == true)
        {
            for (int i = 0; i < InventorySlots.Length; i++)
            {
                InventoryDraggableItem itemInSlot = InventorySlots[i].GetComponentInChildren<InventoryDraggableItem>();
                if (itemInSlot != null && itemInSlot.item == item && itemInSlot.stackCount < maxItemStack)
                {
                    PlaceNewItem(item, InventorySlots[i]);
                    itemInSlot.stackCount++;
                    itemInSlot.UpdateStack();
                    return true;
                }
            }
        }

        //find empty slot
        for (int i = 0; i < InventorySlots.Length; i++)
        {
            InventoryDraggableItem itemInSlot = InventorySlots[i].GetComponentInChildren<InventoryDraggableItem>();
            if (itemInSlot == null)
            {
                PlaceNewItem(item, InventorySlots[i]);
                return true;
            }
        }
        return false;
    }

    void PlaceNewItem(Item item, InventorySlot slot)
    {
        GameObject newItem = Instantiate(invItemPrefab, slot.transform);
        InventoryDraggableItem invIitem = newItem.GetComponent<InventoryDraggableItem>();
        invIitem.InitializeItem(item);
    }
}
