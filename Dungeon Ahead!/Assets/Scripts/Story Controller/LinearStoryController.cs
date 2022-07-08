using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearStoryController : MonoBehaviour
{
    [System.Serializable]
    public struct Event {
        public enum EventType {
            changeVar,
            ThatFunction,
            AnotherFunction,
        }
        public EventType eventType;

        public int arg1;
        public int arg2;
    }

    public List<Event> EventList = new List<Event>();

    private StoryFunction storyFunction;

    private void Start() {
        
        for (int i = 0; i < EventList.Count; i++)
        {
            switch (EventList[i].eventType)
            {
                case Event.EventType.changeVar:
                storyFunction.ChangeVar(EventList[i].arg1, EventList[i].arg2);
                break;
            }
        }
    }

}
