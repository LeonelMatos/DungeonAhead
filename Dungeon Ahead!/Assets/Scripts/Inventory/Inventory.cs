using System;
using System.Collections.Generic;
using UnityEngine;


public class Inventory
{
    public PlayerStats playerStats; //Reference on InventoryUI Awake()
    public event EventHandler OnItemListChanged;
    public List<Item> itemList;
    private Action<Item> useItemAction;
    private QuestGoal questGoal;

    public void GetPlayer(GameObject player)
    {
        playerStats = player.GetComponent<PlayerStats>();

        itemList = player.GetComponent<Player>().startingPosition.storedInventory;
    }

    public Inventory(Action<Item> useItemAction)
    {
        this.useItemAction = useItemAction;
        itemList = new List<Item>();

        //AddItem(new Item { itemType = Item.ItemType.Sword, amount = 1 });
        //AddItem(new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });
        //AddItem(new Item { itemType = Item.ItemType.ManaPotion, amount = 1 });

        //Debug.Log(itemList.Count + " items in inventory");
    }

    public void AddItem(Item item)
    {
        if (item.IsStackable())
        {
            bool itemAlreadyInInventory = false;
            foreach(Item inventoryItem in itemList)
            {
                if(inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }
            if (!itemAlreadyInInventory)
            {
                itemList.Add(item);
            }
        }
        else
        {
            itemList.Add(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);

        foreach (Quest quest in playerStats.questList.ToArray())
        {
            //Debug.Log("Searching " + quest.title);
            if (quest.isActive && quest.goal.goalType == GoalType.Gathering)
                quest.goal.ItemCollected(quest, playerStats);
        }
    }

    public void RemoveItem(Item item)
    {
        if (item.IsStackable()) {
            Item itemInInventory = null;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount -= item.amount;
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory != null && itemInInventory.amount <= 0)
            {
                itemList.Remove(itemInInventory);
            }
        }
        else
        {
            itemList.Remove(item);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
        return;
    }

    public void AddItem_Object(Item item)
    {
        if (item.IsStackable())
        {
            bool itemAlreadyInInventory = false;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount += item.amount;
                    itemAlreadyInInventory = true;
                }
            }
            if (!itemAlreadyInInventory)
            {
                itemList.Add(item);
            }
        }
        else
        {
            itemList.Add(item);
        }
    }

    public void RemoveItem_Object(Item item)
    {
        if (item.IsStackable())
        {
            Item itemInInventory = null;
            foreach (Item inventoryItem in itemList)
            {
                if (inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount -= item.amount;
                    itemInInventory = inventoryItem;
                }
            }
            if (itemInInventory != null && itemInInventory.amount <= 0)
            {
                itemList.Remove(itemInInventory);
            }
        }
        else
        {
            itemList.Remove(item);
        }
    }

    public void UseItem(Item item)
    {
        useItemAction(item);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }

    public void InventoryItemsLog()
    {
        Debug.Log("|||Items in inventory:");

        for (int i = 0; i < itemList.Count; i++)
        {
        Debug.Log($"||Item: {itemList[i].itemType}    Amount: {itemList[i].amount}");
        }
    }
}
