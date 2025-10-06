// using System;
// using System.Collections;
// using DG.Tweening;
// using GoogleMobileAds.Api;
// using NUnit.Framework.Constraints;
// using TMPro;
// using UnityEngine;
//
//
// namespace Zocat
// {
//     public class AdsPanel : UIPanel
//     {
//         private Action _successCallback;
//         private Tween _countTw;
//         private InterstitialAd _interstitial;
//
//         #region Ids
//
// #if UNITY_ANDROID
//         private const string _IntersID = "ca-app-pub-9892823722093394/7180533083";
//
// #elif UNITY_IOS
//         private const string _IntersID = "ca-app-pub-9892823722093394/1813506396";
// #endif
//
//         #endregion
//
//         /*--------------------------------------------------------------------------------------*/
//         private void Start()
//         {
//             LoadInterstitial();
//         }
//
//         /*------------------------------INTERS--------------------------------------------------------*/
//         private void LoadInterstitial()
//         {
//             if (_interstitial != null)
//             {
//                 _interstitial.Destroy();
//                 _interstitial = null;
//             }
//
//             Debug.Log("Loading the interstitial ad.");
//             var adRequest = new AdRequest();
//             InterstitialAd.Load(_IntersID, adRequest,
//                 (InterstitialAd ad, LoadAdError error) =>
//                 {
//                     if (error != null || ad == null)
//                     {
//                         Debug.LogError("interstitial ad failed to load an ad " +
//                                        "with error : " + error);
//                         return;
//                     }
//
//                     Debug.Log("Interstitial ad loaded with response : "
//                               + ad.GetResponseInfo());
//
//                     _interstitial = ad;
//                 });
//         }
//
//         public void ShowInterstitial()
//         {
//             if (!Available) return;
//             if (_interstitial != null && _interstitial.CanShowAd())
//             {
//                 Debug.Log("Showing interstitial ad.");
//                 _interstitial.Show();
//                 LoadInterstitial();
//             }
//             else
//             {
//                 Debug.LogError("Interstitial ad is not ready yet.");
//             }
//         }
//
//         private bool Available => LevelIndexManager.CurrentIndex > 2;
//     }
// }

