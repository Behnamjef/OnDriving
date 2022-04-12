using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance { set; get; }

    public string InterstitialID;
    public string bannerAdUnitID;
    public string RewardVideoID;

    private InterstitialAd interstitial;
    private BannerView Banner;
    private RewardedAd rewardedAd;

    private bool IsRewardAdReady => rewardedAd.IsLoaded();

    public Action OnRewardAdComplete;
    public Action OnAdClosed;

    void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        InitializeInterstitialAds();
        InitializeRewardedAd();
        BannerShow();
    }

    #region Interstitial

    void InitializeInterstitialAds()
    {
        LoadInterstitial();
        interstitial.OnAdClosed += Interstitial_OnAdClosed;
        interstitial.OnAdFailedToLoad += Interstitial_OnAdFailedToLoad;
    }

    private void Interstitial_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        LoadInterstitial();
    }

    private void Interstitial_OnAdClosed(object sender, EventArgs e)
    {
        OnAdClosed?.Invoke();
        LoadInterstitial();
    }

    private void LoadInterstitial()
    {
        interstitial = new InterstitialAd(InterstitialID);

        AdRequest adRequest = new AdRequest.Builder().Build();
        interstitial.LoadAd(adRequest);
    }


    public void ShowInterstitial()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
    }

    #endregion

    #region Banner

    #region Reward ad

    public void BannerShow()
    {
        Banner = new BannerView(bannerAdUnitID, AdSize.Banner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        Banner.LoadAd(request);
    }

    #endregion

    private void InitializeRewardedAd()
    {
        rewardedAd = new RewardedAd(RewardVideoID);
        LoadRewardAd();

        // Called when an ad request has successfully loaded.
        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;
    }

    private void LoadRewardAd()
    {
        // Create an empty ad request.
        var request = new AdRequest.Builder().Build();

        // Load the rewarded ad with the request.
        rewardedAd.LoadAd(request);
    }

    private void HandleRewardedAdClosed(object sender, EventArgs e)
    {
        OnAdClosed?.Invoke();
        LoadRewardAd();
    }

    private void HandleUserEarnedReward(object sender, Reward e)
    {
        OnRewardAdComplete?.Invoke();
    }

    private void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs e)
    {
        LoadRewardAd();
    }

    private void HandleRewardedAdOpening(object sender, EventArgs e)
    {
    }

    private void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        LoadRewardAd();
    }

    private void HandleRewardedAdLoaded(object sender, EventArgs e)
    {
    }

    public void ShowRewardAd()
    {
        if (IsRewardAdReady) {
            rewardedAd.Show();
        }
    }

    #endregion
}