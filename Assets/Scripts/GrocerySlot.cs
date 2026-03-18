using UnityEngine;

public class GrocerySlot : MonoBehaviour
{
    public GroceryItem CurrentItem { get; private set; }

    internal void PlaceItem(GroceryItem groceryItem)
    {
        var newItem = Instantiate(groceryItem.ItemPrefab, transform.position, Quaternion.identity, transform);
        CurrentItem = newItem.GetComponentInChildren<GroceryItem>();
    }

    private void OnMouseDown()
    {
        if (CurrentItem != null)
        {
            Debug.Log("Grocery slot clicked, but it already has an item!");
            ShopController.Instance.RemoveItem(CurrentItem);
            DestroyImmediate(CurrentItem.gameObject);
            CurrentItem = null;
        }
        else
        {
            Debug.Log("Grocery slot clicked and is empty!");
        }
    }
}