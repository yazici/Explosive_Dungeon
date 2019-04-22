using System;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
 
 
public class AdvertisimentShow : MonoBehaviour
{
    public int ad_dieds;
    public static AdvertisimentShow Instance;
    private InterstitialAd interstitial;
 
    public void ShowAd()
    {
        ad_dieds++;
        if(ad_dieds == 3){
            string adUnitId = "ca-app-pub-3009310427348237/3376803074";
            interstitial = new InterstitialAd(adUnitId);
            AdRequest request = new AdRequest.Builder().AddTestDevice(AdRequest.TestDeviceSimulator).AddTestDevice("9A7F9F40F672B81D004178").AddTestDevice("8C81476CEFEB08E7165D1").Build();
            interstitial.LoadAd(request);
            interstitial.OnAdLoaded += OnAdLoaded;
            ad_dieds = 0;
            print("Advertisement showed!");
        }
    }
    public void OnAdLoaded(object sender, System.EventArgs args) => interstitial.Show();
    public void Start()
    {
        string appId = "ca-app-pub-3009310427348237~5778292052";
        MobileAds.Initialize(appId);
        Instance = this;
    }
}
