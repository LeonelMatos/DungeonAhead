using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistantController : MonoBehaviour
{
    public int waitTime = 0;
    LinearStoryController lsc;

    public void Wait(int value) 
    {
        if (value != 0) waitTime = value;
        StartCoroutine(WaitForSeconds());
    }

    private void ReturnLSC()
    {
        gameObject.GetComponent<LinearStoryController>().RunEventList();
    }

    public IEnumerator WaitForSeconds()
    {
        yield return new WaitForSeconds(waitTime);
        ReturnLSC();
    }
}
