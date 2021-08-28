using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public bool isActive;
    public bool isDone;

    public string title;
    [TextArea(3, 3)]
    public string description;
    public Item reward1;
    [Tooltip("Must be the same as Item.amount \n(except when the item is non-Stackable)")]
    public int reward1Amount;
    public Item reward2;
    [Tooltip("Must be the same as Item.amount \n(except when the item is non-Stackable)")]
    public int reward2Amount;
    [Space(10)]
    public QuestGoal goal;

}
