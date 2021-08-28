using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextWindow : MonoBehaviour
{
    private void Start()
    {
        transform.GetChild(3).GetComponent<Button>().onClick.AddListener(CloseTextWindow);
    }

    public void OpenTextWindow(Item item, InventoryUI inventoryUI)
    {
        RectTransform scrollArea = transform.GetChild(1).GetComponent<RectTransform>();
        RectTransform textSize = transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<RectTransform>();

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }

        inventoryUI.OpenInventory();
        transform.GetChild(2).GetComponent<Text>().text = item.itemText.title;
        transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>().text = item.itemText.text;
        transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<Scrollbar>().value = 10;
        if (textSize.GetComponent<Text>().minHeight <= scrollArea.rect.height)
        {
            transform.GetChild(1).GetChild(0).GetChild(1).gameObject.SetActive(false);
        }
    }

    private void CloseTextWindow()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
