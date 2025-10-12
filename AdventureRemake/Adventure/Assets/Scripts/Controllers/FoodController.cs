using UnityEngine;

public class FoodController : MonoBehaviour
{
    public static event System.Action HealFood;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Food Healed");
        HealFood?.Invoke();
        Destroy(gameObject);
    }
}
