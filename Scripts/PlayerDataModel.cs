using UnityEngine;
using System.Collections;
using EasyMobile;

public class PlayerDataModel : MonoBehaviour
{
    public static PlayerDataModel instance;
    public CurrenciesData Currencies = new CurrenciesData();
    public PlayerData Player = new PlayerData();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        LoadData();
    }

    public static int GetCoins()
    {
        return instance.Currencies.Coins;
    }

    public static int GetGems()
    {
        return instance.Currencies.Gems;
    }

    public static int GetTickets()
    {
        return instance.Currencies.Tickets;
    }

    public static int GetXp()
    {
        return instance.Player.XP;
    }

    public static int GetLevel()
    {
        return instance.Player.Level;
    }
    

    public static void AddCoins(int Value)
    {
        instance.Currencies.Coins += Value;
        instance.SaveData();
    }

    public static void AddGems(int Value)
    {
        instance.Currencies.Gems += Value;
        instance.SaveData();
    }

    public static void AddTickets(int Value)
    {
        instance.Currencies.Tickets += Value;
        instance.SaveData();
    }

    public static void AddXp(int Value)
    {
        instance.Player.XP += Value;
        instance.SaveData();
        instance.NextLevelCheck();
    }

    private void NextLevelCheck()
    {
        if (GetXp() > GetNextLevelXP())
            AddLevel(GetLevel() + 1);
    }

    public static float GetNextLevelXP()
    {
        return  GetLevel() * 2;
    }

    public static float GetPreviousLevelXP()
    {
        return GetLevel() != 0 ? (GetLevel() - 1) * 2 : 0;
    }

    public static void AddLevel(int Value)
    {
        instance.Player.Level += Value;
        instance.SaveData();
    }

    public static void SetXp(int Value)
    {
        instance.Player.XP = Value;
        instance.SaveData();
    }

    public static void SetLevel(int Value)
    {
        instance.Player.Level = Value;
        instance.SaveData();
    }


    public static bool TakeCoins(int Value)
    {
        if(GetCoins() - Value >= 0)
        {
            instance.Currencies.Coins -= Value;
            instance.SaveData();
            return true;
        }
        else return false;
    }

    public static bool TakeGems(int Value)
    {
        if(GetGems() - Value >= 0)
        {
            instance.Currencies.Gems -= Value;
            instance.SaveData();
            return true;
        }
        else return false;
    }

    public static bool TakeTickets(int Value)
    {
        if(GetTickets() - Value >= 0)
        {
            instance.Currencies.Tickets -= Value;
            instance.SaveData();
            return true;
        }
        else return false;
    }


    private void SaveData()
    {
        PlayerPrefs.SetString("Currensies", JsonUtility.ToJson(Currencies));
        PlayerPrefs.SetString("Player", JsonUtility.ToJson(Player));
        UpdateView();
    }

    private void LoadData()
    {
        if (HasOldSave())
        {
            LoadOldSave();
        }
        if (HasSave())
            Currencies = JsonUtility.FromJson<CurrenciesData>(PlayerPrefs.GetString("Currensies"));
            Player = JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString("Player"));

        UpdateView();
    }

    private void LoadOldSave()
    {
        GameServices.UnlockAchievement("Length of service"); //TODO: убрать это и сделать класс АчивментсМенеджер
    }

    private void UpdateView()
    {
        TitlePanelPresenter.SetCoinsValue(PlayerDataModel.GetCoins());
        TitlePanelPresenter.SetGemsValue(PlayerDataModel.GetGems());
        TitlePanelPresenter.SetTicketsValue(PlayerDataModel.GetTickets());
        TitlePanelPresenter.SetXpValue(PlayerDataModel.GetXp());
        TitlePanelPresenter.SetLevelValue(PlayerDataModel.GetLevel());
    }

    private bool HasSave()
    {
        return PlayerPrefs.HasKey("Currensies");
    }

    private bool HasOldSave()
    {
       return PlayerPrefs.HasKey("CoinG");
    }
}