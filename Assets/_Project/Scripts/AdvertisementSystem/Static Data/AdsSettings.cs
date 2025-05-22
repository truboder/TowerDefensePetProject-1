using UnityEngine;

namespace AdvertisementSystem.Static_Data
{
    [CreateAssetMenu(fileName = "AdsSettings", menuName = "Game/AdsSettings")]
    public class AdsSettings : ScriptableObject
    {
        [Header("Game IDs")]
        public string AndroidGameId;
        public string IOSGameId;

        [Header("Banner Ad Unit IDs")]
        public string AndroidBannerAdUnitId = "Banner_Android";
        public string IOSBannerAdUnitId = "Banner_iOS";

        [Header("Interstitial Ad Unit IDs")]
        public string AndroidInterstitialAdUnitId = "Interstitial_Android";
        public string IOSInterstitialAdUnitId = "Interstitial_iOS";

        [Header("Reward Settings")]
        public int InterstitialRewardScore = 50;

        [Header("Testing")]
        public bool TestMode = true;
    }
}