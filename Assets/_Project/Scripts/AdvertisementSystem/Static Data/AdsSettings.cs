using UnityEngine;

namespace AdvertisementSystem.Static_Data
{
    [CreateAssetMenu(fileName = "AdsSettings", menuName = "Game/AdsSettings")]
    public class AdsSettings : ScriptableObject
    {
        [Header("Game IDs")]
        public string AndroidGameId;
        public string IOSGameId;

        [Header("Reward Settings")]
        public int InterstitialRewardScore = 50;

        [Header("Testing")]
        public bool TestMode = true;

        public string GameId
        {
            get
            {
                #if UNITY_IOS
                return IOSGameId;
                #elif UNITY_ANDROID || UNITY_EDITOR
                return AndroidGameId;
                #else
                return string.Empty;
                #endif
            }
        }

        public string BannerAdUnitId
        {
            get
            {
                #if UNITY_IOS
                return "Banner_iOS";
                #elif UNITY_ANDROID || UNITY_EDITOR
                return "Banner_Android";
                #else
                return string.Empty;
                #endif
            }
        }

        public string InterstitialAdUnitId
        {
            get
            {
                #if UNITY_IOS
                return "Interstitial_iOS";
                #elif UNITY_ANDROID || UNITY_EDITOR
                return "Interstitial_Android";
                #else
                return string.Empty;
                #endif
            }
        }
    }
}