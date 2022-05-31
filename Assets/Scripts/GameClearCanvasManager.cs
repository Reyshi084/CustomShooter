using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClearCanvasManager : MonoBehaviour
{
    [SerializeField] GameObject _gameclearWindow;
    [SerializeField] GameObject _gameoverWindow;
    [SerializeField] GameObject _filter;

    [SerializeField] Text _gainExpText;
    [SerializeField] Text _lvUpText;
    [SerializeField] Text _maxLvText;

    [SerializeField] TutorialManager _tutMng;

    private void Awake()
    {
        _gameclearWindow.transform.localScale = new Vector3(0, 0, 1);
        _gameoverWindow.transform.localScale = new Vector3(0, 0, 1);
        _gameclearWindow.gameObject.SetActive(false);
        _gameoverWindow.gameObject.SetActive(false);
        _filter.gameObject.SetActive(false);
    }

    public void GameClear(int exp, int befLv, int aftLv, int itemCnt, int befStage, int aftStage)
    {
        PlayerData.CLEARCNT++;
        _gainExpText.text = "EXP:" + exp + " (" + PlayerData.EXP + ")";
        if (aftLv - befLv != 0)
        {
            _lvUpText.text = "LV UP!!  (" + befLv + "->" + aftLv + ")";
        }
        else
        {
            _lvUpText.text = "";
        }

        if(aftStage - befStage == 1)
        {
            _tutMng.DisplayTutorial(SelectSceneManager.SelectStageNum, false, befStage);

            if (befStage % 5 == 0)
            {
                var newMaxLv = befStage + 5;
                _maxLvText.text = "MAXLV UP!!  (" + befStage + "->" + newMaxLv + ")";
            }
            else
            {
                _maxLvText.text = "";
            }
        }
        else
        {
            _maxLvText.text = "";
        }
        

        StartCoroutine(DisplayWindow(true));
        
        
    }

    public void GameOver()
    {
        StartCoroutine(DisplayWindow(false));
    }

    private IEnumerator DisplayWindow(bool cleared)
    {
        Time.timeScale = 0;

        if (cleared)
        {
            _gameclearWindow.gameObject.SetActive(true);
            _filter.gameObject.SetActive(true);

            for (int i = 0; i < 20; i++)
            {
                _gameclearWindow.transform.localScale += new Vector3(0.025f, 0.025f, 0);
                yield return new WaitForSecondsRealtime(0.01f);
            }
        }
        else
        {
            _gameoverWindow.gameObject.SetActive(true);
            _filter.gameObject.SetActive(true);

            for (int i = 0; i < 20; i++)
            {
                _gameoverWindow.transform.localScale += new Vector3(0.025f, 0.025f, 0);
                yield return new WaitForSecondsRealtime(0.01f);
            }
        }
    }
}
