using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TriInspector;

/// Event: element of EventList, defines an action (event)
/// that will run in order at EventList
[DeclareHorizontalGroup("title")]
[DeclareHorizontalGroup("vars")]
[System.Serializable]
public class Event
{
    [Required, Group("vars")]
    [HideLabel]
    public GameObject gameObject;

    public enum Functions
    {
        Debug_Text = 10,
        Assistant_Wait = 100,
        LSC = 500,
        Dialogue = 1000,
        DropLoot = 1200,
        Inventory_AddItem = 1400,
        Inventory_RemoveItem,
        Effects_AddEffect = 1600,
        Effects_RemoveEffect,
        Effects_UseMilk,
        Effects_CameraFadeIn,
        Effects_CameraFadeOut,
        Player_TakeDamage = 1800,
        Player_TakeEnergy,
        Player_SetMaxHealth,
        Player_SetMaxEnergy,
        Player_SetHealth,
        Player_SetEnergy,
        Quest_OpenQuestWindow = 2000,


    }
    [Group("vars")]
    public Functions function;

}
public class LinearStoryController : MonoBehaviour
{
    public bool startOnRun;
    [HideInInspector]
    public int eventListCounter = -1;
    public List<Event> EventList = new List<Event>();


    private void Reset()
    {
        if (!gameObject.TryGetComponent(out AssistantController assistant))
        {
            gameObject.AddComponent<AssistantController>();
            Debug.Log($"{gameObject.name}: Created an AssistantController for this LinearStoryController");
        }
    }

    private void Start()
    {
        if (startOnRun)
        {
            eventListCounter = -1;
            RunEventList();

        }
    }

    public void RunEventList()
    {
        eventListCounter++;

        if (eventListCounter < EventList.Count)
        {

            switch (EventList[eventListCounter].function)
            {
                case Event.Functions.Debug_Text:
                    EventList[eventListCounter].gameObject.GetComponent<TestController>().test(this);
                    break;
                case Event.Functions.Assistant_Wait:
                    gameObject.GetComponent<AssistantController>().Wait();
                    gameObject.GetComponent<AssistantController>().runOrder++;
                    break;
                case Event.Functions.LSC:
                    EventList[eventListCounter].gameObject.GetComponent<LinearStoryController>().eventListCounter = -1;
                    EventList[eventListCounter].gameObject.GetComponent<LinearStoryController>().RunEventList();
                    break;
                case Event.Functions.Dialogue:
                    EventList[eventListCounter].gameObject.GetComponent<DialogueTrigger>().SetDialogue(this);
                    break;
                case Event.Functions.DropLoot:
                    EventList[eventListCounter].gameObject.GetComponent<DropBox>().OpenLoot(this);
                    break;
                case Event.Functions.Inventory_AddItem:
                    GetComponent<AssistantController>().AddItem();
                    break;
                case Event.Functions.Inventory_RemoveItem:
                    GetComponent<AssistantController>().RemoveItem();
                    break;
            }
        }

    }

}