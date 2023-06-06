using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int maxItemStack;
    public InventorySlot[] InventorySlots;
    public GameObject invItemPrefab;


    [SerializeField] MeshFilter staffModel;
    [SerializeField] MeshRenderer staffMat;
    [SerializeField] MeshRenderer bowMat;
    [SerializeField] MeshFilter bowModel;
    [SerializeField] MeshRenderer swordMat;
    [SerializeField] MeshFilter swordModel;

    private void Start()
    {
        UpdateWeaponInInv();
    }
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

    void UpdateWeaponInInv()
    {
        if (gameManager.instance.playerScript.StaffEquipped)
        {
            staffModel = gameManager.instance.playerScript.GetStaffModel();
            staffMat = gameManager.instance.playerScript.GetStaffMat();
        }
        else if (gameManager.instance.playerScript.BowEquipped)
        {
            bowModel = gameManager.instance.playerScript.GetBowModel();
            bowMat = gameManager.instance.playerScript.GetBowMat();
        }
        else if (gameManager.instance.playerScript.SwordEquipped)
        {
            swordModel = gameManager.instance.playerScript.GetSwordModel();
            swordMat = gameManager.instance.playerScript.GetSwordMat();
        }
        
    }
}
