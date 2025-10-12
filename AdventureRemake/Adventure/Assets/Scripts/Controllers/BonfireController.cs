using UnityEngine;

public class BonfireController : MonoBehaviour
{
    public static event System.Action HealBonfire;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Bonfire Healed");
        HealBonfire?.Invoke();
    }
}
