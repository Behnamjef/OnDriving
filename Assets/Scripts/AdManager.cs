using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour
{
    public static AdManager Instance { set; get; }

    public string InterstitialID;
    public string bannerAdUnitID;
    public string RewardVideoID;


    InterstitialAd interstitial;
    BannerView Banner;


    void Awake()
    {

        Instance = this;

    }


    void Start()
    {

        InitializeInterstitialAds();
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
   
        public void BannerShow()
        {
        Banner = new BannerView(bannerAdUnitID, AdSize.Banner, AdPosition.Top);
        AdRequest request = new AdRequest.Builder().Build();
        Banner.LoadAd(request);

        }

        #endregion


    }

