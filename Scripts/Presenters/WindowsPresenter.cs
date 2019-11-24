using UnityEngine;
using System.Collections;
using Doozy.Engine.UI;

public class WindowsPresenter : MonoBehaviour
{
    private static WindowsPresenter instance;
    public UIView WindowsBgAttenuationView, CoinsWarnView, GemsWarnView, TicketsWarnView;
    public UIButton CoinsShowADSButton, GemsShowADSButton, TicketsShowADSButton;

    private void Awake()
    {
        instance = this;
        StartCoroutine(AdsReadyCheck());
    }

    public static void ShowMoneyWarn()
    {
        instance.CoinsWarnView.Show();
        instance.WindowsBgAttenuationView.Show();
    }

    public static void ShowGemsWarn()
    {
        instance.GemsWarnView.Show();
        instance.WindowsBgAttenuationView.Show();
    }

    public static void ShowTicketsWarn()
    {
        instance.TicketsWarnView.Show();
        instance.WindowsBgAttenuationView.Show();
    }

    public static void HideCoinsWarn()
    {
        instance.CoinsWarnView.Hide();
        instance.WindowsBgAttenuationView.Hide();
    }

    public static void HideGemsWarn()
    {
        instance.GemsWarnView.Hide();
        instance.WindowsBgAttenuationView.Hide();
    }

    public static void HideTicketsWarn()
    {
        instance.TicketsWarnView.Hide();
        instance.WindowsBgAttenuationView.Hide();
    }

    private bool IsADSReady()
    {
        bool Ready = ADSManager.IsRewardedAdReady();

        CoinsShowADSButton.Interactable = Ready;
        GemsShowADSButton.Interactable = Ready;
        TicketsShowADSButton.Interactable = Ready;

        return Ready; 
    }

    IEnumerator AdsReadyCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(5);
        while (true)
        {
            IsADSReady();
            yield return wait;
        }
    }
}
