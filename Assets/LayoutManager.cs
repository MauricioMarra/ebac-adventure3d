using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LayoutManager : MonoBehaviour
{
    [SerializeField] private ItemControl _itemPrefab;
    [SerializeField] private Transform _container;

    // Start is called before the first frame update
    void Start()
    {
        CreateItemSlot();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateItemSlot()
    {
        ItemManager.instance.items.ForEach(item => {
            var itemSlot = Instantiate(_itemPrefab, _container);
            //itemSlot.GetComponentsInChildren<Image>().FirstOrDefault(x => x.name.Equals("Icon")).sprite = item.icon;
            UIManager.instance.AddItemToList(itemSlot);
            itemSlot.SetItemSetup(item);
            itemSlot.Initialize();
        } );
    }
}
