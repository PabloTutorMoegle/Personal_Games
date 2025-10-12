using UnityEngine;

public class CastleController : MonoBehaviour
{
    public static event System.Action GameFinished;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var playerInventory = collision.GetComponent<PlayerInventorySystem>();
            bool hasItems = false;
            if (playerInventory != null)
            {
                hasItems = playerInventory.HasItem("Key") && playerInventory.HasItem("Chest");
            }
            if (hasItems)
            {
                Debug.Log("Player has reached the castle with required items. Game Finished!");
                GameFinished?.Invoke();
            }
            else
            {
                Debug.Log("Player reached the castle but is missing required items.");
            }
        }
    }
}
