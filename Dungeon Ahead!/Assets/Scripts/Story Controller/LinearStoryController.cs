using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class LinearStoryController : MonoBehaviour
{
    [SerializeField] private UnityEvent StoryList;
    
    private void Start() {
    StoryList.Invoke();
        
    }
}
