using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeOverlap_Layer : MonoBehaviour
{
    GameObject player;
    SpriteRenderer spriteRenderer;
    private void Start()
    {
        BoxCollider2D trigger = gameObject.AddComponent<BoxCollider2D>();
        trigger.isTrigger = true;
        trigger.size = new Vector2(8, 8);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.AddComponent<TreeOverlay_Trigger>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(GetComponent<TreeOverlay_Trigger>());
    }
}
