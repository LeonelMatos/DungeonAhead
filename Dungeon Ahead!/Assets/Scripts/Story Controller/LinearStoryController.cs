using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class LinearStoryController : MonoBehaviour
{
    [SerializeField] public UnityEvent StoryList;
    
    public bool EOS; ///End Of Script: announces when a system ends

    private void Start() {
    StoryList.Invoke();
        
    }

    public void WaitForEndOfScript()
    {
        EOS = false;
        while (!EOS)
            {
                Debug.Log("Waiting for next listener");
            }
    }

    public void EndOfScript()
    {
        EOS = true;
    }

    public void DebugText(string text)
    {
        for (int i = 0; i < 10; i++)
        {
            Debug.Log(text);
            
        }
    }

}
