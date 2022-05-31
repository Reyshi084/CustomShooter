using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeButtonManager : MonoBehaviour
{
    [SerializeField] HomeManager homeManager;

    public void OnSelectStageButtonDown()
    {
        StartCoroutine(homeManager.DisplayWorld());
    }

    public void OnCustomButtonDown()
    {
        StartCoroutine(homeManager.DisplayCustom());
    }

    public void OnExitButtonDown()
    {
        Application.Quit();
    }

    public void OnHomeButtonDown()
    {
        StartCoroutine(homeManager.Undo());
    }
}
