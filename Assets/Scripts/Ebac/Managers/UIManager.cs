using Ebac.Core.Singleton;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Image _bulletCount;
    [SerializeField] private Slider _playerHealth;

    private List<ItemControl> _items = new();

    public void UpdateBulletCount(float fill)
    {
        _bulletCount.fillAmount = 1f - fill;
    }

    public void UpdateBulletCount(float max, float min)
    {
        _bulletCount.fillAmount = min / max;
    }

    public void UpdatePlayerHealth(float fill) 
    {
        _playerHealth.value = 1f - fill;
    }

    public void UpdatePlayerHealth(float max, float min)
    {
        _playerHealth.value = min / max;
    }

    public void AddItemToList(ItemControl item)
    {
        _items.Add(item);
    }

    public void UpdateItems()
    {
        _items.ForEach(item => item.UpdateValue());
    }
}
