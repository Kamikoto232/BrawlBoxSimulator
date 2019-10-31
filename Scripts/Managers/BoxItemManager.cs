using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoxItemManager : MonoBehaviour
{
    private ItemData[] itemDatas;
    private int currentShowedItemIndex;

    private void Start()
    {
        BoxItemPresenter.OnClickNextSubscribe(() => NextItem());
    }

    private bool closeObtained;
    private void NextItem()
    {
        if (closeObtained)
        {
            BoxItemPresenter.HideObtained();
            return;
        }

        if (currentShowedItemIndex > itemDatas.Length - 1)
        {
            BoxItemPresenter.ShowObtained();
            closeObtained = true;
        }
        else
        {
            ItemDistributor.AddItem(itemDatas[currentShowedItemIndex]);
        }

        currentShowedItemIndex++;
        BoxItemPresenter.SetRemaingValue(itemDatas.Length - currentShowedItemIndex);
    }

    public void OpenBox(Box box) //UnityEventCall
    {
        if (CheckPossibleOpenBox(box))
            TakeCostFromBox(box);
        else
        {
            ShowNotEnoughWindow(box);
            return;
        }

        ItemsInBox = Random.Range(box.MinItems, box.MaxItems);
        BoxItemPresenter.ShowBox(box);
        SetShowedItems(GetItemsFromBox(box.Items));
    }
    
    private bool CheckPossibleOpenBox(Box box)
    {
        bool possible = false;

        switch (box.CostMethod)
        {
            case Box.CostType.Coins:
                possible = TitlePanelManager.TryTakeCoins(box.Cost);
                break;

            case Box.CostType.Gems:
                possible = TitlePanelManager.TryTakeCoins(box.Cost);
                break;

            case Box.CostType.Free:
                possible = true;
                break;

            case Box.CostType.ADS:
                possible = true; //TODO сделать чек доступности рекламы
                break;
        }

        return possible;
    }

    private void TakeCostFromBox(Box box)
    {
        switch (box.CostMethod)
        {
            case Box.CostType.Coins:
                TitlePanelManager.TakeCoins(box.Cost);
                break;

            case Box.CostType.Gems:
                TitlePanelManager.TakeGems(box.Cost);
                break;
        }
    }

    private void ShowNotEnoughWindow(Box box) //TODO реализовать презентер окон
    {

    }

    private void SetShowedItems(ItemData[] itemDatas)
    {
        closeObtained = false;
        this.itemDatas = itemDatas;
        currentShowedItemIndex = 0;
        BoxItemPresenter.SetRemaingValue(itemDatas.Length);
    }

    private int ItemsInBox;
    private float totalChance;
    private ItemData[] GetItemsFromBox(BoxItemData[] boxItemData)
    {
        List<ItemData> itemsToReturn = new List<ItemData>();
        
        foreach (var item in boxItemData)
        {
            totalChance += item.Chance;
        }

        while (itemsToReturn.Count < ItemsInBox)
        {
            foreach (var item in boxItemData)
            {
                if (CheckItem(item) && itemsToReturn.Find(x => x.ItemType == item.ItemType).ItemType != item.ItemType)
                    itemsToReturn.Add(new ItemData(item.ItemType, Random.Range(item.CountMin, item.CountMax)));
            }
        }

        return itemsToReturn.ToArray();
    }

    private bool CheckItem(BoxItemData boxItemData)
    {
        float randomPoint = Random.value * totalChance;

        if (randomPoint < boxItemData.Chance)
            return true;
        else
            return false;
    }
}
