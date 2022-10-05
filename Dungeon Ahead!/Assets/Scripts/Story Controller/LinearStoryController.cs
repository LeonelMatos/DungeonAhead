using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// Event: element of EventList, defines an action (event)
/// that will run in order at EventList
[System.Serializable]
public class Event
{
    public GameObject gameObject;
    
    public enum Functions {
        DebugText,
        A_Wait,
        LSC,
    }
    public Functions function;

}
public class LinearStoryController : MonoBehaviour
{
    public bool startOnRun;
    [HideInInspector]
    public int eventListCounter = -1;
    public List<Event> EventList = new List<Event>();


    private void Reset() {
        if (!gameObject.TryGetComponent(out AssistantController assistant)){
            gameObject.AddComponent<AssistantController>();
            Debug.Log($"{gameObject.name}: Created an AssistantController for this LinearStoryController");
        }
    }

    private void Start()
    {
        if (startOnRun) {
            eventListCounter = -1;
            RunEventList();
        
        }
    }

    // For debug purposes
    private void Update() {
        
        if (Input.GetKeyDown(KeyCode.E) && gameObject.name == "LinearStoryController"){
            eventListCounter = -1;
            RunEventList();
        }
    }
    
    public void RunEventList()
    {
        eventListCounter++;

        if (eventListCounter < EventList.Count) {

            switch (EventList[eventListCounter].function)
                {
                    case Event.Functions.DebugText:
                    EventList[eventListCounter].gameObject.GetComponent<TestController>().test(this);
                    break;
                    case Event.Functions.A_Wait:
                    gameObject.GetComponent<AssistantController>().Wait();
                    gameObject.GetComponent<AssistantController>().runOrder++;
                    break;
                    case Event.Functions.LSC:
                        EventList[eventListCounter].gameObject.GetComponent<LinearStoryController>().eventListCounter = -1;
                        EventList[eventListCounter].gameObject.GetComponent<LinearStoryController>().RunEventList();
                        break;          
                }
        }

    }   

}