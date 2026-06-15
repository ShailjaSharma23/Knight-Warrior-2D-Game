using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);
        Debug.Log("Current Health: " + currentHealth);

        if (currentHealth>0)
        {
            // play hurt animation
        }
        else
        {
            // player dead
        }
    }

    // private void Update()
    // {
    //     if (Keyboard.current.eKey.wasPressedThisFrame)
    //     {
    //         TakeDamage(1);
    //         Debug.Log("key pressed");
    //     }
    // }
}
