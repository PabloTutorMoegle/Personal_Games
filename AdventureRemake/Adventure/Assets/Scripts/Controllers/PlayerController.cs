using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;
using UnityEngine.InputSystem.Controls;

public class PlayerController : MonoBehaviour
{
    private MovementSystem _mv;
    private HealthSystem _hs;
    private HealthBarSystem _hb;
    private PlayerInventorySystem _pi;

    //Health variables
    public float maxHealth = 100;
    public float currentHealth;
    public float healBonfireAmount = 100;
    public float healFoodAmount = 20;
    public float damageAmount = 20;

    //Movement variables
    public float moveSpeed = 5f;
    public Vector3 moveDirection;


    private void Awake()
    {
        TryGetComponent<MovementSystem>(out _mv);
        TryGetComponent<HealthSystem>(out _hs);
        TryGetComponent<HealthBarSystem>(out _hb);
        TryGetComponent<PlayerInventorySystem>(out _pi);
    }

    private void Start()
    {
        _hs.SetMaxHealth(maxHealth);
        _hb.setMaxHealth(maxHealth);
        _hs.SetHealth(maxHealth);
        currentHealth = _hs.GetCurrentHealth();
    }

    private void Update()
    {
        //Player movement
        if (Keyboard.current.wKey.isPressed)
        {
            moveDirection.y = 1;
        }
        else if (Keyboard.current.sKey.isPressed)
        {
            moveDirection.y = -1;
        }
        else
        {
            moveDirection.y = 0;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            moveDirection.x = -1;
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            moveDirection.x = 1;
        }
        else
        {
            moveDirection.x = 0;
        }
        float horizontal = moveDirection.x;
        float vertical = moveDirection.y;
        moveDirection = new Vector3(horizontal, vertical, 0).normalized;
        _mv.MoveLinearVelocity(moveDirection, moveSpeed);

        //Consume Potions
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (_pi.HasItem("Potion"))
            {
                bool removed = _pi.RemoveItem("Potion", 1);
                if (removed)
                {
                    _hs.Heal(50);
                    currentHealth = _hs.GetCurrentHealth();
                    _hb.setHealth(currentHealth);
                    Debug.Log("Consumed a Potion. Current Health: " + currentHealth);
                }
            }
            else
            {
                Debug.Log("No Potions in inventory.");
            }
        }
    }
    public PlayerInventorySystem GetPlayerInventory()
    {
        return _pi;
    }   

    private void OnEnable()
    {
        BonfireController.HealBonfire += HealFromBonfire;
        FoodController.HealFood += HealFromFood;
        SnakeController.SnakeDamage += DamageFromSnake;
        CollectibleSystem.GetCollectible += AddToInventory;
    }
    private void OnDisable()
    {
        BonfireController.HealBonfire -= HealFromBonfire;
        FoodController.HealFood -= HealFromFood;
        SnakeController.SnakeDamage -= DamageFromSnake;
        CollectibleSystem.GetCollectible -= AddToInventory;
    }
    private void HealFromBonfire()
    {
        _hs.Heal(healBonfireAmount);
        currentHealth = _hs.GetCurrentHealth();
        _hb.setHealth(currentHealth);
    }
    private void HealFromFood()
    {
        _hs.Heal(healFoodAmount);
        currentHealth = _hs.GetCurrentHealth();
        _hb.setHealth(currentHealth);
    }
    private void DamageFromSnake()
    {
        _hs.Hurt(damageAmount);
        currentHealth = _hs.GetCurrentHealth();
        _hb.setHealth(currentHealth);
    }
    private void AddToInventory(string collectibleType, int amount, Sprite icon)
    {
        _pi.AddItem(collectibleType, amount, icon);
        Debug.Log("Added to Inventory: " + collectibleType + " x" + amount);
    }
}
