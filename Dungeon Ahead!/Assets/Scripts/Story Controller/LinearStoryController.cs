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
    public bool EOS = false;
    public List<Event> EventList = new List<Event>();

    private void Start() {
        for (int i = 0; i < EventList.Count; i++)
        {
            if (!EOS)
            {
                i--;
                continue;
            }
            switch (EventList[i].function)
            {
                case Event.Functions.test:
                EventList[i].gameObject.GetComponent<TestController>().test(this);
                break;
                case Event.Functions.test_wait:
                EventList[i].gameObject.GetComponent<TestController>().test_wait(this);
                break;
            }
        }
    }

}