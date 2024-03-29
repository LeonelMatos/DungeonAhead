﻿using System;
using UnityEngine;
using TriInspector;

[Serializable]
[DeclareHorizontalGroup("top")]
[DeclareHorizontalGroup("bottom")]
public class Item
{
    public enum ItemType    //Insert itemType here by entry order
    {
        Coin,
        EnergyPotion,
        HealthPotion,
        Medkit,
        Milk,
        NightVisionPotion,
        SpeedPotion,
        Sword,
        Book,
        RandomPotion,
    }
    [GUIColor(0.6f, 0.9f, 1.0f), LabelWidth(70), Group("top")]
    public ItemType itemType;
    [Group("top")]
    [GUIColor(0.6f, 0.9f, 1.0f), LabelWidth(45)]
    public int amount;
    [HideInInspector]
    public bool isDestroyed; //Used at LevelData
    [Group("bottom")]
    public ItemText itemText;

    public Sprite GetSprite()   //Insert sprite here
    {
        switch (itemType)
        {
        default:
            case ItemType.Coin:         return ItemAssets.Instance.coinSprite;
            case ItemType.EnergyPotion:   return ItemAssets.Instance.energyPotionSprite;
            case ItemType.HealthPotion: return ItemAssets.Instance.healthPotionSprite;
            case ItemType.Medkit:       return ItemAssets.Instance.medkitSprite;
            case ItemType.Milk:         return ItemAssets.Instance.milkSprite;
            case ItemType.NightVisionPotion:    return ItemAssets.Instance.nightVisionPotionSprite;
            case ItemType.Sword:        return ItemAssets.Instance.swordSprite;
            case ItemType.SpeedPotion: return ItemAssets.Instance.speedPotionSprite;
            case ItemType.Book: return ItemAssets.Instance.bookSprite;
            case ItemType.RandomPotion: return ItemAssets.Instance.randomPotionSprite;
        }
    }

    public Color GetColor()     //Insert color when dropped here
    {
        switch(itemType)
        {
            default:
            case ItemType.Coin:         return Color.yellow;
            case ItemType.EnergyPotion:   return Color.blue;
            case ItemType.HealthPotion: return Color.red;
            case ItemType.Medkit:       return Color.magenta;
            case ItemType.Milk:         return Color.white;
            case ItemType.NightVisionPotion:  return Color.magenta;
            case ItemType.Sword:        return Color.white;
            case ItemType.SpeedPotion:  return Color.green;
            case ItemType.Book:         return Color.yellow;
            case ItemType.RandomPotion:  return Color.white;
        }
    }

    public bool IsStackable()   //Insert other properties here
    {
        switch(itemType)
        {
            default:
            case ItemType.Coin:
            case ItemType.EnergyPotion:
            case ItemType.HealthPotion:
            case ItemType.Milk:
            case ItemType.NightVisionPotion:
            case ItemType.RandomPotion:
                return true;            //Is stackable
            case ItemType.Medkit:
            case ItemType.Sword:
            case ItemType.Book:
                return false;           //Not stackable
        }
    }

}