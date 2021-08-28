using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    private Text notificationText;

    public IEnumerator OpenNotification(string text, string icon)
    {
        transform.SetAsLastSibling();
        notificationText = GetComponentInChildren<Text>();
        notificationText.text = text;

        gameObject.transform.GetChild(2).GetComponent<Image>().sprite = GetSprite(icon);
        GetComponent<Animator>().SetBool("IsOpen", true);

        yield return new WaitForSecondsRealtime(5);

        GetComponent<Animator>().SetBool("IsOpen", false);
        transform.SetAsFirstSibling();
    }

    public void StartNotification(string text, string icon) //If StartCoroutine is impossible
    {
        StartCoroutine(OpenNotification(text, icon));
    }

    private Sprite GetSprite(string icon)
    {
        switch (icon)
        {
            default:
            case "quest":    return ItemAssets.Instance.questSprite;
            case "potion":   return ItemAssets.Instance.potionSprite;
            case "system":   return ItemAssets.Instance.systemSprite;
        }
    }
}
