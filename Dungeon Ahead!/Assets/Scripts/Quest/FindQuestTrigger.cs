using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindQuestTrigger : MonoBehaviour
{
    public string objectGoalName;
    private PlayerStats player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    void OnTriggerEnter2D()
    {
        if (objectGoalName == null)
            objectGoalName = gameObject.name;
        
        foreach(Quest quest in player.questList.ToArray())
        {
            if (quest.isActive && quest.goal.goalType == GoalType.Find)
                quest.goal.ItemFound(quest, player, gameObject);
        }
    }
}
