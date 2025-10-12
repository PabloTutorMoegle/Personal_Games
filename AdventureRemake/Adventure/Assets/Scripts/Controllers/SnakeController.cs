using UnityEngine;

public class SnakeController : MonoBehaviour
{
    public static event System.Action SnakeDamage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Snake Damage");
        SnakeDamage?.Invoke();
    }
}
