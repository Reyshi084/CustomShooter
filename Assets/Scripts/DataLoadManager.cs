using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataLoadManager : MonoBehaviour
{
    private void Start()
    {
        PlayerData.LoadPlayerData();
        SelectSceneManager.CurrentState = SelectSceneManager.State.Home;
        SceneManager.LoadScene("SelectScene");
    }
}
