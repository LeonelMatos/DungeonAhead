using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public Player player;
    [HideInInspector]
    public bool IsQuestActive;
    public GoalType goalType;
    [Header("Gathering")]
    public int requiredAmount;
    public int currentAmount;
    public Item requiredItem;
    private Inventory inventory;
    [HideInInspector]
    public GameObject questNotification;
    private Notification notification;
    private Dialogue definedDialogue;
    [Header("Find")]
    public GameObject objectGoal;
    public string objectGoalName;


    public bool IsReached()
    {
        return (currentAmount >= requiredAmount);
    }

    public void GetPlayer(Player Player)
    {
        player = Player;
    }

    public void EnemyKilled()   //To Update
    {
        if (goalType == GoalType.Kill)
            currentAmount++;
    }

    int itemAmount;
    public void ItemCollected(Quest quest, PlayerStats playerStats)
    {
        inventory = player.gameObject.GetComponent<Player>().inventory;
        if (goalType == GoalType.Gathering && inventory.GetItemList().Count != 0 && quest.isActive == true)
            itemAmount = requiredItem.amount;
        for (int i = 0; i < inventory.GetItemList().Count; i++)
        {
            if (requiredItem.itemType == inventory.GetItemList()[i].itemType)
            {
                Debug.Log("Quest: Item found!");
                if (requiredItem.IsStackable())
                    currentAmount = inventory.GetItemList()[i].amount;
                else
                    foreach (Item item in inventory.GetItemList())
                        if (item.itemType == requiredItem.itemType)
                            currentAmount++;

                if (IsReached())
                {
                    Debug.Log($"Quest: {quest.title} finished");
                    quest.isActive = false;
                    IsQuestActive = false;//Keep just to be safe
                    if (requiredItem.IsStackable())             //For Stackable items
                        inventory.RemoveItem(requiredItem);
                    else
                    {
                        RemoveNonStackableItems();
                    }

                    playerStats.questList.Remove(quest);
                    notification = questNotification.GetComponent<Notification>();
                    notification.StartCoroutine(notification.OpenNotification($"{quest.title} completed.", "quest"));
                    GiveReward(quest);
                    quest.isDone = true;
                    definedDialogue.isDone = true;
                    break;
                }

                currentAmount = 0;
            }
        }
    }

    public void ItemFound(Quest quest, PlayerStats playerStats, GameObject definedObject)
    {
        if (goalType == GoalType.Find && quest.isActive)
        {
            Debug.Log($"Quest: Found object {definedObject.name}");
            //
            if(objectGoal != null)
            {
                if (objectGoal == definedObject)
                {
                    Debug.Log($"Quest: {quest.title} finished");
                    quest.isDone = true;
                    quest.isActive = false;
                    GiveReward(quest);
                    playerStats.questList.Remove(quest);
                    notification = questNotification.GetComponent<Notification>();
                    notification.StartCoroutine(notification.OpenNotification($"{quest.title} completed.", "quest"));
                    definedDialogue.isDone = true;
                }
            }
            else if (objectGoalName == definedObject.GetComponent<FindQuestTrigger>().objectGoalName)
            {
                Debug.Log($"Quest: {quest.title} finished");
                quest.isDone = true;
                quest.isActive = false;
                GiveReward(quest);
                playerStats.questList.Remove(quest);
                notification = questNotification.GetComponent<Notification>();
                notification.StartCoroutine(notification.OpenNotification($"{quest.title} completed.", "quest"));
                definedDialogue.isDone = true;
            }
        }
    }

    private void RemoveNonStackableItems()
    {
        for (int j = 0; j <= requiredItem.amount; j++)
            for (int k = 0; k < inventory.GetItemList().Count; k++)
                if (inventory.GetItemList()[k].itemType == requiredItem.itemType)
                    inventory.RemoveItem(inventory.GetItemList()[k]);
    }

    public void GetDialogue(Dialogue dialogue)
    {
        definedDialogue = dialogue;
    }

    public void GiveReward(Quest quest) //I think I can make this better, but I don't know how.
    {   if (!quest.isActive)
        {   //For Reward1
            if (quest.reward1Amount != 0)
            {
                if (quest.reward1.IsStackable())
                {
                    player.inventory.AddItem(quest.reward1);
                    Debug.Log($"Gave {quest.reward1.itemType} Amount: {quest.reward1.amount}");
                }
                else
                {
                    for (int j = 1; j <= quest.reward1Amount; j++)
                    {
                        player.inventory.AddItem(quest.reward1);
                        Debug.Log($"Gave {quest.reward1.itemType}");
                    }
                }
            }
            //For Reward2
            if (quest.reward2Amount != 0)
            {
                if (quest.reward2.IsStackable())
                {
                    player.inventory.AddItem(quest.reward2);
                    Debug.Log($"Gave {quest.reward2.itemType} Amount: {quest.reward1.amount}"); 
                }
                else
                {
                    for (int j = 1; j <= quest.reward2Amount; j++)
                    {
                        player.inventory.AddItem(quest.reward2);
                        Debug.Log($"Gave {quest.reward2.itemType}");
                    }
                }
            }
        }
    }
}

public enum GoalType
{
    Kill,
    Gathering,
    Find
}