using UnityEngine;
using System.Collections;

public class PlayerDataModel : MonoBehaviour
{
    public static PlayerDataModel instance;
    public CurrenciesData Currencies = new CurrenciesData();

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
        return 0;
    }

    public static int GetLevel()
    {
        return 0;
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


    public static void SetXp(int Value)
    {
        
    }

    public static void SetLevel(int Value)
    {

    }
     

    public static bool TakeCoins(int Value)
    {
        if(Value - GetCoins() >= 0)
        {
            instance.Currencies.Coins -= Value;
            return true;
        }
        else return false;
    }

    public static bool TakeGems(int Value)
    {
        if(Value - GetGems() >= 0)
        {
            instance.Currencies.Gems -= Value;
            return true;
        }
        else return false;
    }

    public static bool TakeTickets(int Value)
    {
        if(Value - GetTickets() >= 0)
        {
            instance.Currencies.Tickets -= Value;
            return true;
        }
        else return false;
    }


    private void SaveData()
    {
        PlayerPrefs.SetString("Currensies", JsonUtility.ToJson(Currencies));
    }

    private void LoadData()
    {
        if (HasSave())
            Currencies = JsonUtility.FromJson<CurrenciesData>(PlayerPrefs.GetString("Currensies"));

        UpdateView();
    }

    private void UpdateView()
    {
        TitlePanelPresenter.SetCoinsValue(PlayerDataModel.GetCoins());
        TitlePanelPresenter.SetGemsValue(PlayerDataModel.GetGems());
        TitlePanelPresenter.SetTicketsValue(PlayerDataModel.GetTickets());
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