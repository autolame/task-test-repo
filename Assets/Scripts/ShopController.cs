using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public static ShopController Instance { get; private set; }

    [SerializeField]
    private List<GroceryItem> groceryItems;
    [SerializeField]
    private List<GrocerySlot> grocerySlots;

    [SerializeField]
    private float totalPrice;
    [SerializeField]
    private GameObject totalPriceGameObject;
    [SerializeField]
    private TextMeshProUGUI totalPriceText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        totalPriceGameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        Debug.Log("Shop clicked!");

        ShowPricesOnGUI();
        if (groceryItems.Count > 0)
        {
            totalPriceGameObject.SetActive(!totalPriceGameObject.activeSelf);
        }
    }

    private void ShowPricesOnGUI()
    {
        totalPriceText.text = string.Empty;

        foreach (var item in groceryItems)
        {
            totalPriceText.text += $"{item.ItemName}: ${item.Price}\n";
        }
        totalPriceText.text += $"\n\n Total: ${totalPrice}";

        if (groceryItems.Count == 0)
        {
            totalPriceGameObject.SetActive(false);
        }
    }

    public void AddItem(GroceryItem groceryItem)
    {
        foreach (var slot in grocerySlots)
        {
            if (slot.CurrentItem == null)
            {
                slot.PlaceItem(groceryItem);

                if (groceryItems.Count <= grocerySlots.Count)
                {
                    groceryItems.Add(slot.CurrentItem);
                }
                totalPrice = CalculateTotalPrice();
                ShowPricesOnGUI();
                break;
            }
        }
    }

    public void RemoveItem(GroceryItem groceryItem)
    {
        groceryItems.Remove(groceryItem);
        totalPrice = CalculateTotalPrice();
        ShowPricesOnGUI();
    }

    private float CalculateTotalPrice()
    {
        float total = 0f;
        foreach (var item in groceryItems)
        {
            total += item.Price;
        }
        return total;
    }
}