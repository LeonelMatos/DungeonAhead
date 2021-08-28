using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class VectorValue : ScriptableObject
{
    public Vector2 initialValue;

    //Scene
    public bool isFirstScene;

    //Inventory
    public List<Item> storedInventory;

    //PlayerStats
    public int maxHealth;
    public int health;
    public int MaxEnergy;
    public int energy;
    public List<Quest> storedQuests;

    //Effects
    public List<int> activeEffects;
    public List<float> timeCounter;
}
