using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LinearStoryController : MonoBehaviour
{
    ///Struct that holds the type of events for the LSC
    [System.Serializable]
    public struct Event {
        public enum EventType {
            Dialogue,
            AnotherFunction,
        }
        public EventType eventType;

        
    }

    public string test;

    //Sequence of Events to run when activated the LSC
    public List<Event> EventList = new List<Event>();

    private StoryFunction storyFunction;

    void OnInspectorGUI()
    {
        for (int i = 0; i < EventList.Count; i++)
        {
            switch (EventList[i].eventType)
            {
                case Event.EventType.Dialogue:
                    Debug.Log("Event.EventType.Dialogue");
                break;
                case Event.EventType.AnotherFunction:
                    Debug.Log("Event.EventType.AnotherFunction");
                    break;
                default:
                    Debug.Log("Event type not found");
                break;
            }
        }
    }
    private void OnInspectorUpdate() {
        
    }
}
