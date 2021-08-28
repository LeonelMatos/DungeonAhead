using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class Tooltip : MonoBehaviour
{
    private static Tooltip instance;
    [SerializeField]
    private Camera uiCamera;
    [SerializeField]
    private RectTransform canvasRectTransform;

    private Text tooltipText;
    private RectTransform backgroundRectTransform;

    private void Awake()
    {
        instance = this;
        backgroundRectTransform = transform.GetChild(0).GetComponent<RectTransform>();
        tooltipText = transform.GetChild(1).GetComponent<Text>();
        canvasRectTransform = transform.parent.GetComponent<RectTransform>();
        /*ShowTooltip("Random Tooltip text");

            FunctionPeriodic.Create(() =>     //Test stuff
           {
               string abc = "samma lamma duma lama duma you're assuming i'm a human";
               string showText = "";
               for (int i = 0; i < Random.Range(30, 150); i++)
               {
                  showText += abc[Random.Range(0, abc.Length)];
               }
               ShowTooltip(showText);
           }, .5f);
        */

        HideTooltip_Static();
        SetReferences();
        SetReferencesInventoryUI();
    }

    private GameObject player;
    private QuestGiver questGiver;
    private GameObject inventoryUI;

    public void SetReferences()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Player>().GetReference(this);
        
    }

    public void SetReferencesInventoryUI()
    {
        player.GetComponent<Player>().inventoryUI.GetReference(this);
        //GameObject.FindGameObjectWithTag("InventoryUI").GetComponent<InventoryUI>().GetReference(this);
    }

    /*private void Start()
    {
        HideTooltip_Static();
    }*/

    private void Update()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out localPoint);
        transform.localPosition = localPoint;

        Vector2 anchoredPosition = transform.GetComponent<RectTransform>().anchoredPosition;
        if (anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width)
        {
            anchoredPosition.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;
        }
        if (anchoredPosition.y + backgroundRectTransform.rect.height > canvasRectTransform.rect.height)
        {
            anchoredPosition.y = canvasRectTransform.rect.height - backgroundRectTransform.rect.height;
        }
        transform.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
    }

    private void ShowTooltip(string tooltipString)
    {
        gameObject.SetActive(true);
        transform.SetAsLastSibling();

        tooltipText.text = tooltipString;
        float textPaddingSize = 4f;
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + textPaddingSize * 2f, tooltipText.preferredHeight + textPaddingSize * 2f);
        backgroundRectTransform.sizeDelta = backgroundSize;
    }

    private void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    public static void ShowTooltip_Static(string tooltipString)
    {
        instance.ShowTooltip(tooltipString);
    }

    public static void HideTooltip_Static()
    {
        instance.HideTooltip();
    }

    public void HideTooltip_Public()
    {
        HideTooltip_Static();
    }
}
