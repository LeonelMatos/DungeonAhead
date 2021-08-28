using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeOverlap : MonoBehaviour
{
    [HideInInspector]
    public GameObject player;
    [HideInInspector]
    public SpriteRenderer spriteRenderer;
    [Tooltip("To start coroutines FadeOut and FadeIn")]
    public bool FadeInOut = true;
    [Tooltip("Sets an offset to the Overlay_Trigger (positive value => up; negative value => down")]
    public float layerOffset;

    private void Start()
    {
        GameObject layer = new GameObject("Layer");
        layer.transform.SetParent(transform);
        layer.transform.position = transform.position - new Vector3(0, 2, 0);
        TreeOverlap_Layer Layer = layer.AddComponent<TreeOverlap_Layer>();

        if (!TryGetComponent(out BoxCollider2D collider))
        {
            //Debug.LogWarning("GameObject must have a BoxCollider2D for the overlap. But I'll add it, chill");
            gameObject.AddComponent<BoxCollider2D>();
            GetComponent<BoxCollider2D>().isTrigger = true;
            GetComponent<BoxCollider2D>().size = new Vector2 (0.5f, 0.1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(FadeInOut)
            StartCoroutine("FadeOut");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(FadeInOut)
            StartCoroutine("FadeIn");
    }

    private IEnumerator FadeOut()
    {
        for (int i = 0; i < 10; i++)
        {
            GetComponent<SpriteRenderer>().color -= new Color(0f, 0f, 0f, .05f);
            yield return new WaitForSeconds(0.05f);
        }
    }

    private IEnumerator FadeIn()
    {
        for (int i = 0; i < 10; i++)
        {
            GetComponent<SpriteRenderer>().color += new Color(0f, 0f, 0f, .05f);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
