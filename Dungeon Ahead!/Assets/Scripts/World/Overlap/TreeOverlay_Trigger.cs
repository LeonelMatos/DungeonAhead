using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeOverlay_Trigger : MonoBehaviour
{
    GameObject player;
    SpriteRenderer spriteRenderer;
    private float layerOffset;
    BoxCollider2D Collider;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = transform.parent.GetComponent<SpriteRenderer>();
        layerOffset = GetComponentInParent<TreeOverlap>().layerOffset;
        Collider = transform.parent.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (transform.parent.transform.position.y + Collider.offset.y - layerOffset + 2 > player.transform.position.y)
        {
            if(spriteRenderer.sortingLayerName != "Default")
                spriteRenderer.sortingLayerName = "Default";

        }
        else
        {
            if (spriteRenderer.sortingLayerName != "UpperLayer")
                spriteRenderer.sortingLayerName = "UpperLayer";

        }
    }
}
