using Ebac.Core.Singleton;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Coin,
    LifePack
}

[Serializable]
public class ItemSetup
{
    public ItemType Type;
    public SOItem scriptableObjects;
}

public class ItemManager : Singleton<ItemManager>
{
    public List<ItemSetup> items;

    public void AddItemByType(ItemType type)
    {
        items.Find(x => x.Type == type)?.scriptableObjects?.Add();
    }
}
