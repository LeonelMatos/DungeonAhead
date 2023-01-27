using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistantController : MonoBehaviour
{
    [HideInInspector]
    public int runOrder = 0;
    [HideInInspector]
    public int inventoryOrder = 0;
    private int waitTime = 0;

    [Header("Auxiliary Values")]
    [Tooltip("Used in the Assistant methods at the LSC (ex: Wait)")]
    ///List of int values of time used at the correspondent wait method at LSC
    public List<int> waitValues = new List<int>();

    [Space(20)]
    [Tooltip("Used in the Inventory methods at the LSC")]
    public List<Item> itemList = new List<Item>();

    public void Wait() 
    {
        if (runOrder >= waitValues.Count) {
            Debug.LogWarning("runOrder of AssistantController bigger than waitValues list size");
            return;
        }

        waitTime = waitValues[runOrder];
        StartCoroutine(WaitForSeconds());
    }

    public IEnumerator WaitForSeconds()
    {
        yield return new WaitForSeconds(waitTime);
        GetComponent<LinearStoryController>().RunEventList();
    }

    public void AddItem()
    {
        if (inventoryOrder >= itemList.Count)
        {
            Debug.LogWarning("inventoryOrder of AssistantController bigger than itemList size");
            return;
        }

        Inventory inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().inventory;

        inventory.AddItem(itemList[inventoryOrder]);

        inventoryOrder++;
        GetComponent<LinearStoryController>().RunEventList();
    }

    public void RemoveItem()
    {
        if (inventoryOrder >= itemList.Count)
        {
            Debug.LogWarning("inventoryOrder of AssistantController bigger than itemList size");
            return;
        }

        Inventory inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().inventory;

        Debug.Log("Removed item " + itemList[inventoryOrder].ToString());
        inventory.RemoveItem(itemList[inventoryOrder]);
    }

}
