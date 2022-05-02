using System;
using System.Threading.Tasks;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance { set; get; }

#if UNITY_ANDROID
    private string InterstitialID = "ca-app-pub-4769515775892914/3503650607";
    private string RewardVideoID = "ca-app-pub-4769515775892914/8564405598";
    private string bannerAdUnitID = "ca-app-pub-4769515775892914/6129813945";
    private string AppOpenID = "ca-app-pub-3940256099942544/3419835294"; // Test id
#elif UNITY_IOS
    private string InterstitialID = "ca-app-pub-4769515775892914/4802659525";
    private string RewardVideoID = "ca-app-pub-4769515775892914/3273121280";
    private string bannerAdUnitID = "ca-app-pub-4769515775892914/5844278457";
    private string AppOpenID = "ca-app-pub-3940256099942544/5662855259"; // Test id
#endif

    private InterstitialAd interstitial;
    private BannerView Banner;
    private RewardedAd rewardedAd;
    private AppOpenAd appOpenAd;

    public bool IsRewardAdReady => rewardedAd.IsLoaded();
    public bool IsInterstitialAdReady => interstitial.IsLoaded();
    private bool IsAppOpenAdAvailable => appOpenAd != null;
    private bool _isShowingAppOpenAd = false;
    
    public Action OnRewardAdComplete;
    public Action OnAdClosed;

    void Awake()
    {
        Instance = this;
        InitializeBanner();
    }

    void Start()
    {
        InitializeInterstitialAds();
        InitializeRewardedAd();
        BannerShow();
        LoadAppOpenAd();
    }

    #region Interstitial

    void InitializeInterstitialAds()
    {
        interstitial = new InterstitialAd(InterstitialID);

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

    private void InitializeBanner()
    {
        Banner = new BannerView(bannerAdUnitID, AdSize.Banner, AdPosition.Bottom);
    }
    
    public void BannerShow()
    {
        AdRequest request = new AdRequest.Builder().Build();
        Banner.LoadAd(request);
    }

    #endregion

    #region Reward ad

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

    #region App Open ad


    public void LoadAppOpenAd()
    {
        return;
        AdRequest request = new AdRequest.Builder().Build();

        // Load an app open ad for portrait orientation
        AppOpenAd.LoadAd(AppOpenID, ScreenOrientation.Portrait, request, ((appOpenAd, error) =>
        {
            if (error != null)
            {
                // Handle the error.
                Debug.LogFormat("Failed to load the ad. (reason: {0})", error.LoadAdError.GetMessage());
                return;
            }

            // App open ad is loaded.
            this.appOpenAd = appOpenAd;
        }));
    }
    
    private async void ShowAppOpenIfAvailable()
    {
        await ShowAppOpenAd();
    }

    private async Task ShowAppOpenAd()
    {
        return;
        if(_isShowingAppOpenAd)
            return;
        
        while (!IsAppOpenAdAvailable)
        {
            await Task.Delay(500);
        }

        appOpenAd.OnAdDidDismissFullScreenContent += HandleAdDidDismissFullScreenContent;
        appOpenAd.OnAdFailedToPresentFullScreenContent += HandleAdFailedToPresentFullScreenContent;
        appOpenAd.OnAdDidPresentFullScreenContent += HandleAdDidPresentFullScreenContent;
        appOpenAd.OnAdDidRecordImpression += HandleAdDidRecordImpression;
        appOpenAd.OnPaidEvent += HandlePaidEvent;
        
        appOpenAd.Show();
    }
    
    private void HandleAdDidDismissFullScreenContent(object sender, EventArgs args)
    {
        Debug.Log("Closed app open ad");
        // Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
        appOpenAd = null;
        _isShowingAppOpenAd = false;
        LoadAppOpenAd();
    }

    private void HandleAdFailedToPresentFullScreenContent(object sender, AdErrorEventArgs args)
    {
        Debug.LogFormat("Failed to present the ad (reason: {0})", args.AdError.GetMessage());
        // Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
        appOpenAd = null;
        LoadAppOpenAd();
    }

    private void HandleAdDidPresentFullScreenContent(object sender, EventArgs args)
    {
        Debug.Log("Displayed app open ad");
        _isShowingAppOpenAd = true;
    }

    private void HandleAdDidRecordImpression(object sender, EventArgs args)
    {
        Debug.Log("Recorded ad impression");
    }

    private void HandlePaidEvent(object sender, AdValueEventArgs args)
    {
        Debug.LogFormat("Received paid event. (currency: {0}, value: {1}",
            args.AdValue.CurrencyCode, args.AdValue.Value);
    }
    
    #endregion

    private void OnApplicationPause(bool pauseStatus)
    {
        if(!pauseStatus)
            ShowAppOpenIfAvailable();
    }
}