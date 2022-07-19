using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryFunction : MonoBehaviour
{

    ///\todo Get a reference of LSC from gameobject.
    private LinearStoryController LSC;

    private void Start() {
        
        for (int i = 0; i < LSC.EventList.Count; i++)
        {
            switch (LSC.EventList[i].eventType)
            {
                default:
                    Debug.Log("LSC error: Unknown function");
                break;
            }
        }
    }
}
