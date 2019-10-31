using UnityEngine;
using System.Collections;
using Doozy.Engine.Progress;

public class TitlePanelPresenter : MonoBehaviour
{
    private static TitlePanelPresenter instance;
    public Progressor CoinsProg, GemsProg, TicketsProg, XpProg, LevelProg;

    private void Awake()
    {
        instance = this;
    }

    public static void SetCoinsValue(int Value)
    {
        instance.CoinsProg.SetMax(Value);
        instance.CoinsProg.SetValue(Value);
    }

    public static void SetGemsValue(int Value)
    {
        instance.GemsProg.SetMax(Value);
        instance.GemsProg.SetValue(Value);
    }

    public static void SetTicketsValue(int Value)
    {
        instance.TicketsProg.SetMax(Value);
        instance.TicketsProg.SetValue(Value);
    }

    public static void SetXpValue(int Value)
    {
        instance.XpProg.SetMax(Value);
        instance.XpProg.SetValue(Value);
    }

    public static void SetLevelValue(int Value)
    {
        instance.LevelProg.SetMax(Value);
        instance.LevelProg.SetValue(Value);
    }
}
