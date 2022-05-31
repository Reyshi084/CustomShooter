using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageClearButtonManager : MonoBehaviour
{

    [SerializeField] MovieAdsManager movieAdsManager;
    public void OnStageSelectButtonDown()
    {
        Time.timeScale = 1;
        SelectSceneManager.CurrentState = SelectSceneManager.State.Stage;
        DisplayAds();
    }

    public void OnCustomButtonDown()
    {
        Time.timeScale = 1;
        SelectSceneManager.CurrentState = SelectSceneManager.State.Custom;
        DisplayAds();
    }

    public void OnRetryButtonManager()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void DisplayAds()
    {
        if(PlayerData.CLEARCNT >= Utils.AdsInterval)
        {
            movieAdsManager.ShowInterstitialAd();
        }
        else
        {
            SceneManager.LoadScene("SelectScene");
        }

    }
}
