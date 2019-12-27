using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_ADS
using UnityEngine.Advertisements;
#endif

public class AdsManager : MonoBehaviour {

    int _type;

    // type: 0 para dar monedas
    //       1 para challenge
    //       2 para monedas x2 en challenge
    public void ShowAdv(int type)
    {
#if UNITY_ADS
        string videoID = "rewardedVideo";
        if (!Advertisement.IsReady(videoID))        
            Debug.LogWarning("Video not available");
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
                            gm.AddCoins(20);
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
                            gm.AddCoins(100);
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
}

