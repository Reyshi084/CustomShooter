using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    //変数名, // エフェクト名・かかる状態異常・イメージ色・効果
    // 追加する場合、PlayerDataの改変が必要
    public enum Effect
    {
        None, // なし・なし・白・なにも効果はない
        Poison, // 猛毒・嘔吐・黄緑・当たった場所にとどまり、触れている間ダメージを受け続ける
        Fire, // 火炎・燃焼・赤・当たった敵/味方に一定期間継続ダメージを与える
        Stream, // 水流・水没・青・当たった場所にとどまり、触れている間移動速度が遅くなる
        Lightning, // 雷撃・混乱・黄色・当たった敵/味方の移動方向を逆転させる
        Cold, // 冷気・凍結・水色・当たった敵/味方の行動を一定期間止める
        Hypnosis, // 催眠・眠気・桃・当たった敵/味方の攻撃力を一定割合低下させる
        Slash, // 斬撃・出血・紫・当たった敵/味方の残りHPの一定割合ダメージを継続して与える
        Chain, // 鉄鎖・封印・灰色・当たった敵/味方の攻撃を一定時間封印する
    }

    public enum EnemyType
    {
        Triangle, // 三角形
        Square, // 四角形
        Pentagon, // 五角形
        Hexagon, // 六角形
        Star, // 星型
    }

    public enum EnemyBulletType
    {
        Normal, // 通常弾
    }

    public enum EnemyMoveType
    {
        Stay, // その場にとどまる
        Slide_s, // 横移動(中心から左右1セル)

        Star, // スター型の行動
    }

    public enum PlayerBulletType
    {
        Normal,
        Counter,
        Guardian,
    }
 
    public enum AbilityType
    {
        None,
        Duration,  // freeze, outage
        Damage,  // fire, lightning
        Size,  // poison, stream
        Percent,  // slash, sleep
    }


    public static float BulletLowerLimit = -135f;
    public static float BulletUpperLimit = 190f;

    public static float EnemyLowerLimit = -110f;
    public static float EnemySideLimit = 150f;

    public static float StarAppearRate = 1f;

    public static float TickRate = 0.5f;

    public static int FieldSizeX = 11;
    public static int FieldLengthX = FieldSizeX * CellSize;

    public static int CellSize = 20;
    public static int EnemySizeStandard = 16;

    public static float PlayerPosY = -130f;

    public static float PlayerMoveSpeed = 10f;

    public static float MoveLimit = 110f;

    public static float EnemyPosY = 190f;
    public static float StarPosY = 170f;


    public static int EnemyTypeNum = System.Enum.GetValues(typeof(EnemyType)).Length;
    public static int EffectTypeNum = System.Enum.GetValues(typeof(Effect)).Length;
    public static int ItemTypeNum = EnemyTypeNum * EffectTypeNum;
    public static int ItemLvMax = 15;

    public static int NeedFragNum = 10;



    public static int WorldSize = 10;
    public static int StageSize = 10;

    public static int AdsInterval = 5;


    public static Color GetImageColor(Effect effect)
    {
        switch (effect)
        {
            case Utils.Effect.Cold:
                return Color.cyan;
            case Utils.Effect.Fire:
                return Color.red;
            case Utils.Effect.Hypnosis:
                return new Color(1, 0.5f, 1);
            case Utils.Effect.Lightning:
                return Color.yellow;
            case Utils.Effect.Poison:
                return Color.green;
            case Utils.Effect.Slash:
                return new Color(0.5f, 0, 0.5f);
            case Utils.Effect.Stream:
                return Color.blue;
            case Utils.Effect.Chain:
                return Color.gray;
            default:
                return Color.white;
        }
    }

    public static AbilityType GetAbilityType(Effect effect)
    {
        switch (effect)
        {
            case Effect.Cold:
            case Effect.Chain:
                return AbilityType.Duration;
            case Effect.Fire:
            case Effect.Lightning:
                return AbilityType.Damage;
            case Effect.Poison:
            case Effect.Stream:
                return AbilityType.Size;
            case Effect.Hypnosis:
            case Effect.Slash:
                return AbilityType.Percent;
            default:
                return AbilityType.None;

        }
    }
}
