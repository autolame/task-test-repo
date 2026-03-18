using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public static ShopController Instance { get; private set; }

    [SerializeField]
    private List<GroceryItem> groceryItems;
    [SerializeField]
    private List<GrocerySlot> grocerySlots;

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
                break;
            }
        }
    }

    public void RemoveItem(GroceryItem groceryItem)
    {
        groceryItems.Remove(groceryItem);
    }
}