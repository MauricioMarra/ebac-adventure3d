using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemControl : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _itemValue;
    [SerializeField] private Image _icon;

    private ItemSetup _itemSetup;

    public void SetItemSetup(ItemSetup itemSetup)
    {
        this._itemSetup = itemSetup;
    }

    public void Initialize()
    {
        SetIcon();
        SetValue();
    }

    public void SetIcon()
    {
        this._icon.sprite = _itemSetup.icon;
    }

    public void SetValue()
    {
        this._itemValue.text = _itemSetup.scriptableObjects.value.ToString();
    }

    public void UpdateValue()
    {
        this._itemValue.text = _itemSetup.scriptableObjects.value.ToString();
    }
}
