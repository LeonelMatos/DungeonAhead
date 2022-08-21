using System.Diagnostics.Tracing;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Event
{
    public GameObject gameObject;
    
    public enum Functions {
        Dialogue,
        test,
    }
    public Functions function;
    public bool EOS;

}
public class LinearStoryController : MonoBehaviour
{
    public List<Event> EventList = new List<Event>();

    
}
