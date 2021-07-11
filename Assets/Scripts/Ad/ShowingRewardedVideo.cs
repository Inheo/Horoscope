using UnityEngine;
using UnityEngine.Advertisements;

public class ShowingRewardedVideo : MonoBehaviour, IUnityAdsListener
{
    private void Start()
    {
        Advertisement.AddListener(this);
    }
    public void ShowRewardedVideo()
    {
        if (Advertisement.IsReady("Rewarded_Android"))
        {
            Advertisement.Show("Rewarded_Android");
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log(placementId + " ready");
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log(message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log(placementId + " start");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId == "Rewarded_Android" && showResult == ShowResult.Finished)
        {
            HoroscopePanel.Instance.ShowResultAds();
        }
    }
}
