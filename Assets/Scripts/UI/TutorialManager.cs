using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] GameObject _filter;

    [SerializeField] TutorialButtonManager _initialStart; //1-1s
    [SerializeField] TutorialButtonManager _custom; //1-1c
    [SerializeField] TutorialButtonManager _star; //1-2c
    [SerializeField] TutorialButtonManager _abiCustom; //1-5c
    [SerializeField] TutorialButtonManager _square; //1-6s
    [SerializeField] TutorialButtonManager _poison; //2-1s
    [SerializeField] TutorialButtonManager _fire; //2-2s
    [SerializeField] TutorialButtonManager _stream; //2-3s
    [SerializeField] TutorialButtonManager _lightning; //2-4s
    [SerializeField] TutorialButtonManager _cold; //2-6s
    [SerializeField] TutorialButtonManager _hypnosis; //2-7s
    [SerializeField] TutorialButtonManager _slash; //2-8s
    [SerializeField] TutorialButtonManager _chain; //2-9s
    [SerializeField] TutorialButtonManager _efeCustom; //2-10c
    [SerializeField] TutorialButtonManager _pentagon; //3-6s
    [SerializeField] TutorialButtonManager _hexagon; //4-6s

    private TutorialButtonManager _nowTutorial;

    public void DisplayTutorial(int stageNum, bool isStart, int befStage)
    {
        _nowTutorial = null;

        // スタート時にチュートリアルを出すかどうか
        var sflag = stageNum == PlayerData.STAGE && isStart;
        // クリア時にチュートリアルを出すかどうか
        var cflag = stageNum == befStage && !isStart;

        if (!(sflag || cflag))
        {
            return;
        }

        if (isStart)
        {
            switch (stageNum)
            {
                case 1:
                    _nowTutorial = _initialStart;
                    break;
                case 6:
                    _nowTutorial = _square;
                    break;
                case 11:
                    _nowTutorial = _poison;
                    break;
                case 12:
                    _nowTutorial = _fire;
                    break;
                case 13:
                    _nowTutorial = _stream;
                    break;
                case 14:
                    _nowTutorial = _lightning;
                    break;
                case 16:
                    _nowTutorial = _cold;
                    break;
                case 17:
                    _nowTutorial = _hypnosis;
                    break;
                case 18:
                    _nowTutorial = _slash;
                    break;
                case 19:
                    _nowTutorial = _chain;
                    break;
                case 26:
                    _nowTutorial = _pentagon;
                    break;
                case 36:
                    _nowTutorial = _hexagon;
                    break;
            }
        }
        else
        {
            switch (stageNum)
            {
                case 1:
                    _nowTutorial = _custom;
                    break;
                case 2:
                    _nowTutorial = _star;
                    break;
                case 5:
                    _nowTutorial = _abiCustom;
                    break;
                case 20:
                    _nowTutorial = _efeCustom;
                    break;
            }
        }


        if (_nowTutorial == null)
        {
            return;
        }
        
        if (isStart)
        {
            _filter.gameObject.SetActive(true);
            _nowTutorial = Instantiate(_nowTutorial, transform);
            _nowTutorial.SetInfo(_filter);
            Time.timeScale = 0;
        }
        else
        {
            _nowTutorial = Instantiate(_nowTutorial, transform);
            _nowTutorial.SetInfo(null);
        }

        _nowTutorial.transform.localPosition = new Vector3(0, 0);
        
    }

    public void OnBackButtonDown()
    {
        Destroy(_nowTutorial);
        _filter.gameObject.SetActive(false);
    }
}
