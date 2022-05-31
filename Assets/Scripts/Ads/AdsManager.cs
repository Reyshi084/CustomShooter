using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{
    public string gameId = "4775519"; //"GameID"を入力
    public string bannerId = "Banner_Android";
    public bool testMode = true;

    private static bool isDisplay = false;


    // Start is called before the first frame update
    void Start()
    {
        if (!isDisplay)
        {
            Advertisement.Initialize(gameId, testMode);
            StartCoroutine(ShowBanner());
            isDisplay = true;
        }
    }
    IEnumerator ShowBanner()
    {
        while (!Advertisement.isInitialized)
        {
            yield return new WaitForSeconds(0.3f); // 0.3秒後に広告表示
        }
        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER); //バナーを上部中央にセット
        Advertisement.Banner.Show(bannerId);
    }
}
