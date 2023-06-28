using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestControl : MonoBehaviour
{
    [SerializeField] private GameObject _itemToCollect;
    [SerializeField] private KeyCode _openKey;

    private bool _wasOpened = false;
    private bool _playerCanCollect = false;
    private GameObject _itemSpawned = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    [NaughtyAttributes.Button("Open Chest")]
    private void OpenChest()
    {
        GetComponent<Animator>()?.SetTrigger("Open");

        _itemSpawned = Instantiate(_itemToCollect, gameObject.transform);
        _itemSpawned.transform.localPosition = Vector3.up * 2;
        _itemSpawned.transform.localScale = new Vector3(.5f, .5f, .5f);

        _itemSpawned.transform.DOScale(0, .4f).From().SetDelay(.5f);

        _wasOpened = true;

        Invoke(nameof(Collect), 1f);
    }

    private void Collect()
    {
        _itemSpawned.transform.DOMoveY(3, .5f).SetRelative(true);
        _itemSpawned.transform.DORotate(Vector3.up * 360, .5f);
        _itemSpawned.transform.DOScale(0, .4f).SetDelay(.7f);

        ItemManager.instance.AddItemByType(_itemSpawned.GetComponent<CollectableBase>().GetItemType());
    }

    private void OnTriggerStay(Collider other)
    {
        if (_wasOpened) return;

        if (other.CompareTag("Player") && Input.GetKeyDown(_openKey))
            OpenChest();
    }
}
