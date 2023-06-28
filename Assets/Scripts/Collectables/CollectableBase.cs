using System;
using UnityEngine;

public class CollectableBase : MonoBehaviour
{
    [SerializeField] private ItemType _itemType;

    [Header("Sounds")]
    public AudioSource audioSource;

    private string playerTag = "Player";

    [SerializeField] private float _destroyDelay = 5.0f;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag(playerTag)) OnCollect();
    }

    protected void Collect()
    {
        var collider = this.GetComponent<SphereCollider>();

        if (collider != null) 
            collider.enabled = false;

        if (audioSource != null)
            audioSource.Play();

        ItemManager.instance.AddItemByType(_itemType);

        Destroy(gameObject, _destroyDelay);
    }

    protected virtual void OnCollect()
    {
        Collect();
    }

    public ItemType GetItemType()
    {
        return _itemType;
    }
}
