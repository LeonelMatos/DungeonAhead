using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    private Slider healthSlider;
    private Slider energySlider;
    private Text textDisplay;

    public int maxHealth = 100;
    public int health = 0;

    public int maxEnergy = 100;
    public int energy = 0;

    public List<Quest> questList = new List<Quest>();

    private void Awake()
    {
        textDisplay = GameObject.FindGameObjectWithTag("TextDisplay").GetComponent<Text>();

        healthSlider = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Slider>();
        energySlider = GameObject.FindGameObjectWithTag("EnergyBar").GetComponent<Slider>();

        health = maxHealth; //Values changed from Start to Awake
        energy = maxEnergy;
        SetMaxHealth(health);
        SetMaxEnergy(energy);
    }
    private void Start()    //Will reset values when changing scenes
    {
        
    }

    private void Update()
    {
        textDisplay.text = health.ToString() + "\n\n" + energy.ToString();
        SetHealth(health);
        SetEnergy(energy);
        SetMaxHealth(maxHealth);
        SetMaxEnergy(maxEnergy);

        if (health <= 0)
            health = 0;
        else if (health > 100)
            health = 100;
        if (energy <= 0)
            energy = 0;
        else if (energy > 100)
            energy = 100;
    }

    /////////
    //Effects
    
    public void TakeDamage(int damage) => health -= damage;

    public void TakeEnergy(int damage) => energy -= damage;

    private Slider slider;

    public void SetMaxHealth(int health)
    {
        healthSlider.maxValue = health;
        //healthSlider.value = health;  //NOTE: idk why this was here,
                                        // it breaks the slider UI.
    }

    public void SetHealth(int health)
    {
        healthSlider.value = health;
    }
    public void SetMaxEnergy(int energy)
    {
        energySlider.maxValue = energy;
        energySlider.value = health;
    }

    public void SetEnergy(int energy)
    {
        energySlider.value = energy;
    }
}
