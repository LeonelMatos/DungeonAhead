using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using CodeMonkey.Utils;

public class InventoryUI : MonoBehaviour
{
    /* <Warning>
     Sensitive code.
     A single and/or minimal change may destroy the universe itself.
     Proceed with caution!
     </Warning> */
    private Inventory inventory;
    private ItemOptions itemOptions;
    public Transform itemSlotContainer;
    private Transform itemSlotTemplate;
    public GameObject inventoryUI;
    public bool openInventory;
    private Player player;
    private Tooltip windowTooltip;

    private void Awake()//Update: Start -> Awake
    {
        itemSlotContainer = transform.Find("itemSlotContainer");
        itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");

        inventoryUI.SetActive(false);
        openInventory = false;
    }

    [System.Obsolete]
    private void Start()
    {
        transform.FindChild("Image").gameObject.SetActive(true);
        windowTooltip.GetComponent<Window>().SetInventoryTabs(this);

        RefreshInventoryItems();
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    public void GetReference(Tooltip tooltip)
    {
        windowTooltip = tooltip;
    }

    public void OpenInventory()   //@Player
    {
        RefreshInventoryItems();
        if (openInventory == false)
        {
            //Debug.Log("Opened Inventory");
            inventoryUI.SetActive(true);
            openInventory = true;
            windowTooltip.HideTooltip_Public();
            GetComponent<InventoryTabs>().RefreshQuestTab();
        }
        else if(openInventory == true)
        {
            //Debug.Log("Closed Inventory");
            inventoryUI.SetActive(false);
            openInventory = false;
            windowTooltip.HideTooltip_Public();
        }
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        inventory.OnItemListChanged += Inventory_OnItemListChanged;

        RefreshInventoryItems();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    public Text amountText;
    private void RefreshInventoryItems()
    {
        foreach (Transform child in itemSlotContainer) {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        int x = 0;
        int y = 0;
        float itemSlotCellSize = 50f;   // Default: 70 (75)
        foreach (Item item in inventory.GetItemList()) {
            RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
            itemSlotRectTransform.gameObject.SetActive(true);

            player.windowTooltip.GetComponent<Window>().SetTooltipWindow(itemSlotRectTransform, item);
            //windowTooltip.GetComponent<Window>().SetTooltipWindow(itemSlotRectTransform, item);

            itemSlotRectTransform.GetComponent<Button_UI>().ClickFunc = () =>
            {
                //Use Item
                //Debug.Log("Left button");
                //Debug.LogWarning(item.itemType);
                inventory.UseItem(item);
            };
            itemSlotRectTransform.GetComponent<Button_UI>().MouseRightClickFunc = () =>
            {
                //Drop Item
                //Debug.Log("Right button");
                //Debug.LogWarning("Removed item: " + item.itemType);
                Item duplicateItem = new Item { itemType = item.itemType, amount = item.amount };
                inventory.RemoveItem(item);
                ItemWorld.DropItem(player.GetPosition(), duplicateItem);
                windowTooltip.HideTooltip_Public();
            };

            itemSlotRectTransform.anchoredPosition = new Vector2(x * itemSlotCellSize, -y * itemSlotCellSize);
            Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
            image.sprite = item.GetSprite();

            TextMeshProUGUI uiText = itemSlotRectTransform.Find("text").GetComponent<TextMeshProUGUI>();
            if (item.amount > 1)
            {
                //amountText.text = (item.amount + 1).ToString();   //Default - Bugged
                uiText.SetText(item.amount.ToString());   //TMP
            }
            else
            {
               //amountText.text = "";   //Default  /\ /\ public text
               uiText.SetText("");    //TMP
            }

            x++;
            if (x >= 7)  //Default: 5 (>4)
            {
                x = 0;
                y++;
            }
        }
    }

}
