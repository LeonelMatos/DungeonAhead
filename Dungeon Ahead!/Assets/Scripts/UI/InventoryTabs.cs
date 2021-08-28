using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class InventoryTabs : MonoBehaviour
{
    private Transform tabGroup;
    private Transform inventoryTab;
    private Transform characterTab;
    private Transform questTab;
    private Transform statsTab;

    public GameObject character;
    public GameObject quest;
    private GameObject questTemplate;
    private Transform questContent;

    private Color32 selectedColor = new Color32(188, 76, 0, 255);
    private Color32 unselectedColor = new Color32(169, 64, 0, 255);

    private void Start()
    {
        tabGroup = transform.GetChild(1);
        inventoryTab = tabGroup.GetChild(0);
        characterTab = tabGroup.GetChild(1);
        questTab = tabGroup.GetChild(2);
        statsTab = tabGroup.GetChild(3);

        character.SetActive(false);
        quest.SetActive(false);
        inventoryTab.GetComponent<Image>().color = selectedColor;

        inventoryTab.GetComponent<Button_UI>().ClickFunc = () =>    //InventoryTAb
        {
            inventoryTab.GetComponent<Image>().color = selectedColor;
            characterTab.GetComponent<Image>().color = unselectedColor;
            questTab.GetComponent<Image>().color = unselectedColor;
            statsTab.GetComponent<Image>().color = unselectedColor;
            GetComponent<InventoryUI>().itemSlotContainer.gameObject.SetActive(true);
            character.SetActive(false);
            quest.SetActive(false);
        };

        characterTab.GetComponent<Button_UI>().ClickFunc = () =>    //Character
        {
            inventoryTab.GetComponent<Image>().color = unselectedColor;
            characterTab.GetComponent<Image>().color = selectedColor;
            questTab.GetComponent<Image>().color = unselectedColor;
            statsTab.GetComponent<Image>().color = unselectedColor;

            character.SetActive(true);
            quest.SetActive(false);
            GetComponent<InventoryUI>().itemSlotContainer.gameObject.SetActive(false);
        };

        questTab.GetComponent<Button_UI>().ClickFunc = () =>
        {
            inventoryTab.GetComponent<Image>().color = unselectedColor;
            characterTab.GetComponent<Image>().color = unselectedColor;
            questTab.GetComponent<Image>().color = selectedColor;
            statsTab.GetComponent<Image>().color = unselectedColor;

            quest.SetActive(true);
            character.SetActive(false);
            GetComponent<InventoryUI>().itemSlotContainer.gameObject.SetActive(false);

            QuestTab();
        };
    }

    private void QuestTab()
    {

        int y = 0;
        int questCellSize = 60;		//Default: 60

        PlayerStats playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        questTemplate = quest.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        questContent = questTemplate.transform.parent;

        questTemplate.SetActive(false);

        foreach (Transform child in questContent)
        {
            if (child.name != "quest_template")
                Destroy(child.gameObject);
        }

        foreach (Quest questitem in playerStats.questList)
        {
            RectTransform questSlotRectTransform = Instantiate(questTemplate, questContent).GetComponent<RectTransform>();
            questSlotRectTransform.gameObject.SetActive(true);

            questSlotRectTransform.anchoredPosition = new Vector2(0, questTemplate.transform.localPosition.y - questCellSize * y);
            y++;
            
            questContent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 50 + questTemplate.transform.localPosition.y + questCellSize * y);

            Text questSlotTitle = questSlotRectTransform.GetChild(1).GetComponent<Text>();
            Image reward1 = questSlotRectTransform.GetChild(2).GetComponent<Image>();
            Image reward2 = questSlotRectTransform.GetChild(3).GetComponent<Image>();

            questSlotTitle.text = questitem.title;

            reward1.color = Color.white;
            reward2.color = Color.white;
            if (questitem.reward1.amount > 0)
                reward1.sprite = questitem.reward1.GetSprite();
            else
                reward1.color = new Color (0, 0, 0, 0);
            if (questitem.reward2.amount > 0)
                reward2.sprite = questitem.reward2.GetSprite();
            else
                reward2.color = new Color(0, 0, 0, 0);
            questSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
            {
                questTemplate.GetComponent<QuestGiver>().OpenQuestWindow(questitem);
            };
        }
    }

    public void RefreshQuestTab()
    {
        //Check if QuestTab is selected; check if questContent is outdated.
        questTemplate = quest.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        questContent = questTemplate.transform.parent;
        //Check if questContent is outdated doesn't work, too bad.
        if (transform.GetChild(1).GetChild(2).GetComponent<Image>().color == selectedColor)
            QuestTab();
    }
}
