using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOptionButtonManager : MonoBehaviour
{
    [SerializeField] GameObject _poseWindow;
    [SerializeField] GameObject _filter;

    public void OnResumeButtonDown()
    {
        HideWindow();
    }

    public void OnPoseButtonDown()
    {
        StartCoroutine(DisplayWindow());
    }

    private void HideWindow()
    {
        _poseWindow.transform.localScale = new Vector3(0, 0);
        _filter.SetActive(false);
        _poseWindow.SetActive(false);
        Time.timeScale = 1;
    }

    private IEnumerator DisplayWindow()
    {
        Time.timeScale = 0;
        _filter.SetActive(true);
        _poseWindow.SetActive(true);

        for(int i = 0; i < 20; i++)
        {
            _poseWindow.transform.localScale += new Vector3(0.025f, 0.025f);
            yield return new WaitForSecondsRealtime(0.01f);
        }
    }
}
