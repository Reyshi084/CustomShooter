using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionManager : MonoBehaviour
{
    [SerializeField] GameObject _filter;
    [SerializeField] GameObject _optionWindow;
    [SerializeField] GameObject _resetWindow;

    private GameObject _nowFilter;
    private Coroutine _resetCoroutine;

    private void Start()
    {
        _optionWindow.SetActive(false);
        _resetWindow.SetActive(false);
    }

    public void OnOptionButtonDown()
    {
        _nowFilter = Instantiate(_filter);
        _optionWindow.SetActive(true);
    }

    public void OnBackOptionButtonDown()
    {
        Destroy(_nowFilter);
        _optionWindow.SetActive(false);
    }

    public void OnPrivacyPolicyButtonDown()
    {
        Application.OpenURL("https://drive.google.com/file/d/1b8dGTcqZ_9ib9QyXmK8Y80TMqfwhkRGH/view");
    }

    public void OnResetButtonDown()
    {
        _resetWindow.SetActive(true);
    }

    public void OnBackResetButtonDown()
    {
        _resetWindow.SetActive(false);
    }

    public void OnResetButtonHold()
    {
        _resetCoroutine = StartCoroutine(ResetAll());
    }

    public void OnResetButtonUp()
    {
        if (_resetCoroutine != null)
        {
            StopCoroutine(_resetCoroutine);
        }
    }

    private IEnumerator ResetAll()
    {
        yield return new WaitForSeconds(5f);
        PlayerData.Reset();
        Debug.Log("Reset!!");
        Application.Quit();
    }
}
