using UnityEngine;
using UnityEngine.Advertisements;

public class ADManager : MonoBehaviour, IUnityAdsListener
{
    #if UNITY_IOS
        private string gameId = "4370528";
            
        private string rewardedVideoID = "Rewarded_iOS";
    #else
        string gameId = "4370529";
        string rewardedVideoID = "Rewarded_Android";
#endif

    private void Start()
    {
        Advertisement.Initialize(gameId);
        Advertisement.AddListener(this);
    }

    public void PlayRewardedVideo()
    {
        if(Advertisement.IsReady(rewardedVideoID))
        {
            Advertisement.Show(rewardedVideoID);
        }
        else
        {
            Debug.Log("AD not Ready");
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        return;
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log("Error: " + message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        return;
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if(placementId == rewardedVideoID && showResult == ShowResult.Finished)
        {
            GameManager.Instance.RewardVideo();
        }
    }
}
