using System;
using System.Collections;
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
    private Animator _anim;
    private SpriteRenderer _sr;
    private bool _isAttacking = false;

    //Health variables
    public float maxHealth = 100;
    public float currentHealth;

    //Movement variables
    public float moveSpeed = 5f;
    public Vector3 moveDirection;


    private void Awake()
    {
        TryGetComponent<MovementSystem>(out _mv);
        TryGetComponent<HealthSystem>(out _hs);
        TryGetComponent<HealthBarSystem>(out _hb);
        TryGetComponent<PlayerInventorySystem>(out _pi);
        TryGetComponent<Animator>(out _anim);
        TryGetComponent<SpriteRenderer>(out _sr);
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
        if (Keyboard.current.wKey.isPressed && _isAttacking == false)
        {
            moveDirection.y = 1;
            _anim.SetFloat("Yinput", moveDirection.y);
            _anim.SetFloat("Xinput", 0);
        }
        else if (Keyboard.current.sKey.isPressed && _isAttacking == false)
        {
            moveDirection.y = -1;
            _anim.SetFloat("Yinput", moveDirection.y);
            _anim.SetFloat("Xinput", 0);
        }
        else
        {
            moveDirection.y = 0;
        }

        if (Keyboard.current.aKey.isPressed && _isAttacking == false)
        {
            moveDirection.x = -1;
            _anim.SetFloat("Xinput", moveDirection.x);
            _anim.SetFloat("Yinput", 0);
            _sr.flipX = true;
        }
        else if (Keyboard.current.dKey.isPressed && _isAttacking == false)
        {
            moveDirection.x = 1;
            _anim.SetFloat("Xinput", moveDirection.x);
            _anim.SetFloat("Yinput", 0);
            _sr.flipX = false;
        }
        else
        {
            moveDirection.x = 0;
        }
        
        moveDirection = new Vector3(moveDirection.x, moveDirection.y, 0).normalized;
        _mv.MoveLinearVelocity(moveDirection, moveSpeed);

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            _anim.SetFloat("Xinput", 1);
            _anim.SetFloat("Yinput", 1);
            _isAttacking = true;

            IEnumerator ResetInputs()
            {
                yield return new WaitForSeconds(0.3f);
                _anim.SetFloat("Xinput", 0);
                _anim.SetFloat("Yinput", 0);
                _isAttacking = false;
            }

            StartCoroutine(ResetInputs());
        }

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
}
