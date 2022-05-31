using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// GameManagerにアタッチする
public class EnemySpriteData : MonoBehaviour
{
    // [SerializeField] private Sprite[]
    [SerializeField] private Sprite[] _TRI_sprites;
    [SerializeField] private Sprite[] _SQU_sprites;
    [SerializeField] private Sprite[] _PNT_sprites;
    [SerializeField] private Sprite[] _HEX_sprites;
    [SerializeField] private Sprite[] _STR_sprites;

    // 引数の識別名に応じたスプライト３種を渡す
    public Sprite[] GetSprite(Utils.EnemyType enemyType)
    {
        switch (enemyType)
        {
            case Utils.EnemyType.Triangle:
                return new Sprite[] 
                { 
                    _TRI_sprites[0], _TRI_sprites[1], _TRI_sprites[2]
                };
            case Utils.EnemyType.Square:
                return new Sprite[]
                {
                    _SQU_sprites[0], _SQU_sprites[1], _SQU_sprites[2]
                };
            case Utils.EnemyType.Pentagon:
                return new Sprite[]
                {
                    _PNT_sprites[0], _PNT_sprites[1], _PNT_sprites[2]
                };
            case Utils.EnemyType.Hexagon:
                return new Sprite[]
                {
                    _HEX_sprites[0], _HEX_sprites[1], _HEX_sprites[2]
                };
            case Utils.EnemyType.Star:
                return new Sprite[]
                {
                    _STR_sprites[0], _STR_sprites[1], _STR_sprites[2]
                };
            default:
                Debug.LogError(enemyType + "でのスプライト取得ができませんでした。");
                return null;
        }
    }
}
