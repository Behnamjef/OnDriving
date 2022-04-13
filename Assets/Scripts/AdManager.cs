using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance { set; get; }

#if UNITY_ANDROID
    private string InterstitialID = "ca-app-pub-3940256099942544/1033173712";
    private string bannerAdUnitID = "ca-app-pub-3940256099942544/6300978111";
    private string RewardVideoID = "ca-app-pub-3940256099942544/5224354917";
    
#elif UNITY_IOS
    private string InterstitialID = "ca-app-pub-3940256099942544/4411468910";
    private string bannerAdUnitID = "ca-app-pub-3940256099942544/2934735716";
    private string RewardVideoID = "ca-app-pub-3940256099942544/1712485313";
#endif
    
    private InterstitialAd interstitial;
    private BannerView Banner;
    private RewardedAd rewardedAd;

    public bool IsRewardAdReady => rewardedAd.IsLoaded();
    public bool IsInterstitialAdReady => interstitial.IsLoaded();

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


    public bool ShowInterstitial()
    {
        if (!IsInterstitialAdReady) return false;
        interstitial.Show();
        return true;

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

    public bool ShowRewardAd()
    {
        if (!IsRewardAdReady) return false;
        rewardedAd.Show();
        return true;

    }

    #endregion
}