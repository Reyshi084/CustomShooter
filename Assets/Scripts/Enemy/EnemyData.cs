using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ステージごとの敵ステータス情報を静的クラスとして持つ
// 名前, 識別名, HP, 攻撃力, 防御力, 弾速, 移動速度, エフェクト, 効果時間, ダメージ, 割合, 弾道タイプ, 移動タイプ
public static class EnemyData
{
    private static readonly List<Enemy[]> _enemiesData = new List<Enemy[]>
    {
        /*
        new Enemy("name", Utils.EnemyType.xxx, hp, atk, dfe, bspd, mspd, rspd, 
            Utils.Effect.xxx, dur, dmg, pcnt, Utils.EnemyBulletType.xxx, Utils.EnemyMoveType.xxx, size, exp), 
        */

        new Enemy[] // デバッグ用サンプルステージ
        {
            new Enemy("トライアングル(S)", Utils.EnemyType.Triangle, 1, 1, 0, 10.0f, 5f, 1f, Utils.Effect.Fire, 1f, 1, 0.5f, Utils.EnemyMoveType.Stay, 1, 0, 1),
            new Enemy("スクエア(M)", Utils.EnemyType.Square, 30, 1, 0, 8.0f, 10.0f, 1f, 1, 0, 2)
        },
        new Enemy[] // 1-1
        {
            new Enemy("トライアングル(S)", Utils.EnemyType.Triangle, 1, 2, 0, 10.0f, 0f, 1f,
                1, 1, 1),
            new Enemy("トライアングル(M)", Utils.EnemyType.Triangle, 3, 5, 0, 15.0f, 0f, 1f, 5.0f,
                2, 3, 1, 1),
        },
        new Enemy[] // 1-2
        {
            new Enemy("トライアングル(S)", Utils.EnemyType.Triangle, 2, 3, 0, 10.0f, 0f, 1f,
                1, 2, 1),
            new Enemy("トライアングル(M)", Utils.EnemyType.Triangle, 5, 5, 0, 15.0f, 0f, 1f, 5.0f,
                2, 4, 1, 1),
        },
        new Enemy[] // 1-3
        {
            new Enemy("トライアングル(S)", Utils.EnemyType.Triangle, 3, 4, 0, 10.0f, 0f, 1f,
                1, 2, 1),
            new Enemy("トライアングル(M)", Utils.EnemyType.Triangle, 10, 5, 0, 15.0f, 0f, 1f, 5.0f,
                2, 6, 0.5f, 2),
        },
        new Enemy[] // 1-4
        {
            new Enemy("トライアングル(M)", Utils.EnemyType.Triangle, 20, 6, 0, 15.0f, 0f, 1f,
                2, 12, 0.3f, 2),
            new Enemy("トライアングル(L)", Utils.EnemyType.Triangle, 30, 8, 0, 20.0f, 0f, 1f, 5.0f,
                3, 15, 0.8f, 2),
        },
        new Enemy[] // 1-5
        {
            new Enemy("トライアングル(S)", Utils.EnemyType.Triangle, 6, 5, 0, 10.0f, 0f, 1f,
                1, 5, 1),
            new Enemy("トライアングル(L)", Utils.EnemyType.Triangle, 40, 9, 0, 20.0f, 0f, 1f, 3.0f,
                3, 20, 1f, 2),
        },

        new Enemy[] // 1-6
        {
            new Enemy("トライアングル(S)", Utils.EnemyType.Triangle, 10, 4, 0, 12.0f, 0f, 1f,
                1, 8, 2),
            new Enemy("スクエア(S)", Utils.EnemyType.Square, 5, 1, 5, 12.0f, 0f, 1f,
                1, 15, 0.2f, 1),
        },
        new Enemy[] // 1-7
        {
            new Enemy("トライアングル(S)", Utils.EnemyType.Triangle, 12, 5, 0, 12.0f, 0f, 1f,
                1, 8, 2),
            new Enemy("スクエア(S)", Utils.EnemyType.Square, 5, 2, 6, 12.0f, 0f, 1f,
                1, 17, 0.2f, 1),
        },
        new Enemy[] // 1-8
        {
            new Enemy("スクエア(M)", Utils.EnemyType.Square, 10, 4, 8, 12.0f, 0f, 1f,
                2, 30, 2),
        },
        new Enemy[] // 1-9
        {
            new Enemy("スクエア(S)", Utils.EnemyType.Square, 10, 3, 8, 12.0f, 0f, 1f,
                1, 10, 1),
            new Enemy("スクエア(L)", Utils.EnemyType.Square, 15, 8, 10, 12.0f, 0f, 1f, 5.0f,
                3, 15, 0.8f, 2),
        },
        new Enemy[] // 1-10
        {
            new Enemy("スクエア(S)", Utils.EnemyType.Square, 10, 5, 7, 12.0f, 0f, 1f,
                1, 20, 1),
            new Enemy("スクエア(M)", Utils.EnemyType.Square, 16, 7, 9, 12.0f, 0f, 1f, 5.0f,
                2, 30, 0.5f, 2),
            new Enemy("スクエア(L)", Utils.EnemyType.Square, 24, 9, 11, 12.0f, 0f, 1f, 4.0f,
                3, 40, 1f, 2),
        },

        new Enemy[] // 2-1
        {
            new Enemy("トライアングル(S)", Utils.EnemyType.Triangle, 24, 3, 0, 5f, 5f, 1f, Utils.Effect.Poison, 5f, 2, 0, 5.0f, Utils.EnemyMoveType.Stay,
                1, 45, 0.4f, 1),
            new Enemy("スクエア(S)", Utils.EnemyType.Square, 11, 1, 8, 12.0f, 0, 1f,
                1, 40, 2),
        },
        new Enemy[] // 2-2
        {
            new Enemy("トライアングル(S)", Utils.EnemyType.Triangle, 28, 3, 0, 15f, 0, 1f, Utils.Effect.Fire, 2f, 1, 0, 5.0f, Utils.EnemyMoveType.Stay,
                1, 28, 0.1f, 1),
            new Enemy("スクエア(S)", Utils.EnemyType.Square, 11, 1, 9, 12.0f, 0, 1f,
                1, 26, 2),
        },
        new Enemy[] // 2-3
        {
            new Enemy("トライアングル(S)", Utils.EnemyType.Triangle, 20, 3, 0, 5f, 5f, 1f, Utils.Effect.Stream, 7f, 0, 0.5f, Utils.EnemyMoveType.Stay,
                1, 33, 0.12f, 1),
            new Enemy("スクエア(S)", Utils.EnemyType.Square, 10, 1, 7, 12.0f, 0, 1f,
                1, 20, 2),
        },
        new Enemy[] // 2-4
        {
            new Enemy("トライアングル(S)", Utils.EnemyType.Triangle, 35, 4, 0, 25f, 0f, 1f, Utils.Effect.Lightning, 5f, 0, 0, 5.0f, Utils.EnemyMoveType.Stay,
                1, 45, 0.15f, 1),
            new Enemy("スクエア(S)", Utils.EnemyType.Square, 12, 1, 15, 12.0f, 0, 1f,
                1, 25, 2),
        },
        new Enemy[] // 2-5
        {
            new Enemy("トライアングル(S)", Utils.EnemyType.Triangle, 42, 5, 0, 7f, 0f, 1f, Utils.Effect.Poison, 5f, 2, 0, 7.0f, Utils.EnemyMoveType.Stay,
                1, 100, 0.3f, 1),
            new Enemy("トライアングル(S)", Utils.EnemyType.Triangle, 42, 5, 0, 12f, 0f, 1f, Utils.Effect.Fire, 2f, 1, 0, 7.0f, Utils.EnemyMoveType.Stay,
                1, 100, 0.3f, 1),
            new Enemy("トライアングル(S)", Utils.EnemyType.Triangle, 42, 5, 0, 7f, 0f, 1f, Utils.Effect.Stream, 7f, 0, 0.5f, 7.0f, Utils.EnemyMoveType.Stay,
                1, 100, 0.3f, 1),
            new Enemy("トライアングル(S)", Utils.EnemyType.Triangle, 42, 7, 0, 25f, 0f, 1f, Utils.Effect.Lightning, 5f, 0, 0, 7.0f, Utils.EnemyMoveType.Stay,
                1, 100, 0.3f, 1),
        },

        new Enemy[] // 2-6
        {
            new Enemy("トライアングル(S)", Utils.EnemyType.Triangle, 50, 5, 0, 18f, 0f, 1f, Utils.Effect.Cold, 1f, 0, 0, 7.0f, Utils.EnemyMoveType.Stay,
                1, 50, 0.07f, 1),
        },

        new Enemy[] // 2-7
        {
            new Enemy("トライアングル(S)", Utils.EnemyType.Triangle, 60, 3, 0, 18f, 5f, 1f, Utils.Effect.Hypnosis, 4f, 0, 0.5f, 6.0f, Utils.EnemyMoveType.Stay,
                1, 80, 0.2f, 1),
            new Enemy("スクエア(L)", Utils.EnemyType.Square, 20, 5, 25, 12f, 0, 1f,
                3, 195, 2),
        },

        new Enemy[] // 2-8
        {
            new Enemy("トライアングル(S)", Utils.EnemyType.Triangle, 100, 15, 0, 25f, 0, 1f, Utils.Effect.Slash, 0f, 4, 0.7f, 5.0f, Utils.EnemyMoveType.Stay,
                1, 85, 0.15f, 1),
            new Enemy("スクエア(S)", Utils.EnemyType.Square, 20, 5, 15, 5f, 5f, 1f,
                1, 70, 0.2f, 2),
        },

        new Enemy[] // 2-9
        {
            new Enemy("トライアングル(S)", Utils.EnemyType.Triangle, 75, 9, 0, 25f, 0, 1f, Utils.Effect.Chain, 3f, 0, 0, 5.0f, Utils.EnemyMoveType.Stay,
                1, 80, 0.15f, 1),
        },

        new Enemy[] // 2-10
        {
            new Enemy("トライアングル(S)", Utils.EnemyType.Triangle, 81, 7, 0, 15f, 0f, 1f, Utils.Effect.Cold, 1f, 0, 0, 7.0f, Utils.EnemyMoveType.Stay,
                1, 150, 0.3f, 1),
            new Enemy("トライアングル(S)", Utils.EnemyType.Triangle, 81, 7, 0, 20f, 0f, 1f, Utils.Effect.Hypnosis, 8f, 0, 0.3f, 7.0f, Utils.EnemyMoveType.Stay,
                1, 150, 0.3f, 1),
            new Enemy("トライアングル(S)", Utils.EnemyType.Triangle, 81, 1, 0, 25f, 0f, 1f, Utils.Effect.Slash, 7f, 4, 0.5f, 7.0f, Utils.EnemyMoveType.Stay,
                1, 150, 0.3f, 1),
            new Enemy("トライアングル(S)", Utils.EnemyType.Triangle, 81, 9, 0, 20f, 0f, 1f, Utils.Effect.Chain, 3f, 0, 0, 7.0f, Utils.EnemyMoveType.Stay,
                1, 150, 0.3f, 1),
        },

        new Enemy[] // 3-1
        {
            new Enemy("トライアングル(S)", Utils.EnemyType.Triangle, 96, 10, 0, 15f, 7f, 1f, Utils.EnemyMoveType.Slide_s,
                1, 180, 0.15f, 2),
            new Enemy("スクエア(S)", Utils.EnemyType.Square, 35, 5, 25, 10f, 5f, 1f,
                1, 150, 0.15f, 2),
        },

        new Enemy[] // 3-2
        {
            new Enemy("トライアングル(S)", Utils.EnemyType.Triangle, 82, 6, 0, 5f, 5f, 1f, Utils.Effect.Poison, 5f, 2, 0, 12.0f, Utils.EnemyMoveType.Slide_s,
                1, 200, 0.2f, 2),
            new Enemy("トライアングル(S)", Utils.EnemyType.Triangle, 82, 6, 0, 5f, 5f, 1f, Utils.Effect.Stream, 5f, 0, 0.2f, 12.0f, Utils.EnemyMoveType.Slide_s,
                1, 200, 0.2f, 2),
        },

        new Enemy[] // 3-3
        {
            new Enemy("トライアングル(M)", Utils.EnemyType.Triangle, 220, 7, 0, 25f, 10f, 1f, Utils.Effect.Fire, 3f, 2, 0, 3.0f, Utils.EnemyMoveType.Slide_s,
                2, 400, 0.5f, 2),
            new Enemy("スクエア(M)", Utils.EnemyType.Square, 30, 10, 40, 15f, 10, 1f, Utils.Effect.Cold, 1f, 0, 0, 3.0f, Utils.EnemyMoveType.Slide_s,
                2, 400, 0.5f, 2),
        },

        new Enemy[] // 3-4
        {
            new Enemy("トライアングル(S)", Utils.EnemyType.Triangle, 120, 10, 0, 12f, 10f, 1f, Utils.Effect.Stream, 7f, 0, 0.2f, Utils.EnemyMoveType.Slide_s,
                1, 130, 0.25f, 2),
            new Enemy("トライアングル(S)", Utils.EnemyType.Triangle, 120, 12, 0, 17f, 10f, 1f, Utils.Effect.Lightning, 7f, 0, 0, Utils.EnemyMoveType.Slide_s,
                1, 132, 0.15f, 2),
        },

        new Enemy[] // 3-5
        {
            new Enemy("スクエア(S)", Utils.EnemyType.Square, 35, 10, 40, 15f, 10f, 1f, Utils.Effect.Chain, 5f, 0, 0f, 5.0f, Utils.EnemyMoveType.Slide_s,
                1, 130, 0.2f, 2),
            new Enemy("トライアングル(S)", Utils.EnemyType.Triangle, 150, 12, 0, 15f, 10f, 1f, Utils.Effect.Cold, 1.5f, 0, 0, 5.0f, Utils.EnemyMoveType.Slide_s,
                1, 132, 0.2f, 2),
        },

        new Enemy[] // 3-6
        {
            new Enemy("ペンタゴン(S)", Utils.EnemyType.Pentagon, 300, 15, 20, 12.0f, 0f, 1f,
                1, 400, 0.3f, 1),
        },

        new Enemy[] // 3-7
        {
            new Enemy("ペンタゴン(S)", Utils.EnemyType.Pentagon, 300, 15, 20, 18.0f, 7f, 1f, Utils.EnemyMoveType.Slide_s,
                1, 400, 0.3f, 1),
            new Enemy("スクエア(S)", Utils.EnemyType.Square, 80, 8, 30, 25f, 0, 1f, Utils.Effect.Slash, 0f, 5, 0.6f, Utils.EnemyMoveType.Stay,
                1, 250, 0.35f, 2),
            new Enemy("スクエア(S)", Utils.EnemyType.Square, 80, 8, 30, 20f, 0, 1f, Utils.Effect.Hypnosis, 3f, 0, 0.4f, Utils.EnemyMoveType.Stay,
                1, 250, 0.35f, 2),
        },

        new Enemy[] // 3-8
        {
            new Enemy("ペンタゴン(S)", Utils.EnemyType.Pentagon, 300, 15, 15, 18.0f, 0f, 1f, Utils.Effect.Poison, 10.0f, 2, 0, 9.0f, Utils.EnemyMoveType.Stay,
                1, 300, 0.3f, 1),
            new Enemy("ペンタゴン(S)", Utils.EnemyType.Pentagon, 300, 15, 15, 18.0f, 0f, 1f, Utils.Effect.Stream, 10.0f, 0, 0.5f, 9.0f, Utils.EnemyMoveType.Stay,
                1, 300, 0.3f, 1),
            new Enemy("ペンタゴン(S)", Utils.EnemyType.Pentagon, 300, 15, 15, 18.0f, 0f, 1f, Utils.Effect.Chain, 3.0f, 0, 0, 9.0f, Utils.EnemyMoveType.Stay,
                1, 300, 0.3f, 1),
            new Enemy("スクエア(S)", Utils.EnemyType.Square, 100, 10, 30, 10f, 0, 1f,
                1, 200, 0.2f, 2),
        },

        new Enemy[] // 3-9
        {
            new Enemy("ペンタゴン(M)", Utils.EnemyType.Pentagon, 600, 18, 30, 25.0f, 7f, 1f, Utils.Effect.Lightning, 5f, 0, 0, 4.0f, Utils.EnemyMoveType.Slide_s,
                2, 600, 0.3f, 2),
        },

        new Enemy[] // 3-10
        {
            new Enemy("ペンタゴン(L)", Utils.EnemyType.Pentagon, 1500, 20, 30, 25.0f, 0f, 1f, 2f,
                3, 1500, 0.4f, 2),
        },

        new Enemy[] // 4-1
        {
            new Enemy("トライアングル(L)", Utils.EnemyType.Triangle, 500, 15, 0, 20.0f, 0f, 2.2f, Utils.Effect.Cold, 1f, 0, 0, Utils.EnemyMoveType.Stay,
                3, 8, 0.4f, 3),
            new Enemy("トライアングル(L)", Utils.EnemyType.Triangle, 500, 15, 0, 20.0f, 0f, 2.2f, Utils.Effect.Stream, 99f, 0, 0.5f, Utils.EnemyMoveType.Stay,
                3, 8, 0.4f, 3),
        },

        new Enemy[] // 4-2
        {
            new Enemy("スクエア(L)", Utils.EnemyType.Square, 300, 10, 9999, 20f, 0, 1f, Utils.Effect.Fire, 5f, 1, 0, Utils.EnemyMoveType.Stay,
                3, 200, 0.3f, 3),
        },

        new Enemy[] // 4-3
        {
            new Enemy("ペンタゴン(S)", Utils.EnemyType.Pentagon, 1000, 7, 30, 30f, 0, 1f, Utils.Effect.Chain, 1f, 0, 0, 0.2f, Utils.EnemyMoveType.Stay,
                1, 200, 0.4f, 3),
        },

        new Enemy[] // 4-4
        {
            new Enemy("スクエア(S)", Utils.EnemyType.Square, 50, 10, 30, 20f, 0, 1f, Utils.Effect.Lightning, 5f, 0, 0, 10f, Utils.EnemyMoveType.Stay,
                1, 200, 0.2f, 3),
            new Enemy("ペンタゴン(L)", Utils.EnemyType.Pentagon, 8191, 15, 0, 20.0f, 10f, 1f, Utils.Effect.Slash, 0, 0, 0.7f, 3.0f, Utils.EnemyMoveType.Slide_s,
                3, 8, 1f, 3),
        },

        new Enemy[] // 4-5
        {
            new Enemy("スクエア(S)", Utils.EnemyType.Square, 150, 10, 40, 5f, 0, 1f, Utils.Effect.Hypnosis, 5f, 0, 0.5f, 20f, Utils.EnemyMoveType.Stay,
                1, 200, 0.2f, 3),
            new Enemy("ペンタゴン(S)", Utils.EnemyType.Pentagon, 200, 7, 30, 15f, 0, 1f, 9.0f,
                1, 200, 0.4f, 3),
            new Enemy("トライアングル(S)", Utils.EnemyType.Triangle, 100, 15, 0, 25.0f, 0f, 1f, Utils.Effect.Poison, 3f, 5, 0.8f, 0.5f, Utils.EnemyMoveType.Stay,
                1, 8, 0.4f, 3),
        },

        new Enemy[] // 4-6
        {
            new Enemy("ヘキサゴン(S)", Utils.EnemyType.Hexagon, 500, 15, 40, 35f, 0, 1f,
                1, 200, 0.2f, 1),
            new Enemy("スクエア(S)", Utils.EnemyType.Square, 100, 12, 50, 10.0f, 20f, 1f, Utils.Effect.Lightning, 7f, 0, 0, 5.0f, Utils.EnemyMoveType.Stay,
                1, 8, 0.2f, 3),
        },

        new Enemy[] // 4-7
        {
            new Enemy("ヘキサゴン(S)", Utils.EnemyType.Hexagon, 650, 20, 50, 30f, 0, 1.8f, Utils.Effect.Hypnosis, 3f, 0, 0.4f, 12.0f, Utils.EnemyMoveType.Stay,
                1, 200, 0.2f, 1),
            new Enemy("トライアングル(M)", Utils.EnemyType.Triangle, 200, 15, 0, 15.0f, 0f, 1f,
                2, 8, 0.4f, 3),
        },

        new Enemy[] // 4-8
        {
            new Enemy("ヘキサゴン(M)", Utils.EnemyType.Hexagon, 500, 20, 50, 35f, 0, 1f, Utils.Effect.Fire, 4f, 2, 0, 10.0f, Utils.EnemyMoveType.Stay,
                2, 200, 0.4f, 2),
            new Enemy("スクエア(M)", Utils.EnemyType.Square, 200, 20, 9999, 15.0f, 10f, 1f, Utils.Effect.Poison, 5f, 3, 0, 10.0f, Utils.EnemyMoveType.Slide_s,
                2, 8, 0.4f, 3),
        },

        new Enemy[] // 4-9
        {
            new Enemy("ヘキサゴン(S)", Utils.EnemyType.Hexagon, 220, 20, 40, 30f, 0, 1f, Utils.Effect.Chain, 2f, 0, 0, 10.0f, Utils.EnemyMoveType.Stay,
                1, 200, 0.1f, 1),
        },

        new Enemy[] // 4-10
        {
            new Enemy("ヘキサゴン(M)", Utils.EnemyType.Hexagon, 1000, 20, 60, 40f, 0, 1f, 3.0f,
                2, 200, 0.5f, 2),
            new Enemy("ヘキサゴン(L)", Utils.EnemyType.Hexagon, 2000, 30, 60, 45f, 0, 1f, 3.0f,
                3, 200, 1f, 2),
        },

    };

