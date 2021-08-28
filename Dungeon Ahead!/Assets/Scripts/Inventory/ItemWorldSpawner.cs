﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorldSpawner : MonoBehaviour
{
    public Item item;

    private void Start()
    {
        ItemWorld.SpawnItemWorld(transform.position, item);
        Destroy(gameObject);
    }
}
