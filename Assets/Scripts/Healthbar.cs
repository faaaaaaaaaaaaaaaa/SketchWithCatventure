using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class Healthbar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    
    public void SetHealth(int health)
    {
        slider.value = health;
    }

    void Start()
    {
        //GetComponent<CharacterStats>().OnHealthChanged += HealthChanged;
    }
     /*void HealthChanged(int maxHealth, int currentHealth)
    {
 
            float healthPercent = (float)currentHealth / maxHealth;
            
            
        
     }*/
}
