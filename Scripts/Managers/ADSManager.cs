using UnityEngine;
using System.Collections;
using EasyMobile;
using System;

public class ADSManager : MonoBehaviour
{
    private static ADSManager instance;

    private void Awake()
    {
        instance = this;

        if (!RuntimeManager.IsInitialized())
            RuntimeManager.Init();
    }

    public static void HideBanner()     //TODO:сделать показ баннеров в определенных местах
    {
        Advertising.HideBannerAd(BannerAdNetwork.UnityAds, AdPlacement.Default);
    }

    public static void ShowBannerLeft()
    {
        Advertising.ShowBannerAd(BannerAdNetwork.UnityAds, BannerAdPosition.BottomLeft, BannerAdSize.Banner);
    }

    public enum RewardType { Coins, Gems, Tickets, None }
    private int countToAdd;
    public static void ShowRewardedAds(RewardType rewardType, int Count)
    {
        if (IsRewardedAdReady() == false) return;

        instance.countToAdd = Count;
        switch (rewardType)
        {
            case RewardType.Coins:
                Advertising.RewardedAdCompleted += instance.RewardCoins;
                break;

            case RewardType.Gems:
                Advertising.RewardedAdCompleted += instance.RewardGems;
                break;

            case RewardType.Tickets:
                Advertising.RewardedAdCompleted += instance.RewardTickets;
                break;
        }

        Advertising.ShowRewardedAd(RewardedAdNetwork.UnityAds, AdPlacement.Default);
    }

    public void ShowRewardedCoinsAds(int Count)
    {
        instance.countToAdd = Count;

        if (IsRewardedAdReady())
        {
            Advertising.RewardedAdCompleted += instance.RewardCoins;
            Advertising.ShowRewardedAd(RewardedAdNetwork.UnityAds, AdPlacement.Default);
        }
    }

    public void ShowRewardedGemsAds(int Count)
    {
        instance.countToAdd = Count;

        if (IsRewardedAdReady())
        {
            Advertising.RewardedAdCompleted += instance.RewardGems;
            Advertising.ShowRewardedAd(RewardedAdNetwork.UnityAds, AdPlacement.Default);
        }
    }

    public void ShowRewardedTicketsAds(int Count)
    {
        instance.countToAdd = Count;

        if (IsRewardedAdReady())
        {
            Advertising.RewardedAdCompleted += instance.RewardTickets;
            Advertising.ShowRewardedAd(RewardedAdNetwork.UnityAds, AdPlacement.Default);
        }
    }

    private void RewardCoins(RewardedAdNetwork rewardedAdNet, AdPlacement adPlacement)
    {
        PlayerDataModel.AddCoins(countToAdd);
        Advertising.RewardedAdCompleted -= RewardCoins;
    }

    private void RewardGems(RewardedAdNetwork rewardedAdNet, AdPlacement adPlacement)
    {
        PlayerDataModel.AddCoins(countToAdd);
        Advertising.RewardedAdCompleted -= RewardGems;
    }

    private void RewardTickets(RewardedAdNetwork rewardedAdNet, AdPlacement adPlacement)
    {
        PlayerDataModel.AddCoins(countToAdd);
        Advertising.RewardedAdCompleted -= RewardTickets;
    }


    public static void ShowInterstitialAds()
    {
        if (IsInterstitialAdReady())
        {
            Advertising.ShowInterstitialAd(InterstitialAdNetwork.UnityAds, AdPlacement.Default);
        }
    }

    public static bool IsRewardedAdReady()
    {
        return Advertising.IsRewardedAdReady(RewardedAdNetwork.UnityAds, AdPlacement.Default);
    }

    public static bool IsInterstitialAdReady()
    {
        return Advertising.IsInterstitialAdReady(InterstitialAdNetwork.UnityAds, AdPlacement.Default);
    }
}
