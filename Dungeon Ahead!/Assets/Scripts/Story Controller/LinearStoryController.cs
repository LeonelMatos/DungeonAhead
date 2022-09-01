using System.Diagnostics.Tracing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Event: element of EventList, defines an action (event)
/// that will run in order at EventList
[System.Serializable]
public class Event
{
    public GameObject gameObject;
    
    public enum Functions {
        Dialogue,
        test,
        test_wait,
    }
    public Functions function;

}
public class LinearStoryController : MonoBehaviour
{
    public int eventListCounter;
    public List<Event> EventList = new List<Event>();

    private void Start() {
        eventListCounter = 0;
        
        RunEventList();
    }

    public void RunEventList()
    {
        if (eventListCounter < EventList.Count) {

            switch (EventList[eventListCounter].function)
                {
                    case Event.Functions.test:
                    EventList[eventListCounter].gameObject.GetComponent<TestController>().test(this);
                    this.eventListCounter++;
                    break;
                    case Event.Functions.test_wait:
                    EventList[eventListCounter].gameObject.GetComponent<TestController>().test_wait(this);
                    this.eventListCounter++;
                    break;
                }
        }
        else
            Debug.LogWarning("EventListCounter longer than expected:: " + eventListCounter);

    }   

}