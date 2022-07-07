using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorldSpawner : MonoBehaviour
{
    public Item item;
<<<<<<< Updated upstream

    private void Start()
    {
        ItemWorld.SpawnItemWorld(transform.position, item);
=======
    public Player player;

    private void Start()
    {
        ItemWorld.SpawnItemWorld(transform.position, new Item{isPrePlaced = true, itemType = item.itemType, amount = item.amount, itemText = item.itemText});
>>>>>>> Stashed changes
        Destroy(gameObject);
    }
}
