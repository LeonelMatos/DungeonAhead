using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.UI;

public class Window : MonoBehaviour
{
    private Transform inventoryTabs;

    public void SetTooltipWindow (RectTransform itemSlotRectTransform, Item item)
    {
        itemSlotRectTransform.GetComponent<Button_UI>().MouseOverFunc = () => Tooltip.ShowTooltip_Static(item.itemType.ToString());
        
        if (item.itemText.title != "")  //Check if item has title
            itemSlotRectTransform.GetComponent<Button_UI>().MouseOverFunc = () => Tooltip.ShowTooltip_Static(item.itemText.title);

        itemSlotRectTransform.GetComponent<Button_UI>().MouseOutOnceFunc = () => Tooltip.HideTooltip_Static();
    }

    //To fix
    public void SetInventoryTabs (InventoryUI inventoryUI)
    {
        Debug.Log("Setting tabs");
        inventoryTabs = inventoryUI.transform.GetChild(1);
        inventoryTabs.GetChild(0).GetComponent<Button_UI>().MouseOverFunc = () => Tooltip.ShowTooltip_Static("Inventory");
        inventoryTabs.GetChild(0).GetComponent<Button_UI>().MouseOutOnceFunc = () => Tooltip.HideTooltip_Static();
        inventoryTabs.GetChild(1).GetComponent<Button_UI>().MouseOverFunc = () => Tooltip.ShowTooltip_Static("Character");
        inventoryTabs.GetChild(1).GetComponent<Button_UI>().MouseOutOnceFunc = () => Tooltip.HideTooltip_Static();
        inventoryTabs.GetChild(2).GetComponent<Button_UI>().MouseOverFunc = () => Tooltip.ShowTooltip_Static("Quests");
        inventoryTabs.GetChild(2).GetComponent<Button_UI>().MouseOutOnceFunc = () => Tooltip.HideTooltip_Static();
        inventoryTabs.GetChild(3).GetComponent<Button_UI>().MouseOverFunc = () => Tooltip.ShowTooltip_Static("Statistics");
        inventoryTabs.GetChild(3).GetComponent<Button_UI>().MouseOutOnceFunc = () => Tooltip.HideTooltip_Static();
    }

    public void SetTooltipQuestWindow(Item item1, Item item2)
    {
        GameObject reward1 = GameObject.FindGameObjectWithTag("QuestWindow").transform.GetChild(3).gameObject;
        GameObject reward2 = GameObject.FindGameObjectWithTag("QuestWindow").transform.GetChild(4).gameObject;
        if(item1.amount != 0)
        {
        reward1.GetComponentInChildren<Button_UI>().MouseOverFunc = () => Tooltip.ShowTooltip_Static(item1.itemType.ToString());
        reward1.GetComponentInChildren<Button_UI>().MouseOutOnceFunc = () => Tooltip.HideTooltip_Static();
        }
        if(item2.amount != 0)
        {
        reward2.GetComponentInChildren<Button_UI>().MouseOverFunc = () => Tooltip.ShowTooltip_Static(item2.itemType.ToString());
        reward2.GetComponentInChildren<Button_UI>().MouseOutOnceFunc = () => Tooltip.HideTooltip_Static();
        }
    }
}
