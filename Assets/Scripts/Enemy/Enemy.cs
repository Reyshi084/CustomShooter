using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// EnemyDataからのみインスタンス生成されるクラス
// 敵のパラメータのみ扱い、インスタンス生成、スプライト参照はEnemyBehaviourで行う
public class Enemy
{
    public string EnemyName { protected set; get; } // 敵の名前
    public Utils.EnemyType EnemyType { protected set; get; } // スプライト識別名
    public int MAX_hp { protected set; get; } // 最大HP
    public int MAX_attack { protected set; get; } // 初期攻撃力
    public int MAX_defense { protected set; get; } // 初期防御力
    public float BulletSpeed { protected set; get; } // 弾丸の速度
    public float MoveSpeed { protected set; get; } // 移動速度
    public float RegularMoveSpeed { protected set; get; } // 縦方向の移動速度
    public Utils.Effect AddingEffect { protected set; get; } // 攻撃時の追加効果
    public float Duration { protected set; get; } // 追加効果の持続時間（すべての追加効果）
    public int Damage { protected set; get; } // 追加効果のダメージ量（火炎・水流・猛毒のみ使用）
    public float Percent { protected set; get; } // 追加効果の割合（斬撃・催眠のみ使用）
    public Utils.EnemyBulletType EBT { protected set; get; } // 弾道の種類
    public bool IsBoss { protected set; get; } // ボスかどうか
    public float BulletInterval { protected set; get; } // ボスの弾の間隔
    public Utils.EnemyMoveType EMT { protected set; get; } // 移動方法
    public int Size { protected set; get; } // 敵の大きさ(16x16を1とする)
    public int EXP { protected set; get; } // もらえる経験値量
    public float DropRate { protected set; get; } // ドロップ率（-1の場合はデフォルト）
    public int ItemLv { protected set; get; } // アイテムレベル



    // 全て決定できるメソッド
    public Enemy(string enemyName, Utils.EnemyType enemyType, int hp, int attack, int defense, float bulletSpeed, float moveSpeed, float regularMoveSpeed,
        Utils.Effect effect, float duration, int damage, float percent, Utils.EnemyBulletType enemyBulletType, bool isBoss, float bulletInterval, Utils.EnemyMoveType enemyMoveType,
        int size, int exp, float dropRate, int itemLv)
    {
        EnemyName = enemyName;
        EnemyType = enemyType;
        MAX_hp = hp;
        MAX_attack = attack;
        MAX_defense = defense;
        BulletSpeed = bulletSpeed;
        MoveSpeed = moveSpeed;
        RegularMoveSpeed = regularMoveSpeed;
        AddingEffect = effect;
        Duration = duration;
        Damage = damage;
        Percent = percent;
        EBT = enemyBulletType;
        IsBoss = isBoss;
        BulletInterval = bulletInterval;
        EMT = enemyMoveType;
        Size = size;
        EXP = exp;
        DropRate = dropRate;
        ItemLv = itemLv;
    }

    // 状態異常を持たず、通常弾を撃つ雑魚敵
    public Enemy(string enemyName, Utils.EnemyType enemyType, int hp, int attack, int defense, float bulletSpeed, float moveSpeed, float regularMoveSpeed, 
        int size, int exp, int itemLv)
    {
        EnemyName = enemyName;
        EnemyType = enemyType;
        MAX_hp = hp;
        MAX_attack = attack;
        MAX_defense = defense;
        BulletSpeed = bulletSpeed;
        MoveSpeed = moveSpeed;
        RegularMoveSpeed = regularMoveSpeed;
        AddingEffect = Utils.Effect.None;
        Duration = 0;
        Damage = 0;
        Percent = 0;
        EBT = Utils.EnemyBulletType.Normal;
        IsBoss = false;
        BulletInterval = 999f;
        EMT = Utils.EnemyMoveType.Stay;
        Size = size;
        EXP = exp;
        DropRate = -1;
        ItemLv = itemLv;
    }

    // 状態異常を持たず、通常弾を撃つ雑魚敵(ドロップ率変更可能)
    public Enemy(string enemyName, Utils.EnemyType enemyType, int hp, int attack, int defense, float bulletSpeed, float moveSpeed, float regularMoveSpeed,
        int size, int exp, float dropRate, int itemLv)
    {
        EnemyName = enemyName;
        EnemyType = enemyType;
        MAX_hp = hp;
        MAX_attack = attack;
        MAX_defense = defense;
        BulletSpeed = bulletSpeed;
        MoveSpeed = moveSpeed;
        RegularMoveSpeed = regularMoveSpeed;
        AddingEffect = Utils.Effect.None;
        Duration = 0;
        Damage = 0;
        Percent = 0;
        EBT = Utils.EnemyBulletType.Normal;
        IsBoss = false;
        BulletInterval = 999f;
        EMT = Utils.EnemyMoveType.Stay;
        Size = size;
        EXP = exp;
        DropRate = dropRate;
        ItemLv = itemLv;
    }

