using UnityEngine;
using System.Collections;

[System.Serializable]
public struct CurrenciesData
{
    public int Coins, Gems, Tickets;

    public CurrenciesData(int coins, int gems, int tickets)
    {
        Coins = coins;
        Gems = gems;
        Tickets = tickets;
    }
}
