using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    private Notification notification;
    [Tooltip("Manual check in order to work")]
    public GameObject questWindow;
    [Tooltip("Manual check in order to work")]
    public PlayerStats playerStats;
    [Space(30)]
    public Quest quest;
    private Text titleText;
    private Text descriptionText;
    private Text reward1Text;
    private Text reward2Text;
    private Image reward1Img;
    private Image reward2Img;
    private Button acceptButton;
    private Button rejectButton;

    private GameObject questNotification;
    private QuestGiver childQuest;
    private Dialogue definedDialogue;

    private void Start()    //To update: references without grab&drop
    {                       //Updated: Don't change QuestWindow's hierarchy
        
        //gameObject.GetComponent<DialogueTrigger>().HasQuest = true;   Doesn't work with children.

        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        //questWindow = GameObject.FindGameObjectWithTag("QuestWindow");
        //titleText = GameObject.FindGameObjectWithTag("QuestWindow_Title").GetComponent<Text>();

        questNotification = questWindow.GetComponentInParent<Transform>().GetChild(0).gameObject;
        quest.goal.questNotification = questNotification;
        quest.goal.requiredAmount = quest.goal.requiredItem.amount;

        quest.goal.GetPlayer(playerStats.GetComponent<Player>());   //Gives Player to QuestGoal
    }

    private void SetReferences()
    {
        titleText = questWindow.transform.GetChild(1).GetComponent<Text>();
        descriptionText = questWindow.transform.GetChild(2).GetComponent<Text>();
        reward1Text = questWindow.transform.GetChild(3).GetComponent<Text>();
        reward1Img = reward1Text.transform.GetChild(0).GetComponent<Image>();
        reward2Text = questWindow.transform.GetChild(4).GetComponent<Text>();
        reward2Img = reward2Text.transform.GetChild(0).GetComponent<Image>();
        acceptButton = questWindow.transform.GetChild(7).GetComponent<Button>();
        rejectButton = questWindow.transform.GetChild(8).GetComponent<Button>();
        rejectButton.onClick.AddListener(CloseQuest);
    }

    public void SetQuest(Dialogue dialogue) //For multiple quests in one gameobject
    {
        quest.goal.GetDialogue(dialogue);
        if (quest.isDone)
        {

            for (int i = 0; i <= transform.childCount; i++) //Can get out of bounds
            {
                childQuest = transform.GetChild(i).GetComponent<QuestGiver>();
                if (!childQuest.quest.isDone)
                {
                    OpenQuestWindow(childQuest.quest);
                    break;
                }
            }
            //If every quest is done it will give the error
            //Transform child out of bounds.
        }
        else
            OpenQuestWindow(quest);
    }

    public void OpenQuestWindow(Quest quest)
    {
        SetReferences();
        questWindow.SetActive(true);
        reward1Img.gameObject.SetActive(true);
        reward2Img.gameObject.SetActive(true);
        quest.isDone = false;
        titleText.text = quest.title;
        descriptionText.text = quest.description;
        reward1Text.text = quest.reward1Amount.ToString();
        reward2Text.text = quest.reward2Amount.ToString();
        if (quest.reward1Amount == 0)
        {
            reward1Img.gameObject.SetActive(false);
            reward1Text.text = " ";
        }
        if(quest.reward2Amount == 0)
        {
            reward2Img.gameObject.SetActive(false);
            reward2Text.text = " ";
        }
        reward1Img.sprite = quest.reward1.GetSprite();
        reward2Img.sprite = quest.reward2.GetSprite();

        quest.goal.player.windowTooltip.GetComponent<Window>().SetTooltipQuestWindow(quest.reward1, quest.reward2);

        if (quest.isActive)
        {
            acceptButton.interactable = false;
            acceptButton.gameObject.SetActive(false);
        }
        else
        {
            acceptButton.gameObject.SetActive(true);
            acceptButton.interactable = true;
            acceptButton.onClick.AddListener(delegate { AcceptQuest(quest); });
        }
    }

    public void AcceptQuest(Quest quest)
    {
        if (quest.isActive)
            return;
        ///acceptButton.interactable = false;      //Blocks button from all
        acceptButton.onClick.RemoveAllListeners();
        questWindow.SetActive(false);
        quest.isActive = true;
        quest.goal.IsQuestActive = true;
        playerStats.questList.Add(quest);
        Debug.Log("Quest: Accepted quest:  " + quest.title);
        notification = questNotification.GetComponent<Notification>();
        notification.StartCoroutine(notification.OpenNotification($"{quest.title} started.", "quest"));
        quest.goal.ItemCollected(quest, playerStats);
    }

    private void CloseQuest()
    {
        questWindow.SetActive(false);
    }
}
