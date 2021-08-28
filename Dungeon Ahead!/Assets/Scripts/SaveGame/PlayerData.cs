using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    public PlayerData (Player player)
    {
        loadedLevel = player.gameObject.scene.name;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;

        storedInventory = player.inventory.GetItemList();

        maxHealth = player.GetComponent<PlayerStats>().maxHealth;
        health = player.GetComponent<PlayerStats>().health;
        maxEnergy = player.GetComponent<PlayerStats>().maxEnergy;
        energy = player.GetComponent<PlayerStats>().energy;

        storedQuests = player.GetComponent<PlayerStats>().questList;

        activeEffects = player.GetComponent<Effects>().activeEffects;
        timeCounter = player.GetComponent<Effects>().timeCounter;
    }

    public string loadedLevel;

    //PlayerPos
    public float[] position;

    //Inventory
    public List<Item> storedInventory;

    //PlayerStats
    public int maxHealth;
    public int health;
    public int maxEnergy;
    public int energy;
    public List<Quest> storedQuests;

    //Effects
    public List<int> activeEffects;
    public List<float> timeCounter;
}
