using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_ADS
using UnityEngine.Advertisements;
#endif

public class AdsManager : MonoBehaviour {

    int _type;
    uint coinsToGive;

    // type: 0 para dar monedas
    //       1 para challenge
    //       2 para monedas x2
    //       3 para video non-rewarded
    public void ShowAdv(int type)
    {
#if UNITY_ADS
        string videoID;
        if(type == 3)
            videoID = "video";
        else
            videoID = "rewardedVideo";

        if (!Advertisement.IsReady(videoID))
        {
            Debug.LogWarning("Video not available");
            coinsToGive = 0;
        }        
        else
        {
            _type = type;
            ShowOptions options = new ShowOptions();
            options.resultCallback = OnAddResult;
            Advertisement.Show(videoID, options);
        }         
#endif    
    }
#if UNITY_ADS
    private void OnAddResult(ShowResult result)
    {
        //do action
        if(result == ShowResult.Finished)
        {
            switch (_type)
            {
                case 0:
                    {
                        GameManager gm = FindObjectOfType<GameManager>();
                        if (gm)
                        {
                            gm.AddCoins(coinsToGive);
                            coinsToGive = 0;
                        }
                        break;
                    }
                case 1:
                    {
                        ScenesManager sm = FindObjectOfType<ScenesManager>();
                        if (sm)
                        {
                            sm.ChangeToChallenge(0);
                        }
                        break;
                    }
                case 2:
                    {
                        GameManager gm = FindObjectOfType<GameManager>();
                        if (gm)
                        {
                            gm.AddCoins(coinsToGive*2);
                            coinsToGive = 0;
                            ScenesManager sm = FindObjectOfType<ScenesManager>();
                            if (sm)
                            {
                                sm.ChangeToMainMenu();
                            }
                        }
                        break;
                    }
                default:
                    break;
            }
            
        }
    }
#endif
    public void AddCoins(int coins)
    {
        coinsToGive = (uint)coins;
    }
}

