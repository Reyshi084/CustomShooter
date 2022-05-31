using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeManager : MonoBehaviour
{
    [SerializeField] GameObject StageSelectFrame;
    [SerializeField] GameObject HomeSelectFrame;
    [SerializeField] GameObject CustomFrame;
    [SerializeField] GameObject AbiCustomFrame;
    [SerializeField] GameObject EfeCustomFrame;

    [SerializeField] WorldSelectManager WorldSelectMng;

    public static bool Mooving { set; get; } = false;


    void Start()
    {
        switch (SelectSceneManager.CurrentState)
        {
            case SelectSceneManager.State.Home:
                SetFrame(HomeSelectFrame);
                AwayFrame(StageSelectFrame);
                AwayFrame(CustomFrame);
                break;
            case SelectSceneManager.State.Stage:
                AwayFrame(StageSelectFrame);
                AwayFrame(HomeSelectFrame);
                AwayFrame(CustomFrame);

                WorldSelectMng.SetStageFrame(SelectSceneManager.PrevWorld);
                break;
            case SelectSceneManager.State.Custom:
                SetFrame(CustomFrame);
                AwayFrame(HomeSelectFrame);
                AwayFrame(StageSelectFrame);
                break;
            case SelectSceneManager.State.Awake:
                PlayerData.LoadPlayerData();
                SetFrame(HomeSelectFrame);
                AwayFrame(StageSelectFrame);
                AwayFrame(CustomFrame);
                break;
            case SelectSceneManager.State.World:
                SetFrame(StageSelectFrame);
                AwayFrame(HomeSelectFrame);
                AwayFrame(CustomFrame);
                break;
            default:
                SetFrame(HomeSelectFrame);
                AwayFrame(StageSelectFrame);
                AwayFrame(CustomFrame);
                break;
        }
        PlayerData.SavePlayerData();
    }

    public IEnumerator DisplayWorld()
    {
        if (!Mooving)
        {
            Mooving = true;
            SelectSceneManager.CurrentState = SelectSceneManager.State.World;
            yield return RemoveFrame(HomeSelectFrame);
            yield return MoveFrame(StageSelectFrame);
            Mooving = false;
        }
    }

    public IEnumerator DisplayCustom()
    {
        if (!Mooving)
        {
            Mooving = true;
            SelectSceneManager.CurrentState = SelectSceneManager.State.Custom;
            yield return RemoveFrame(HomeSelectFrame);
            yield return MoveFrame(CustomFrame);
            Mooving = false;
        }
    }

    public IEnumerator DisplayAbiCustomFrame()
    {
        if (!Mooving)
        {
            Mooving = true;
            SelectSceneManager.CurrentState = SelectSceneManager.State.AbiCustom;
            yield return RemoveFrame(CustomFrame);
            yield return MoveFrame(AbiCustomFrame);
            Mooving = false;
        }
    }

    public IEnumerator DisplayEfeCustomFrame()
    {
        if (!Mooving)
        {
            Mooving = true;
            SelectSceneManager.CurrentState = SelectSceneManager.State.EfeCustom;
            yield return RemoveFrame(CustomFrame);
            yield return MoveFrame(EfeCustomFrame);
            Mooving = false;
        }
    }

    public IEnumerator Undo()
    {
        if (Mooving)
        {
            yield break;
        }

        Mooving = true;

        switch (SelectSceneManager.CurrentState)
        {
            case SelectSceneManager.State.Custom:
                PlayerData.SavePlayerData();
                yield return RemoveFrame(CustomFrame);
                yield return MoveFrame(HomeSelectFrame);
                SelectSceneManager.CurrentState = SelectSceneManager.State.Home;
                break;
            case SelectSceneManager.State.Stage:
                yield return WorldSelectMng.RemoveStageFrame(SelectSceneManager.PrevWorld);
                yield return MoveFrame(StageSelectFrame);
                SelectSceneManager.CurrentState = SelectSceneManager.State.World;
                break;
            case SelectSceneManager.State.World:
                yield return RemoveFrame(StageSelectFrame);
                yield return MoveFrame(HomeSelectFrame);
                SelectSceneManager.CurrentState = SelectSceneManager.State.Home;
                break;

            case SelectSceneManager.State.AbiCustom:
                PlayerData.SavePlayerData();
                yield return RemoveFrame(AbiCustomFrame);
                yield return MoveFrame(CustomFrame);
                SelectSceneManager.CurrentState = SelectSceneManager.State.Custom;
                break;
            case SelectSceneManager.State.EfeCustom:
                PlayerData.SavePlayerData();
                yield return RemoveFrame(EfeCustomFrame);
                yield return MoveFrame(CustomFrame);
                SelectSceneManager.CurrentState = SelectSceneManager.State.Custom;
                break;
        }

        Mooving = false;
    }

    public static void SetFrame(GameObject gameObject)
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0);
    }

    public static void AwayFrame(GameObject gameObject)
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(1000, 0);
    }

    public static IEnumerator MoveFrame(GameObject gameObject)
    {
        for (int i = 20; i >= 0; i--)
        {
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(i * 50, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }

    public static IEnumerator RemoveFrame(GameObject gameObject)
    {
        for (int i = 0; i <= 20; i++)
        {
            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(i * 50, 0);
            yield return new WaitForSeconds(0.01f);
        }
    }

}
