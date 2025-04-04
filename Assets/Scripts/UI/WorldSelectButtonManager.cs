﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldSelectButtonManager : MonoBehaviour
{
    [SerializeField] Text _text;

    private int _worldNum;
    private StageSelectButtonCreate _stageFrame;
    private WorldSelectManager _worldFrame;

    public void OnButtonDown()
    {
        if(_worldNum == 5 && 5 <= ((PlayerData.STAGE - 1) / Utils.StageSize) + 1)
        {
            StartCoroutine(ComingSoon());
            return;
        }

        if (Utils.WorldSize < _worldNum)
        {
            return;
        }

        if (_worldNum <= ((PlayerData.STAGE - 1) / Utils.StageSize) + 1 && !HomeManager.Mooving)
        {
            SelectSceneManager.SetPrevWorldW(_worldNum);
            SelectSceneManager.CurrentState = SelectSceneManager.State.Stage;
            StartCoroutine(DisplayStageSelect());
        }
    }

    public void SetInfo(int worldNum, StageSelectButtonCreate stageFrame, WorldSelectManager worldFrame)
    {
        _worldNum = worldNum;
        _stageFrame = stageFrame;
        _worldFrame = worldFrame;
        SetText();
    }

    private void SetText()
    {
        if((PlayerData.STAGE - 1) / Utils.StageSize + 1 < _worldNum)
        {
            GetComponentInChildren<Image>().color -= new Color(0, 0, 0, 0.5f);
            _text.color -= new Color(0, 0, 0, 0.5f);
            _text.text = "未解禁";
        }
        else
        {
            _text.text = "WORLD : " + _worldNum;
        }
    }

    private IEnumerator DisplayStageSelect()
    {
        HomeManager.Mooving = true;
        yield return HomeManager.RemoveFrame(_worldFrame.gameObject);
        yield return HomeManager.MoveFrame(_stageFrame.gameObject);
        HomeManager.Mooving = false;
    }

    private IEnumerator ComingSoon()
    {
        _text.color = Color.white;
        for(int i = 0; i < 20; i++)
        {
            _text.color -= new Color(0, 0, 0, 0.05f);
            yield return new WaitForSeconds(0.02f);
        }
        _text.text = "～Coming Soon～";
        for (int i = 0; i < 20; i++)
        {
            _text.color += new Color(0, 0, 0, 0.05f);
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(3.0f);
        for (int i = 0; i < 20; i++)
        {
            _text.color -= new Color(0, 0, 0, 0.05f);
            yield return new WaitForSeconds(0.02f);
        }
        SetText();
        for (int i = 0; i < 20; i++)
        {
            _text.color += new Color(0, 0, 0, 0.05f);
            yield return new WaitForSeconds(0.02f);
        }
    }
}