    // 状態異常を持たず、通常弾を撃つボス敵
    public Enemy(string enemyName, Utils.EnemyType enemyType, int hp, int attack, int defense, float bulletSpeed, float moveSpeed, float regularMoveSpeed,
        float bulletInterval, int size, int exp, float dropRate, int itemLv)
    {
        EnemyName = enemyName;
        EnemyType = enemyType;
        MAX_hp = hp;
        MAX_attack = attack;
        MAX_defense = defense;
        BulletSpeed = bulletSpeed;
        MoveSpeed = moveSpeed;
        RegularMoveSpeed = regularMoveSpeed;
        AddingEffect = Utils.Effect.None;
        Duration = 0;
        Damage = 0;
        Percent = 0;
        EBT = Utils.EnemyBulletType.Normal;
        IsBoss = true;
        BulletInterval = bulletInterval;
        EMT = Utils.EnemyMoveType.Stay;
        Size = size;
        EXP = exp;
        DropRate = dropRate;
        ItemLv = itemLv;
    }

    // 状態異常を持ち、通常弾を撃つ雑魚敵
    public Enemy(string enemyName, Utils.EnemyType enemyType, int hp, int attack, int defense, float bulletSpeed, float moveSpeed, float regularMoveSpeed,
        Utils.Effect effect, float duration, int damage, float percent, Utils.EnemyMoveType enemyMoveType, int size, int exp, int itemLv)
    {
        EnemyName = enemyName;
        EnemyType = enemyType;
        MAX_hp = hp;
        MAX_attack = attack;
        MAX_defense = defense;
        BulletSpeed = bulletSpeed;
        MoveSpeed = moveSpeed;
        RegularMoveSpeed = regularMoveSpeed;
        AddingEffect = effect;
        Duration = duration;
        Damage = damage;
        Percent = percent;
        EBT = Utils.EnemyBulletType.Normal;
        IsBoss = false;
        BulletInterval = 999f;
        EMT = enemyMoveType;
        Size = size;
        EXP = exp;
        DropRate = -1;
        ItemLv = itemLv;
    }

    // 状態異常を持ち、通常弾を撃つ雑魚敵(ドロップ率変更可能)
    public Enemy(string enemyName, Utils.EnemyType enemyType, int hp, int attack, int defense, float bulletSpeed, float moveSpeed, float regularMoveSpeed,
        Utils.Effect effect, float duration, int damage, float percent, Utils.EnemyMoveType enemyMoveType, int size, int exp, float dropRate, int itemLv)
    {
        EnemyName = enemyName;
        EnemyType = enemyType;
        MAX_hp = hp;
        MAX_attack = attack;
        MAX_defense = defense;
        BulletSpeed = bulletSpeed;
        MoveSpeed = moveSpeed;
        RegularMoveSpeed = regularMoveSpeed;
        AddingEffect = effect;
        Duration = duration;
        Damage = damage;
        Percent = percent;
        EBT = Utils.EnemyBulletType.Normal;
        IsBoss = false;
        BulletInterval = 999f;
        EMT = enemyMoveType;
        Size = size;
        EXP = exp;
        DropRate = dropRate;
        ItemLv = itemLv;
    }

    // 状態異常を持ち、通常弾を撃つボス敵
    public Enemy(string enemyName, Utils.EnemyType enemyType, int hp, int attack, int defense, float bulletSpeed, float moveSpeed, float regularMoveSpeed,
        Utils.Effect effect, float duration, int damage, float percent, float bulletInterval, 
        Utils.EnemyMoveType enemyMoveType, int size, int exp, float dropRate, int itemLv)
    {
        EnemyName = enemyName;
        EnemyType = enemyType;
        MAX_hp = hp;
        MAX_attack = attack;
        MAX_defense = defense;
        BulletSpeed = bulletSpeed;
        MoveSpeed = moveSpeed;
        RegularMoveSpeed = regularMoveSpeed;
        AddingEffect = effect;
        Duration = duration;
        Damage = damage;
        Percent = percent;
        EBT = Utils.EnemyBulletType.Normal;
        IsBoss = true;
        BulletInterval = bulletInterval;
        EMT = enemyMoveType;
        Size = size;
        EXP = exp;
        DropRate = dropRate;
        ItemLv = itemLv;
    }

