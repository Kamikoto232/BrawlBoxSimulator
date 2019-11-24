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
        {
            TakeCostFromBox(box);
            ItemsInBox = Random.Range(box.MinItems, box.MaxItems);
            BoxItemPresenter.ShowBox(box);
            SetShowedItems(GetItemsFromBox(box.Items));
            PlayerDataModel.AddXp(box.XP);
        }
        else
        {
            ShowNotEnoughWindow(box);
        }
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
                possible = TitlePanelManager.TryTakeGems(box.Cost);
                break;

            case Box.CostType.Free:
                possible = true;
                break;

            case Box.CostType.ADS:
                possible = ADSManager.IsRewardedAdReady();
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
        switch (box.CostMethod)
        {
            case Box.CostType.Coins:
                WindowsManager.ShowCoinWarn();
                break;

            case Box.CostType.Gems:
                WindowsManager.ShowGemsWarn();
                break;

            case Box.CostType.ADS:
                WindowsManager.ShowGemsWarn();
                break;
        }
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
        List<ItemData> items = new List<ItemData>();
        
        foreach (var item in boxItemData)
        {
            totalChance += item.Chance;
        }

        while (items.Count < ItemsInBox)
        {
            foreach (var item in boxItemData)
            {
                if (IsGetted(item) && items.Find(x => x.ItemType == item.ItemType).ItemType != item.ItemType)
                    items.Add(new ItemData(item.ItemType, Random.Range(item.CountMin, item.CountMax), item.ItemType == ItemData.Type.Brawler ? BrawlersManager.GetBrawler() : null));
            }
        }

        return items.ToArray();
    }

    private bool IsGetted(BoxItemData boxItemData)
    {
        float randomPoint = Random.value * totalChance;

        if(boxItemData.ItemType == ItemData.Type.Brawler)
            boxItemData.Chance = GetBrawlerChance();
        
        if (randomPoint < boxItemData.Chance)
            return true;
        else
            return false;
    }

    private float GetBrawlerChance()
    {
        return BrawlersManager.GetChance();
    }
}
