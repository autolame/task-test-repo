using UnityEngine;

public class GroceryItem : MonoBehaviour
{
    [SerializeField]
    private string itemName;
    [SerializeField]
    private float price;
    [SerializeField]
    private GameObject itemPrefab;

    public float Price { get => price; }
    public GameObject ItemPrefab { get => itemPrefab; }

    private void OnMouseDown()
    {
        Debug.Log($"Grocery item '{itemName}' clicked!");
        ShopController.Instance.AddItem(this);
    }
}
