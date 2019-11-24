using UnityEngine;
using System.Collections;

public class TitlePanelManager : MonoBehaviour
{
    private static TitlePanelManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ItemDistributor.OnGetItem += GetItem;
    }

    public static void GetItem(ItemData itemData)
    {
        switch (itemData.ItemType)
        {
            case ItemData.Type.Coins:
                instance.AddCoins(itemData.Count);
                break;

            case ItemData.Type.Gems:
                instance.AddGems(itemData.Count);
                break;

            case ItemData.Type.Tickets:
                instance.AddTickets(itemData.Count);
                break;
        }
    }


    public static bool TryTakeCoins(int Value)
    {
        return PlayerDataModel.GetCoins() - Value >= 0;
    }

    public static bool TryTakeGems(int Value)
    {
        return PlayerDataModel.GetGems() - Value >= 0;
    }

    public static bool TryTakeTickets(int Value)
    {
        return PlayerDataModel.GetTickets() - Value >= 0;
    }

    public static void TakeCoins(int Value)
    {
        if (TryTakeCoins(Value)) PlayerDataModel.TakeCoins(Value);
        else Debug.LogWarning("Не хватает монет");
    }

    public static void TakeGems(int Value)
    {
        if (TryTakeCoins(Value)) PlayerDataModel.TakeGems(Value);
        else Debug.LogWarning("Не хватает гемов");
    }

    public static void TakeTickets(int Value)
    {
        if (TryTakeTickets(Value)) PlayerDataModel.TakeTickets(Value);
        else Debug.LogWarning("Не хватает билетов");
    }


    public void AddCoins(int Value)
    {
        PlayerDataModel.AddCoins(Value);
        TitlePanelPresenter.SetCoinsValue(PlayerDataModel.GetCoins());
    }

    public void AddGems(int Value)
    {
        PlayerDataModel.AddGems(Value);
        TitlePanelPresenter.SetGemsValue(PlayerDataModel.GetGems());
    }

    public void AddTickets(int Value)
    {
        PlayerDataModel.AddTickets(Value);
        TitlePanelPresenter.SetTicketsValue(PlayerDataModel.GetTickets());
    }

    public void AddXp(int Value)
    {
        if (PlayerDataModel.GetXp() + Value > GetNextLevelXp())
        {
            PlayerDataModel.SetXp(PlayerDataModel.GetXp() + Value - GetNextLevelXp());
            SetLevel(PlayerDataModel.GetLevel());
        }
        else
            PlayerDataModel.SetXp(Value);
        
        TitlePanelPresenter.SetXpValue(PlayerDataModel.GetXp());
    }


    private void SetLevel(int Value)
    {
        PlayerDataModel.SetLevel(Value);
        TitlePanelPresenter.SetLevelValue(PlayerDataModel.GetLevel());
    }

    private int GetNextLevelXp()
    {
        return Mathf.RoundToInt(PlayerDataModel.GetLevel() * 0.5f);
    }

}
