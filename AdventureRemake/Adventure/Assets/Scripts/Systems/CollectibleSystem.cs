using UnityEngine;

public class CollectibleSystem : MonoBehaviour
{
    public string collectibleType;
    public int amount = 1;
    public Sprite icon;
    public static event System.Action<string, int, Sprite> GetCollectible;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Collected");
            GetCollectible?.Invoke(collectibleType, amount, icon);
            Destroy(gameObject);
        }
    }
}
