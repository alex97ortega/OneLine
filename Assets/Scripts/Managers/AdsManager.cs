using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_ADS
using UnityEngine.Advertisements;
#endif

public class AdsManager : MonoBehaviour {

    public void ShowAdv()
    {
#if UNITY_ADS
        string videoID = "rewardedVideo";
        if (!Advertisement.IsReady(videoID))        
            Debug.LogWarning("Video not available");
        else
        {
            ShowOptions options = new ShowOptions();
            options.resultCallback = OnAddResult;
            Advertisement.Show(videoID, options);
        }         
#endif    
    }
#if UNITY_ADS
    private void OnAddResult(ShowResult result)
    {
        //call the GameManager to give coins
        if(result == ShowResult.Finished)
        {
            GameManager gm = FindObjectOfType<GameManager>();
            if (gm)
            {
                gm.AddCoins(20);
            }
        }
    }
#endif
}