    // 状態異常を持たず、移動できる雑魚敵
    public Enemy(string enemyName, Utils.EnemyType enemyType, int hp, int attack, int defense, float bulletSpeed, float moveSpeed, float regularMoveSpeed,
        Utils.EnemyMoveType enemyMoveType, int size, int exp, float dropRate, int itemLv)
    {
        EnemyName = enemyName;
        EnemyType = enemyType;
        MAX_hp = hp;
        MAX_attack = attack;
        MAX_defense = defense;
        BulletSpeed = bulletSpeed;
        MoveSpeed = moveSpeed;
        RegularMoveSpeed = regularMoveSpeed;
        AddingEffect = Utils.Effect.None;
        Duration = 0;
        Damage = 0;
        Percent = 0;
        EBT = Utils.EnemyBulletType.Normal;
        IsBoss = false;
        BulletInterval = 999f;
        EMT = enemyMoveType;
        Size = size;
        EXP = exp;
        DropRate = dropRate;
        ItemLv = itemLv;
    }

    // スター型の敵
    public Enemy(string enemyName, Utils.EnemyType enemyType, int hp, int attack, int defense, float moveSpeed,
        Utils.EnemyMoveType enemyMoveType, int size, int exp, float dropRate, int itemLv)
    {
        EnemyName = enemyName;
        EnemyType = enemyType;
        MAX_hp = hp;
        MAX_attack = attack;
        MAX_defense = defense;
        BulletSpeed = 1f;
        MoveSpeed = moveSpeed;
        RegularMoveSpeed = 0f;
        AddingEffect = Utils.Effect.None;
        Duration = 0;
        Damage = 0;
        Percent = 0;
        EBT = Utils.EnemyBulletType.Normal;
        IsBoss = true;
        BulletInterval = 999f;
        EMT = enemyMoveType;
        Size = size;
        EXP = exp;
        DropRate = dropRate;
        ItemLv = itemLv;
    }

    // スター型の敵（追加効果あり）
    public Enemy(string enemyName, Utils.EnemyType enemyType, int hp, int attack, int defense, float moveSpeed, Utils.Effect effect,
        Utils.EnemyMoveType enemyMoveType, int size, int exp, float dropRate, int itemLv)
    {
        EnemyName = enemyName;
        EnemyType = enemyType;
        MAX_hp = hp;
        MAX_attack = attack;
        MAX_defense = defense;
        BulletSpeed = 1f;
        MoveSpeed = moveSpeed;
        RegularMoveSpeed = 0f;
        AddingEffect = effect;
        Duration = 0;
        Damage = 0;
        Percent = 0;
        EBT = Utils.EnemyBulletType.Normal;
        IsBoss = true;
        BulletInterval = 999f;
        EMT = enemyMoveType;
        Size = size;
        EXP = exp;
        DropRate = dropRate;
        ItemLv = itemLv;
    }

    // tri -> squ -> pnt -> hex -> str -> tir.poison -> ...
    public static int GetItemNum(Utils.EnemyType enemyType, Utils.Effect effect)
    {

        int etNum = (int)enemyType;
        int efeNum = (int)effect;

        return etNum + (efeNum * Utils.EnemyTypeNum);
    }

    public static string GetItemName(int itemNum, int itemLv)
    {
        string name = "";

        int etNum = itemNum % Utils.EnemyTypeNum;
        int efeNum = itemNum / Utils.EnemyTypeNum;

        switch (etNum)
        {
            case 0:
                name += "三角形の欠片";
                break;
            case 1:
                name += "四角形の欠片";
                break;
            case 2:
                name += "五角形の欠片";
                break;
            case 3:
                name += "六角形の欠片";
                break;
            case 4:
                name += "綺羅星の欠片";
                break;
        }

        switch (efeNum)
        {
            case 0:
                name += "【白色】";
                break;
            case 1:
                name += "【緑色】";
                break;
            case 2:
                name += "【赤色】";
                break;
            case 3:
                name += "【青色】";
                break;
            case 4:
                name += "【黄色】";
                break;
            case 5:
                name += "【水色】";
                break;
            case 6:
                name += "【桃色】";
                break;
            case 7:
                name += "【紫色】";
                break;
            case 8:
                name += "【灰色】";
                break;
        }

        name += "LV." + itemLv;


        return name;
    }

}
