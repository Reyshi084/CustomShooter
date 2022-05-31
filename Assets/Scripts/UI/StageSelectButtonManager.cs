using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectButtonManager : MonoBehaviour
{
    public int StageNum { set; get; } = 0;


    public void OnButtonDown()
    {
        if (StageData.GetStageName(StageNum).Equals("未実装"))
        {
            return;
        }

        if (StageNum <= PlayerData.STAGE)
        {
            PlayerData.ConvertEnergyToParam();
            SelectSceneManager.SelectStageNum = StageNum;
            SelectSceneManager.SetPrevWorldS(StageNum);
            SceneManager.LoadScene("StageScene");
        }
    }


}
