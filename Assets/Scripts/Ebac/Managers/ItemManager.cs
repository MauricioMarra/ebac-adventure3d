using Ebac.Core.Singleton;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Coin,
    LifePack,
    Invencibility,
    Powerful,
    SpeedUp
}

[Serializable]
public class ItemSetup
{
    public ItemType Type;
    public SOItem scriptableObjects;
    public Sprite icon;
}

public class ItemManager : Singleton<ItemManager>
{
    public List<ItemSetup> items;

    public void AddItemByType(ItemType type)
    {
        items.Find(x => x.Type == type)?.scriptableObjects?.Add();
        UIManager.instance.UpdateItems();
    }

    public ItemSetup GetItemByType(ItemType type)
    {
        var item = items.Find(x => x.Type == type && x.scriptableObjects.value > 0);

        return item;
    }

    public void RemoveItemByType(ItemType type)
    {
        var item = items.Find(x => x.Type == type);
        item?.scriptableObjects?.Subtract();

        UIManager.instance.UpdateItems();
    }
}
