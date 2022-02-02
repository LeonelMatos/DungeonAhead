using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;
using CodeMonkey.Utils;
public class ItemWorld : MonoBehaviour
{
    public static ItemWorld SpawnItemWorld(Vector2 position, Item item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;
    }
    
    //To Update: Grabbed item right after being dropped
    public static ItemWorld DropItem(Vector3 dropPosition, Item item)
    {
        Vector3 randomDir = UtilsClass.GetRandomDir();
        ItemWorld itemWorld = SpawnItemWorld(dropPosition + randomDir * 1f, item);
        itemWorld.GetComponent<Rigidbody2D>().AddForce(randomDir * 2f, ForceMode2D.Impulse);

        //To LevelData: sets item to isDestroyed = false
        ///\todo Ao droppar o item adiciona-o ao levelData
        return itemWorld;
    }
    
    private Item item;
    private SpriteRenderer spriteRenderer;
    private Light2D light2D;
    private TextMeshPro textMeshPro;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        light2D = GetComponentInChildren<Light2D>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
    }

    public void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
        light2D.color = item.GetColor();

        if (item.amount > 1)
        {
            textMeshPro.SetText(item.amount.ToString());
        }
        else
        {
            textMeshPro.SetText("");
        }
    }

    public Item GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}