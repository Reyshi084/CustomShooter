using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class MovieAdsManager : MonoBehaviour
{

    string gameId = "4775519";
    string mySurfacingId = "Interstitial_Android";
    [SerializeField] bool testMode = true;

    void Start()
    {
        // 広告サービスの初期化:
        Advertisement.Initialize(gameId, testMode);
    }

    public void ShowInterstitialAd()
    {
        // Showメソッドを呼ぶ前にUnity Adsの準備ができているかチェック:
        if (Advertisement.IsReady())
        {
            Advertisement.Show(mySurfacingId);
            PlayerData.CLEARCNT = 0;
        }
        else
        {
            Debug.Log("インタースティシャル広告の準備が現在できていません。後ほどもう一度お試しください。");
        }

        SceneManager.LoadScene("SelectScene");
    }
}
