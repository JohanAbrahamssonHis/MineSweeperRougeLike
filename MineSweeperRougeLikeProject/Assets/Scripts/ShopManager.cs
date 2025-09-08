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
        //Total Odds of all rarities
        int totalOdds = 0;
        for (int i = 1; i <= HighestRarity; i++) totalOdds += i;
        
        //Gets a random number
        int rarityValueSelected = Random.Range(1, totalOdds+1);
        
        //Gets a test value (currentValue) and ses if it is over that value
        int rarityValue = 0;
        int currentValue = 0;
        for (int i = 1; i <= HighestRarity; i++)
        {
            //Adds current rarity
            currentValue += i;
            //If it is not over, then go to next rarity
            if (rarityValueSelected > currentValue) continue;
            //Set rarity value to the inverse of what i is (Higher i means less rare item)
            rarityValue = HighestRarity+1-i;
            break;
        }
        
        //returns the list of what items have that rarity and have not been banned to showup
        return ItemList.Where(x => x.rarity == rarityValue && !BannedItemList.Contains(x)).ToList();
    }
}
