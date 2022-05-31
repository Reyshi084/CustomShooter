using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SelectSceneManager
{
    public static int SelectStageNum { set; get; } = 0;
    public static int PrevWorld { private set; get; } = 0;

    public enum State
    {
        Home,

        World,
        Stage,

        Custom,
        Awake,

        AbiCustom,
        EfeCustom,
    }

    public static State CurrentState { set; get; } = State.Awake;

    public static void SetPrevWorldS(int stageNum)
    {
        PrevWorld = (stageNum - 1) / Utils.StageSize + 1;
    }
    public static void SetPrevWorldW(int worldNum)
    {
        PrevWorld = worldNum;
    }
}
