using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    public GameObject BuySellMenu;
    public GameObject BuyMenu;
    public GameObject SellMenu;
    public TextMeshProUGUI buyHealthPotCount;
    public TextMeshProUGUI buyManaPotCount;
    public TextMeshProUGUI sellHealthPotCount; //I'm lazy, so sue me!
    public TextMeshProUGUI sellManaPotCount;
    public TextMeshProUGUI FunGilCount;
    public TextMeshProUGUI warningText;
    public Item HealthPotion;
    public Item ManaPotion;

    public void BuyButton()
    {
        gameManager.instance.buttons.UIAudio.PlayOneShot(gameManager.instance.buttons.MenuButtonClick);
        BuySellMenu.SetActive(false);
        BuyMenu.SetActive(true);
        UpdateBuyHealthPotCount();
        UpdateBuyManaPotCount();
    }
    public void SellButton()
    {
        gameManager.instance.buttons.UIAudio.PlayOneShot(gameManager.instance.buttons.MenuButtonClick);
        BuySellMenu.SetActive(false);
        SellMenu.SetActive(true);
        UpdateSellHealthPotCount();
        UpdateSellManaPotCount();
        
    }
    public void ExitButton()
    {
        gameManager.instance.buttons.UIAudio.PlayOneShot(gameManager.instance.buttons.MenuButtonClick);
        gameManager.instance.UnpausedState();
    }
    public void BackButton()
    {
        gameManager.instance.buttons.UIAudio.PlayOneShot(gameManager.instance.buttons.MenuButtonClick);
        BuyMenu.SetActive(false);
        SellMenu.SetActive(false);
        BuySellMenu.SetActive(true);
    }
    public void BuyHealthPot()
    {
        if (gameManager.instance.playerHUD.GetFunGil() >= 100)
        {
            if (gameManager.instance.invManager.AddItem(HealthPotion))
            {
                UpdateBuyHealthPotCount();
                gameManager.instance.playerHUD.addFunGil(-100);
                FunGilCount.text = gameManager.instance.playerHUD.GetFunGil().ToString();
            }
            else
                StartCoroutine(InventoryFull());
        }
        else
            StartCoroutine(NotEnoughMoney());
        
    }
    public void BuyManaPot()
    {
        if (gameManager.instance.playerHUD.GetFunGil() >= 100)
        {
            if (gameManager.instance.invManager.AddItem(ManaPotion))
            {
                UpdateBuyManaPotCount();
                gameManager.instance.playerHUD.addFunGil(-100);
                FunGilCount.text = gameManager.instance.playerHUD.GetFunGil().ToString();
            }
            else
                StartCoroutine(InventoryFull());
        }
        else
            StartCoroutine(NotEnoughMoney());
    }
    public void SellHealthPot()
    {
        if (gameManager.instance.invManager.RemoveItem(HealthPotion))
        {
            UpdateSellHealthPotCount();
            gameManager.instance.playerHUD.addFunGil(50);
            FunGilCount.text = gameManager.instance.playerHUD.GetFunGil().ToString();
        }
        else
            StartCoroutine(CantSell());
    }
    public void SellManaPot()
    {
        if (gameManager.instance.invManager.RemoveItem(ManaPotion))
        {
            UpdateSellManaPotCount();
            gameManager.instance.playerHUD.addFunGil(50);
            FunGilCount.text = gameManager.instance.playerHUD.GetFunGil().ToString();
        }
        else
            StartCoroutine(CantSell());
    }

    IEnumerator NotEnoughMoney()
    {
        StopCoroutine(CantSell());
        StopCoroutine(InventoryFull());
        warningText.text = "Not enough FunGils!";
        warningText.enabled = true;
        yield return new WaitForSeconds(2);
        warningText.enabled = false;
    }
    IEnumerator CantSell()
    {
        StopCoroutine(InventoryFull());
        StopCoroutine(NotEnoughMoney());
        warningText.text = "Nothing to Sell!";
        warningText.enabled = true;
        yield return new WaitForSeconds(2);
        warningText.enabled = false;
    }IEnumerator InventoryFull()
    {
        StopCoroutine(CantSell());
        StopCoroutine(NotEnoughMoney());
        warningText.text = "Inventory Full!";
        warningText.enabled = true;
        yield return new WaitForSeconds(2);
        warningText.enabled = false;
    }

    void UpdateBuyHealthPotCount()
    {
        buyHealthPotCount.text = "x" + gameManager.instance.invManager.GetStackCount(HealthPotion);
    }
    void UpdateSellHealthPotCount()
    {
        sellHealthPotCount.text = "x" + gameManager.instance.invManager.GetStackCount(HealthPotion);
    }
    void UpdateBuyManaPotCount()
    {
        buyManaPotCount.text = "x" + gameManager.instance.invManager.GetStackCount(ManaPotion);
    }
    void UpdateSellManaPotCount()
    {
        sellManaPotCount.text = "x" + gameManager.instance.invManager.GetStackCount(ManaPotion);
    }
}
