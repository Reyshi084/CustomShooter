using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionManager : MonoBehaviour
{
    [SerializeField] GameObject _custom;
    [SerializeField] GameObject _abiCustom;
    [SerializeField] GameObject _efeCustom;
    [SerializeField] GameObject _filter;

    private GameObject _nowWindow;

    public void OnCustomHelpDown()
    {
        DisplayCustomDescription();
    }

    public void OnEfeHelpDown()
    {
        DisplayEfeCustomDescription();
    }

    public void OnAbiHelpDown()
    {
        DisplayAbiCustomDescription();
    }

    public void OnOkButtonDown()
    {
        _nowWindow.gameObject.SetActive(false);
        _filter.gameObject.SetActive(false);
    }

    private void DisplayCustomDescription()
    {
        _nowWindow = _custom;
        _nowWindow.gameObject.SetActive(true);
        _filter.gameObject.SetActive(true);
    }

    private void DisplayAbiCustomDescription()
    {
        _nowWindow = _abiCustom;
        _nowWindow.gameObject.SetActive(true);
        _filter.gameObject.SetActive(true);
    }

    private void DisplayEfeCustomDescription()
    {
        _nowWindow = _efeCustom;
        _nowWindow.gameObject.SetActive(true);
        _filter.gameObject.SetActive(true);
    }
}
