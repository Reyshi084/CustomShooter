using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSelectManager : MonoBehaviour
{
    [SerializeField] StageSelectButtonCreate _stageSelectPrefab;
    [SerializeField] WorldSelectButtonManager _worldButtonPrefab;
    [SerializeField] GameObject _stageSelectLists;
    [SerializeField] GameObject _worldSelectFrame;

    private StageSelectButtonCreate[] _frames = new StageSelectButtonCreate[Utils.WorldSize];

    private const int OFFSET = -100;
    private const int WIDTH = 150;


    private void Awake()
    {
        // ステージフレームの生成
        for(int i = 0; i < Utils.WorldSize; i++)
        {
            _frames[i] = Instantiate(_stageSelectPrefab, _stageSelectLists.transform);
            _frames[i].Create(i + 1);
            HomeManager.AwayFrame(_frames[i].gameObject);

        }

        // ワールド選択ボタンの生成
        for(int i = 0; i < Utils.WorldSize; i++)
        {
            var button = Instantiate(_worldButtonPrefab, _worldSelectFrame.transform);
            button.transform.localPosition = new Vector3(0, OFFSET - i * WIDTH);
            button.SetInfo(i + 1, _frames[i], this);
        }

    }


    public void SetStageFrame(int worldNum)
    {
        HomeManager.SetFrame(_frames[worldNum - 1].gameObject);
    }

    public void AwayStageFrame(int worldNum)
    {
        HomeManager.AwayFrame(_frames[worldNum - 1].gameObject);
    }

    public IEnumerator MoveStageFrame(int worldNum)
    {
        yield return HomeManager.MoveFrame(_frames[worldNum - 1].gameObject);
    }

    public IEnumerator RemoveStageFrame(int worldNum)
    {
        yield return HomeManager.RemoveFrame(_frames[worldNum - 1].gameObject);
    }
}
