using UnityEngine;

public class SnakeController : MonoBehaviour
{
    private HealthSystem _hs;
    private HealthBarSystem _hb;
    //Health variables
    public float maxHealth = 50;
    public float currentHealth;
    public static event System.Action SnakeDamage;

    private void Awake()
    {
        TryGetComponent<HealthSystem>(out _hs);
        TryGetComponent<HealthBarSystem>(out _hb);
    }
    private void Start()
    {
        _hs.SetMaxHealth(maxHealth);
        _hs.SetHealth(maxHealth);
        _hb.setMaxHealth(maxHealth);
        currentHealth = _hs.GetCurrentHealth();
    }
    private void Update()
    {
        currentHealth = _hs.GetCurrentHealth();
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Snake Damage");
        SnakeDamage?.Invoke();
    }
}
