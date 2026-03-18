using System.Collections;
using UnityEngine;

public class GrocerySlot : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem newItemParticleSystem;
    [SerializeField]
    private ParticleSystem removeItemParticleSystem;

    public GroceryItem CurrentItem { get; private set; }
    public Collider CurrentItemCollider { get; private set; }

    private void Awake()
    {
        newItemParticleSystem.Stop();
        removeItemParticleSystem.Stop();
    }

    internal void PlaceItem(GroceryItem groceryItem, Vector3 originPosition)
    {
        var newItem = Instantiate(groceryItem.ItemPrefab, originPosition, Quaternion.identity, transform);
        CurrentItem = newItem.GetComponentInChildren<GroceryItem>();
        CurrentItem.enabled = false; // Disable interaction while animating
        CurrentItemCollider = CurrentItem.GetComponent<Collider>();
        CurrentItemCollider.enabled = false; // Disable collider while animating
        AnimatePlacingItem(originPosition);
        newItemParticleSystem.Stop();
        removeItemParticleSystem.Stop();
    }

    private void OnMouseDown()
    {
        if (CurrentItem != null)
        {
            Debug.Log("Grocery slot clicked, but it already has an item!");
            ShopController.Instance.RemoveItem(CurrentItem);
            DestroyImmediate(CurrentItem.transform.parent.gameObject);
            removeItemParticleSystem.Play();
            CurrentItem = null;
        }
        else
        {
            Debug.Log("Grocery slot clicked and is empty!");
        }
    }

    private void AnimatePlacingItem(Vector3 originPosition)
    {
        var animationSpeed = 1f;
        StartCoroutine(AnimateItemMovement(originPosition, transform.position, animationSpeed));
    }

    IEnumerator AnimateItemMovement(Vector3 start, Vector3 end, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            CurrentItem.transform.parent.position = Vector3.Lerp(start, end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = end;
        newItemParticleSystem.Play();
    }
}