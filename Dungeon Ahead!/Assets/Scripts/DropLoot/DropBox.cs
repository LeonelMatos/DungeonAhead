using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
\brief Component used when dropping items from the gameObject
*/
[SerializeField]
public class DropBox : MonoBehaviour
{
    /// List of items as loot to be dropped.
    public List<Item> loot;
    [Header("Properties")]
    [Range(0, 10)]
    public int lootMaxCount = 3;

    private PlayerControls controls;

    /// Defines the gameObject's DropBox as already looted
    private bool looted;

    /// Used when this script is summoned by the given lsc
    private LinearStoryController lsc;

    private void Awake()
    {
        if (!TryGetComponent(out BoxCollider2D collider))
        {
            BoxCollider2D newTrigger = gameObject.AddComponent<BoxCollider2D>();
            newTrigger.isTrigger = true;
            newTrigger.size = new Vector2(4f, 4f);
        }
        else if (TryGetComponent(out BoxCollider2D gotCollider) && !gotCollider.isTrigger)
        {
            BoxCollider2D newTrigger = gameObject.AddComponent<BoxCollider2D>();
            newTrigger.isTrigger = true;
            newTrigger.size = new Vector2(4f, 4f);
        }
    }

    void Start()
    {
        controls = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().playerControls;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.AddComponent<DropBox_Trigger>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(GetComponent<DropBox_Trigger>());
    }

    public void OpenLoot()
    {
        int random;

        random = Random.Range(0, lootMaxCount);
        if (!looted)
        {
            if (loot.Count == 0)
            {
                Debug.Log("(no loot) Generating loot...");
                for (int i = 0; i <= random; i++)
                {
                    loot.Add(new Item { itemType = GenerateLoot(Random.Range(1, 9)), amount = 1, itemText = new ItemText { title = "" } });
                }
            }

            if (gameObject.TryGetComponent<Animator>(out Animator animator))
                GetComponent<Animator>().SetTrigger("OpenBarrel");
            StartCoroutine("DropLoot");
            looted = true;
            if (lsc != null)
                StartCoroutine(WaitForDrop());
        }
    }

    public void OpenLoot(LinearStoryController lsc)
    {
        this.lsc = lsc;
        OpenLoot();
    }

    private IEnumerator WaitForDrop()
    {
        yield return new WaitForSeconds(2);
        lsc.RunEventList();
    }

    private IEnumerator DropLoot()
    {
        for (int i = 0; i < loot.Count; i++)
        {
            ItemWorld.DropItem(transform.position, loot[i]);
            yield return new WaitForSeconds(0.5f);
        }
    }

    private Item.ItemType GenerateLoot(int random)
    {
        switch (random)
        {
            case 0:
                return Item.ItemType.Coin;
            case 1:
                return Item.ItemType.Coin;
            case 2:
                return Item.ItemType.HealthPotion;
            case 3:
                return Item.ItemType.HealthPotion;
            case 4:
                return Item.ItemType.Milk;
            case 5:
                return Item.ItemType.Milk;
            case 6:
                return Item.ItemType.SpeedPotion;
            case 7:
                return Item.ItemType.SpeedPotion;
            case 8:
                return Item.ItemType.EnergyPotion;
            case 9:
                return Item.ItemType.EnergyPotion;
            default:
                Debug.LogWarning("How did we get here?");
                return Item.ItemType.Coin;
        }
    }
}
