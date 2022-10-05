using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistantController : MonoBehaviour
{
    [HideInInspector]
    public int runOrder = 0;
    private int waitTime = 0;

    public List<int> waitValues = new List<int>();

    LinearStoryController lsc;

    public void Wait() 
    {
        if (runOrder >= waitValues.Count) {
            Debug.LogWarning("runOrder of AssistantController bigger than waitValues list size");
            return;
        }

        waitTime = waitValues[runOrder];
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
