using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public List<ShopItem> ShopItems;
    public List<Item> ItemList;
    private List<Item> BannedItemList;
    private int HighestRarity;
    void Start()
    {
        ActionEvents.Instance.TriggerEventShop(this);
        BannedItemList = new List<Item>();
        
        foreach (var item in ItemList.Where(item => item.rarity > HighestRarity)) HighestRarity = item.rarity;
        
        BannedItemList.Clear();
        
        ShopItems.ForEach(x => x.Item = GetShopItem());
        ShopItems.ForEach(x => x.SetUpShopItem());
        
        ActionEvents.Instance.TriggerEventShopAfter(this);
    }


    private Item GetShopItem()
    {
        List<Item> rarityItems = GetRarityValue();
        
        while (rarityItems.Count == 0) rarityItems = GetRarityValue();
        
        int selectItem = Random.Range(0, rarityItems.Count);

        BannedItemList.Add(rarityItems[selectItem]);

        return Instantiate(rarityItems[selectItem]);
    }

    private List<Item> GetRarityValue()
    {
        int totalOdds = 0;
        for (int i = 1; i <= HighestRarity; i++) totalOdds += i;
        
        int rarityValueSelected = Random.Range(1, totalOdds+1);
        
        int rarityValue = 0;
        int temp = 0;
        for (int i = 1; i <= HighestRarity; i++)
        {
            temp += i;
            if (rarityValueSelected > temp) continue;
            rarityValue = HighestRarity+1-i;
            break;
        }
        
        return ItemList.Where(x => x.rarity == rarityValue && !BannedItemList.Contains(x)).ToList();
    }
}
