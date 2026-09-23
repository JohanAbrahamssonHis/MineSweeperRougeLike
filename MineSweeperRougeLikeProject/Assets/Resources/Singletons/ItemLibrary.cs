using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Singletons/Library/ItemLibrary", fileName = "ItemLibrary")]
public class ItemLibrary : ScriptableObject
{
    private static ItemLibrary _instance;

    public static ItemLibrary Instance
    {
        get
        {
            if (_instance == null) _instance = Resources.Load<ItemLibrary>("Singletons/ItemLibrary");
            return _instance;
        }
    }

       [SerializeField] private List<Item> _baseSetItems;

    public List<Item> Items { private set; get; }

    public void ConnectLibrary() => Items = new(_baseSetItems);

    public void SetListDependancy(List<Item> Items) => this.Items = Items;

    public Item GetRandomItem()
    {
        if (Items.Count == 0)
        {
            Debug.LogWarning("No items available.");
            return null;
        }

        int randomIndex = UnityEngine.Random.Range(0, Items.Count);
        return Items[randomIndex];
    }

    public bool UseSetItem = false;
    public Item SetItem;
}
