using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearStoryController : MonoBehaviour
{
    [System.Serializable]
    public struct Event {
        public enum EventType {
            ThisFunction,
            ThatFunction,
            AnotherFunction,
        }
        public EventType eventType;

        public int arg1;
        public int arg2;
    }

    public List<Event> EventList = new List<Event>();

}
