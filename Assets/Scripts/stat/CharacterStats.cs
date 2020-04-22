using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    // Health
    public int maxHealth = 100;
    public int currentHealth { get; private set; }
    //public Healthbar healthbar;

    public stat damage;
    public stat armor;

    public event System.Action<int, int> OnHealthChanged;

    // Set current health to max health
    // when starting the game.
    void Awake()
    {
        currentHealth = maxHealth;
        //healthbar.SetMaxHealth(maxHealth);
    }

    // Damage the character
    public void TakeDamage(int damage)
    {
        // Subtract the armor value
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        // Damage the character
        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");


        if (OnHealthChanged != null)
        {
            OnHealthChanged(maxHealth, currentHealth);
            //healthbar.SetMaxHealth(currentHealth);
        }

        // If health reaches zero
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        // Die in some way
        // This method is meant to be overwritten
        Debug.Log(transform.name + " died.");
    }


}
