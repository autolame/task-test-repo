using UnityEngine;

public class CashierController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private void OnMouseDown()
    {
        Debug.Log("Cashier clicked!");
        animator.SetTrigger("Wave");
    }
}