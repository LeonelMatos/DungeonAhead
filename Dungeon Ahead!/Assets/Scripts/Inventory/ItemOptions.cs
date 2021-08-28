using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemOptions : MonoBehaviour, IPointerClickHandler
{
    /// <summary>
    /// O código é estúpido, não sei o que fazer.
    /// Arranja isto!
    /// 
    /// Update:
    /// Código não-funcional.
    /// </summary>
    private Inventory inventory;
    private ItemWorld ItemWorld;
    private Player player;
    public Transform itemSlotTemplate;
    public GameObject invOptionsButton;

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        foreach (Item item in inventory.GetItemList())
        {
            Instantiate(invOptionsButton, itemSlotTemplate);

            if (pointerEventData.button == PointerEventData.InputButton.Right)
            {
                //Drop Item
                Debug.Log("Right button");
                Debug.Log(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject);
                
                inventory.RemoveItem(item);
                ItemWorld.DropItem(player.GetPosition(), item);
            }

            if (pointerEventData.button == PointerEventData.InputButton.Left)
            {
                //Use Item
                Debug.Log("Left button");

            }
        }
    }
}
