using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectButtonCreate : MonoBehaviour
{
    [SerializeField] private GameObject stageSelectButtonPrefab;
    [SerializeField] private GameObject stageSelectFrame;

    private int _offset = -100;
    private int _width = 150;

    public int WorldNum { set; get; }

    public void Create(int worldNum)
    {
        // GetComponent<RectTransform>().anchoredPosition = new Vector3(1000, 500, 0);

        WorldNum = worldNum;

        for(int i = 0; i < Utils.StageSize; i++)
        {
            var stageNum = (WorldNum - 1) * Utils.StageSize + (i + 1);

            var button = Instantiate(stageSelectButtonPrefab, stageSelectFrame.transform);
            button.transform.localPosition = new Vector3(0, _offset - i * _width);
            var ssbm = button.GetComponentInChildren<StageSelectButtonManager>();
            ssbm.StageNum = stageNum;

            var stageName = StageData.GetStageName(stageNum);
            var ssbmText = ssbm.transform.GetComponent<Text>();

            if (stageName.CompareTo("未解禁") == 0 || stageName.CompareTo("未実装") == 0)
            {
                ssbmText.color -= new Color(0, 0, 0, 0.5f);
                button.GetComponentInChildren<Image>().color -= new Color(0, 0, 0, 0.5f);
            }

            ssbmText.text = "-" + stageName + "-";
        }
    }




}
