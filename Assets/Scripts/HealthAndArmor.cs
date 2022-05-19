using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthAndArmor : MonoBehaviour
{
    // Armor Text
    [SerializeField]
    private Text armorText;

    //Health Text
    [SerializeField]
    private Text healthText;

    //Health Value
    [SerializeField]
    private float health = 100;

    //Max Health Value
    [SerializeField]
    private float maxHealth = 100;

    //Armor Value
    [SerializeField]
    private float armor = 100;

    // Game Over Script
    private GameOver gOver;

    private Backpack backpack;

    /// <summary>
    /// Gets the GameOver script to end the game if the payer dies.
    /// </summary>
    private void Start()
    {
        backpack = GameObject.FindGameObjectWithTag("Backpack").GetComponent<Backpack>();
        health = backpack.health;
        armor = backpack.armor;

        gOver = GetComponent<GameOver>();

        armorText.text = $"{(int)armor}";
        healthText.text = $"{(int)health}";
    }

    /// <summary>
    /// Takes dammage based on amount of armor you have and ends the game if you have no health left.
    /// </summary>
    /// <param name="damage">Amount of dammage given</param>
    public void TakeDamage(float damage)
    {
        // Calculates dammage based on armor/
        if(armor >= damage *0.8)
        {
            armor -= (damage * 0.8f);
            health -= (damage * 0.2f);
        }
        else
        {
            damage -= armor;
            armor = 0;
            health -= damage;
        }

        // Updates texts.
        armorText.text = $"{(int)armor}";
        healthText.text = $"{(int)health}";

        // Ends the game if the player has no health left.
        if(health <= 0)
        {
            gOver.Over();
        }
    }

    /// <summary>
    /// Adds health to player.
    /// </summary>
    /// <param name="lives">Amount of health to add</param>
    public void AddHealth(float lives)
    {
        if((health+ lives) >= maxHealth)
        {
            health = maxHealth;
        }
        else health += lives;

        healthText.text = $"{(int)health}";
    }

    public float GetHealth()
    {
        return health;
    }

    public float GetArmor()
    {
        return armor;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }
}
