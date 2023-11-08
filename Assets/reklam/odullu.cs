using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class odullu : MonoBehaviour {

    public static odullu instance;

    [SerializeField] Button but;
    [SerializeField] bool b2x, odulverildi;

    private string _adUnitId = "ca-app-pub-6299849283659692/5205947459";

    private RewardedAd _rewardedAd;

    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.
        });

        StartCoroutine(bekleload());
    }

    IEnumerator bekleload()
    {
        yield return new WaitForSeconds(5f);
        LoadRewardedAd();
    }

    private void Update()
    {
        if (but == null)
            but = menumanager.menumanagersc.button2x;

        if (_rewardedAd == null && but != null)
        {
            but.gameObject.SetActive(false);
            return;
        }

        if (_rewardedAd.CanShowAd() && but != null)
        {
            but.onClick.RemoveAllListeners();
            but.onClick.AddListener(() => ShowRewardedAd());
            but.gameObject.SetActive(true);
        }
        else if (but != null)
        {
            but.onClick.RemoveAllListeners();
            but.gameObject.SetActive(false);
        }
    }

    public void LoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (_rewardedAd != null)
        {
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        RewardedAd.Load(_adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
              // if error is not null, the load request failed.
              if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                _rewardedAd = ad;
            });
    }

    public void ShowRewardedAd()
    {
        const string rewardMsg =
            "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        RegisterEventHandlers(_rewardedAd);

        if (_rewardedAd != null && _rewardedAd.CanShowAd())
        {
            _rewardedAd.Show((Reward reward) =>
            {
                // TODO: Reward the user.
                Debug.Log(string.Format(rewardMsg, reward.Type, reward.Amount));
            });

            StartCoroutine(bekleyoket());
        }
    }

    IEnumerator bekleyoket()
    {
        yield return new WaitForSeconds(1f);
        _rewardedAd.Destroy();
        print("reklamkapatildi");
        yield return new WaitForSeconds(1f);
        LoadRewardedAd();
    }

    private void RegisterEventHandlers(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(string.Format("Rewarded ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded ad recorded an impression.");

            if (!odulverildi)
            {
                if (!b2x)
                    PlayerPrefs.SetFloat("money", PlayerPrefs.GetFloat("money") + 1000f);
                else
                {
                    PlayerPrefs.SetFloat("money", PlayerPrefs.GetFloat("money") + gamemanager.managersc.money);
                }

                odulverildi = true;
            }
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Rewarded ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
    {
            Debug.Log("Rewarded Ad full screen content closed.");

            odulverildi = false;
            // Reload the ad so that we can show another as soon as possible.
            LoadRewardedAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);
            
            // Reload the ad so that we can show another as soon as possible.
            LoadRewardedAd();
        };
    }
}
