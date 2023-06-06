using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int maxItemStack;
    public InventorySlot[] InventorySlots;
    public GameObject invItemPrefab;
    public bool updated;


    [SerializeField] MeshFilter staffModel;
    [SerializeField] MeshRenderer staffMat;
    [SerializeField] MeshRenderer bowMat;
    [SerializeField] MeshFilter bowModel;
    [SerializeField] MeshRenderer swordMat;
    [SerializeField] MeshFilter swordModel;

    private void Update()
    {
        if (gameManager.instance.Inventory.activeSelf && !updated)
        {
            updated = true;
            UpdateWeaponInInv();
        }
        
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

    public void UpdateWeaponInInv()
    {
        if (gameManager.instance.playerScript.StaffEquipped)
        {
            staffModel.mesh = gameManager.instance.playerScript.GetStaffModel().sharedMesh;
            staffMat.material = gameManager.instance.playerScript.GetStaffMat().sharedMaterial;
        }
        else if (gameManager.instance.playerScript.BowEquipped)
        {
            bowModel.mesh = gameManager.instance.playerScript.GetBowModel().sharedMesh;
            bowMat.material = gameManager.instance.playerScript.GetBowMat().sharedMaterial;
        }
        else if (gameManager.instance.playerScript.SwordEquipped)
        {
            swordModel.mesh = gameManager.instance.playerScript.GetSwordModel().sharedMesh;
            swordMat.material = gameManager.instance.playerScript.GetSwordMat().sharedMaterial;
        }
        
    }
}
