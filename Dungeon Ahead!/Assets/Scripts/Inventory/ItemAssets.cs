using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public Transform pfItemWorld;

    public Sprite coinSprite;
    public Sprite energyPotionSprite;
    public Sprite healthPotionSprite;
    public Sprite medkitSprite;
    public Sprite milkSprite;
    public Sprite nightVisionPotionSprite;
    public Sprite swordSprite;
    public Sprite speedPotionSprite;
    public Sprite bookSprite;


    [Space(10)]
    [Header("Icons")]
    public Sprite potionSprite;
    public Sprite questSprite;
    public Sprite systemSprite;
}