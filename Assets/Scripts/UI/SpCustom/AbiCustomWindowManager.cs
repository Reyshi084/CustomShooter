using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Buttonにつける
public class AbiCustomWindowManager : MonoBehaviour
{
    [SerializeField] HomeManager _homeManager;

    [SerializeField] GameObject _abiCustomWindow;

    [SerializeField] Image _frame;
    [SerializeField] Text _frameText;

    private bool _active;

    private void Start()
    {
        HomeManager.AwayFrame(_abiCustomWindow);

        // stage5クリアで解放
        if(PlayerData.STAGE < 6)
        {
            _active = false;

            _frame.color = new Color(1, 1, 1, 0.5f);
            _frameText.color = new Color(1, 1, 1, 0.5f);
            _frameText.text = "未解禁";
        }
        else
        {
            _active = true;
        }

    }

    public void OnAbiCustomButtonDown()
    {
        if (_active)
        {
            StartCoroutine(_homeManager.DisplayAbiCustomFrame());    
        }
    }

}
