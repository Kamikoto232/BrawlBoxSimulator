using UnityEngine;
using System.Collections;

public class WindowsManager : MonoBehaviour
{
    public static void ShowCoinWarn()
    {
        WindowsPresenter.ShowMoneyWarn();
    }

    public static void ShowGemsWarn()
    {
        WindowsPresenter.ShowGemsWarn();
    }

    public static void ShowTicketsWarn()
    {
        WindowsPresenter.ShowTicketsWarn();
    }

    public static void ShowADSWarn()
    {
        WindowsPresenter.ShowGemsWarn();
    }


    public void HideCoinsWarn()
    {
        WindowsPresenter.HideCoinsWarn();
    }

    public void HideGemsWarn()
    {
        WindowsPresenter.HideGemsWarn();
    }

    public void HideTicketsWarn()
    {
        WindowsPresenter.HideTicketsWarn();
    }

    public static void HideADSWarn()
    {
        WindowsPresenter.HideTicketsWarn();
    }
}