    private static readonly List<Enemy> _starData = new List<Enemy>
    {
        // サンプル用データ
        new Enemy("スター(S)", Utils.EnemyType.Star, 1, 1, 9999, 5f, Utils.EnemyMoveType.Star,
                1, 10, 0.5f, 1),

        //w1
        new Enemy("スター(S)", Utils.EnemyType.Star, 1, 1, 9999, 2f, Utils.EnemyMoveType.Star,
                1, 5, 1f, 1),
        new Enemy("スター(S)", Utils.EnemyType.Star, 1, 1, 9999, 2f, Utils.EnemyMoveType.Star,
                1, 15, 1f, 1),
        new Enemy("スター(S)", Utils.EnemyType.Star, 1, 1, 9999, 2f, Utils.EnemyMoveType.Star,
                1, 25, 1f, 1),
        new Enemy("スター(S)", Utils.EnemyType.Star, 1, 1, 9999, 2f, Utils.EnemyMoveType.Star,
                1, 50, 1f, 1),
        new Enemy("スター(S)", Utils.EnemyType.Star, 1, 1, 9999, 2f, Utils.EnemyMoveType.Star,
                1, 100, 1f, 1),

        new Enemy("スター(S)", Utils.EnemyType.Star, 1, 1, 9999, 2f, Utils.EnemyMoveType.Star,
                1, 150, 1f, 1),
        new Enemy("スター(S)", Utils.EnemyType.Star, 1, 1, 9999, 2f, Utils.EnemyMoveType.Star,
                1, 200, 1f, 1),
        new Enemy("スター(S)", Utils.EnemyType.Star, 1, 1, 9999, 2f, Utils.EnemyMoveType.Star,
                1, 250, 1f, 1),
        new Enemy("スター(S)", Utils.EnemyType.Star, 1, 1, 9999, 2f, Utils.EnemyMoveType.Star,
                1, 300, 1f, 1),
        new Enemy("スター(S)", Utils.EnemyType.Star, 1, 1, 9999, 2f, Utils.EnemyMoveType.Star,
                1, 400, 1f, 1),


        //w2
        new Enemy("スター(S)", Utils.EnemyType.Star, 1, 1, 9999, 2.5f, Utils.Effect.Poison, Utils.EnemyMoveType.Star,
                1, 450, 1f, 2),
        new Enemy("スター(S)", Utils.EnemyType.Star, 1, 1, 9999, 2.5f, Utils.Effect.Fire, Utils.EnemyMoveType.Star,
                1, 500, 1f, 2),
        new Enemy("スター(S)", Utils.EnemyType.Star, 1, 1, 9999, 2.5f, Utils.Effect.Stream, Utils.EnemyMoveType.Star,
                1, 600, 1f, 2),
        new Enemy("スター(S)", Utils.EnemyType.Star, 1, 1, 9999, 2.5f, Utils.Effect.Lightning, Utils.EnemyMoveType.Star,
                1, 700, 1f, 2),
        new Enemy("スター(S)", Utils.EnemyType.Star, 1, 1, 9999, 2.5f, Utils.EnemyMoveType.Star,
                1, 900, 1f, 2),

        new Enemy("スター(S)", Utils.EnemyType.Star, 1, 1, 9999, 2.5f, Utils.Effect.Cold, Utils.EnemyMoveType.Star,
                1, 1100, 1f, 2),
        new Enemy("スター(S)", Utils.EnemyType.Star, 1, 1, 9999, 2.5f, Utils.Effect.Hypnosis, Utils.EnemyMoveType.Star,
                1, 1300, 1f, 2),
        new Enemy("スター(S)", Utils.EnemyType.Star, 1, 1, 9999, 2.5f, Utils.Effect.Slash, Utils.EnemyMoveType.Star,
                1, 1500, 1f, 2),
        new Enemy("スター(S)", Utils.EnemyType.Star, 1, 1, 9999, 2.5f, Utils.Effect.Chain, Utils.EnemyMoveType.Star,
                1, 1800, 1f, 2),
        new Enemy("スター(S)", Utils.EnemyType.Star, 1, 1, 9999, 2.5f, Utils.EnemyMoveType.Star,
                1, 2000, 1f, 2),

        //w3
        new Enemy("スター(S)", Utils.EnemyType.Star, 1, 1, 9999, 3f, Utils.EnemyMoveType.Star,
                1, 2500, 1f, 3),
        new Enemy("スター(S)", Utils.EnemyType.Star, 1, 1, 9999, 3f, Utils.Effect.Poison, Utils.EnemyMoveType.Star,
                1, 2700, 1f, 3),
        new Enemy("スター(S)", Utils.EnemyType.Star, 1, 1, 9999, 3f, Utils.Effect.Fire, Utils.EnemyMoveType.Star,
                1, 2900, 1f, 3),
        new Enemy("スター(S)", Utils.EnemyType.Star, 1, 1, 9999, 3f, Utils.Effect.Lightning, Utils.EnemyMoveType.Star,
                1, 3100, 1f, 3),
        new Enemy("スター(S)", Utils.EnemyType.Star, 1, 1, 9999, 3f, Utils.Effect.Chain, Utils.EnemyMoveType.Star,
                1, 3300, 1f, 3),

        new Enemy("スター(S)", Utils.EnemyType.Star, 1, 1, 9999, 3f, Utils.EnemyMoveType.Star,
                1, 3500, 1f, 3),
        new Enemy("スター(S)", Utils.EnemyType.Star, 1, 1, 9999, 3f, Utils.Effect.Hypnosis, Utils.EnemyMoveType.Star,
                1, 3700, 1f, 3),
        new Enemy("スター(S)", Utils.EnemyType.Star, 1, 1, 9999, 3f, Utils.Effect.Stream, Utils.EnemyMoveType.Star,
                1, 3900, 1f, 3),
        new Enemy("スター(S)", Utils.EnemyType.Star, 1, 1, 9999, 3f,  Utils.Effect.Lightning, Utils.EnemyMoveType.Star,
                1, 4200, 1f, 3),
        new Enemy("スター(S)", Utils.EnemyType.Star, 1, 1, 9999, 3f, Utils.EnemyMoveType.Star,
                1, 4500, 1f, 3),

        //w4
        new Enemy("スター(S)", Utils.EnemyType.Star, 1, 1, 9999, 3f, Utils.Effect.Cold, Utils.EnemyMoveType.Star,
                1, 4700, 1f, 4),
        new Enemy("スター(S)", Utils.EnemyType.Star, 1, 1, 9999, 3f, Utils.Effect.Fire, Utils.EnemyMoveType.Star,
                1, 4900, 1f, 4),
        new Enemy("スター(S)", Utils.EnemyType.Star, 1, 1, 9999, 3f, Utils.Effect.Chain, Utils.EnemyMoveType.Star,
                1, 5100, 1f, 4),
        new Enemy("スター(S)", Utils.EnemyType.Star, 1, 1, 9999, 3f, Utils.Effect.Slash, Utils.EnemyMoveType.Star,
                1, 5300, 1f, 4),
        new Enemy("スター(S)", Utils.EnemyType.Star, 1, 1, 9999, 3f, Utils.Effect.Poison, Utils.EnemyMoveType.Star,
                1, 5500, 1f, 4),

        new Enemy("スター(S)", Utils.EnemyType.Star, 1, 1, 9999, 3f, Utils.EnemyMoveType.Star,
                1, 5700, 1f, 4),
        new Enemy("スター(S)", Utils.EnemyType.Star, 1, 1, 9999, 3f, Utils.Effect.Fire, Utils.EnemyMoveType.Star,
                1, 5900, 1f, 4),
        new Enemy("スター(S)", Utils.EnemyType.Star, 1, 1, 9999, 3f, Utils.Effect.Fire, Utils.EnemyMoveType.Star,
                1, 6100, 1f, 4),
        new Enemy("スター(S)", Utils.EnemyType.Star, 1, 1, 9999, 3f, Utils.Effect.Chain, Utils.EnemyMoveType.Star,
                1, 6300, 1f, 4),
        new Enemy("スター(S)", Utils.EnemyType.Star, 1, 1, 9999, 3f, Utils.EnemyMoveType.Star,
                1, 6500, 1f, 4),
    };

    public static Enemy GetEnemyData(int stageNum, int enemyNum)
    {
        return _enemiesData[stageNum][enemyNum - 1];
    }

    public static Enemy GetStarData(int stageNum)
    {
        return _starData[stageNum];
    }

    public static int GetEnemyNum(int stageNum)
    {
        return _enemiesData[stageNum].Length;
    }
}
