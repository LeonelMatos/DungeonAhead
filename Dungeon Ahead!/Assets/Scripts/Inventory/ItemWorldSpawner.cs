using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorldSpawner : MonoBehaviour
{
    public Item item;

    private void Start()
    {
        ItemWorld.SpawnItemWorld(transform.position, new Item{isPrePlaced = true, itemType = item.itemType, amount = item.amount, itemText = item.itemText, x = gameObject.transform.position.x, y = gameObject.transform.position.y});
        Destroy(gameObject);
    }
}
