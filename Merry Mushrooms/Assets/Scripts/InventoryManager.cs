using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    public List<TabButton> tabButtons;
    public List<GameObject> tabObjects;
    public Color tabIdle;
    public Color tabHover;
    public Color tabActive;
    public TabButton selectedTab;
    public int maxItemStack;
    public InventorySlot[] InventorySlots;
    public GameObject invItemPrefab;
    public TextMeshProUGUI SkillPointCounter;
    
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
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            updated = false;
            gameManager.instance.UnpausedState();
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
                    //PlaceNewItem(item, InventorySlots[i]);
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
    public bool RemoveItem(Item item)
    {
        if (item.stackable == true)
        {
            for (int i = 0; i < InventorySlots.Length; i++)
            {
                InventoryDraggableItem itemInSlot = InventorySlots[i].GetComponentInChildren<InventoryDraggableItem>();
                if (itemInSlot != null && itemInSlot.item == item && itemInSlot.stackCount < maxItemStack)
                {
                    
                    itemInSlot.stackCount--;
                    if (itemInSlot.stackCount < 1)
                    {
                        Destroy(itemInSlot.gameObject);
                        
                    }
                    itemInSlot.UpdateStack();
                    return true;
                }
            }
        }
        else
        {
            for (int i = 0; i < InventorySlots.Length; i++)
            {
                InventoryDraggableItem itemInSlot = InventorySlots[i].GetComponentInChildren<InventoryDraggableItem>();
                if (itemInSlot != null && itemInSlot.item == item)
                {
                    Destroy(itemInSlot.gameObject);
                    return true;
                }
            }
        }


        
        return false;
    }
    public int GetStackCount(Item item)
    {
        if (item.stackable == true)
        {
            for (int i = 0; i < InventorySlots.Length; i++)
            {
                InventoryDraggableItem itemInSlot = InventorySlots[i].GetComponentInChildren<InventoryDraggableItem>();
                if (itemInSlot != null && itemInSlot.item == item && itemInSlot.stackCount < maxItemStack)
                {
                    return itemInSlot.stackCount;
                }
            }
        }
        else
        {
            for (int i = 0; i < InventorySlots.Length; i++)
            {
                InventoryDraggableItem itemInSlot = InventorySlots[i].GetComponentInChildren<InventoryDraggableItem>();
                if (itemInSlot != null && itemInSlot.item == item)
                {
                    return 1;
                }
            }
        }
        return 0;
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
    public void UpdateSkillPoints()
    {
        SkillPointCounter.text = gameManager.instance.playerScript.skillPoints.ToString();
    }

    //Inventory Tabs
    public void AddTab(TabButton btn)
    {
        if (tabButtons == null)
            tabButtons = new List<TabButton>();
        tabButtons.Add(btn);
    }
    public void OnTabEnter(TabButton btn)
    {
        ResetTabs();
        if (selectedTab == null || btn != selectedTab)
        {
            btn.tabImage.color = tabHover;
        }
    }
    public void OnTabExit(TabButton btn)
    {
        ResetTabs();
    }
    public void OnTabSelected(TabButton btn)
    {
        selectedTab = btn;
        ResetTabs();
        btn.tabImage.color = tabActive;
        int index = btn.transform.GetSiblingIndex();
        for (int i = 0; i < tabObjects.Count; i++)
        {
            if (i == index)
                tabObjects[i].SetActive(true);
            else
                tabObjects[i].SetActive(false);
        }
    }
    public void ResetTabs()
    {
        foreach (TabButton btn in tabButtons)
        {
            if (selectedTab != null && btn == selectedTab)
                continue;
            btn.tabImage.color = tabIdle;
        }
    }
}
