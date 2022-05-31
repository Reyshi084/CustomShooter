using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    [SerializeField] private PlayerBehaviour _player;
    [SerializeField] private EnemyBehaviour _enemyPrefab;
    [SerializeField] private GameClearCanvasManager _gccm;
    [SerializeField] private TutorialManager _tutMng;
    [SerializeField] private Slider _enemyHpBar;

    private List<EnemyBehaviour> _enemies;

    private float _enemyBulletInterval;

    private int _maxEnemyNum;
    private int _currentEnemyNum;

    public float EnemyStandardSpeed { protected set; get; }

    private bool _gameover;

    private int _stageNum;


    private class Item
    {
        internal int itemNum;
        internal int itemLv;

        internal Item(int n, int l)
        {
            itemNum = n;
            itemLv = l;
        }
    }
    private List<Item> _gainItems;
    private int _gainExp;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StageCreate(SelectSceneManager.SelectStageNum));
        _tutMng.DisplayTutorial(SelectSceneManager.SelectStageNum, true, _stageNum);
    }

    // StageDataとEnemyDataの情報をもとに、ステージを生成する
    private IEnumerator StageCreate(int stageNum)
    {
        _stageNum = stageNum;

        if(stageNum == 0)
        {
            PlayerData.LoadPlayerData();
        }

        // デバッグ用
        // PlayerData.SetEffect(Utils.Effect.Chain);

        EnemyBehaviour.InitChainNum();
        EnemyBehaviour.InitChargedEnemy();

        _enemies = new List<EnemyBehaviour>();
        _gainItems = new List<Item>();
        _gainExp = 0;

        var esd = this.GetComponent<EnemySpriteData>(); // ステージごとの敵データ取得

        var Xlen = StageData.GetStageLengthX(stageNum);
        var Ylen = StageData.GetStageLengthY(stageNum);

        _enemyBulletInterval = StageData.GetBulletInterval(stageNum);

        _maxEnemyNum = _currentEnemyNum = StageData.GetEnemyNum(stageNum);

        EnemyStandardSpeed = StageData.GetEnemyStandardSpeed(stageNum);

        _gameover = false;
        
        _enemyHpBar.gameObject.SetActive(false);


        StartCoroutine(LaunchBulletCoroutine()); // t秒に１回攻撃が来る場合

        // 敵の生成
        for (int y = 0; y < Ylen; y++)
        {
            for(int x = 0; x < Xlen; x++)
            {
                var enemyNum = StageData.GetStageData(stageNum, x, y);
                if (enemyNum != 0)
                {
                    var enemy = Instantiate(_enemyPrefab, new Vector3(((-Xlen / 2) + x) * Utils.CellSize, Utils.EnemyPosY), Quaternion.identity, this.transform);
                    var enemyData = EnemyData.GetEnemyData(stageNum, enemyNum);
                    enemy.SetEnemy(enemyData, esd.GetSprite(enemyData.EnemyType), this);
                    _enemies.Add(enemy);
                }
            }

            yield return new WaitForSeconds(Utils.CellSize / EnemyStandardSpeed);
        }

        // スターの処理
        if(Random.Range(0f, 1f) <= Utils.StarAppearRate)
        {
            int dir;
            if(Random.Range(0, 2) == 1)
            {
                dir = 1;
            }
            else
            {
                dir = -1;
            }
            var enemy = Instantiate(_enemyPrefab, new Vector3(dir * Utils.EnemySideLimit, Utils.StarPosY), Quaternion.identity, this.transform);
            var enemyData = EnemyData.GetStarData(stageNum);
            enemy.SetEnemy(enemyData, esd.GetSprite(enemyData.EnemyType), this);
            _currentEnemyNum++;
        }
    }


    private IEnumerator LaunchBulletCoroutine()
    {
        // インターバルが0以下の場合、弾は発射されない
        if (_enemyBulletInterval <= 0f)
        {
            yield break;
        }

        while (true)
        {
            if(_enemies.Count == 0)
            {
                yield return new WaitForSeconds(_enemyBulletInterval);
                continue;
            }

            var enemy = _enemies[Random.Range(0, _enemies.Count)];
            if (!enemy.gameObject.activeInHierarchy || enemy.IsBossEnemy)
            {
                _enemies.Remove(enemy);
                continue;
            }
            else
            {
                enemy.LaunchConstantBullet();
            }
            yield return new WaitForSeconds(_enemyBulletInterval);
        }
    }

    public void SetHpBar(int maxHP, int nowHP)
    {
        if (nowHP <= 0)
        {
            _enemyHpBar.gameObject.SetActive(false);
        }
        else
        {
            _enemyHpBar.gameObject.SetActive(true);

            _enemyHpBar.value = (float)nowHP / maxHP;
        }
    }

    public void StackGainItem(int itemNum, int itemLv)
    {
        _gainItems.Add(new Item(itemNum, itemLv));
    }

    public void DestroyEnemy()
    {
        _currentEnemyNum--;

        _player.Heal();

        if(_currentEnemyNum <= 0)
        {
            GameClear();
        }
    }

    //現在のところ、スター型のみ経験値が反映される
    public void GainExp(int exp)
    {
        _gainExp += exp;
    }

    public void AwayEnemy()
    {
        _currentEnemyNum--;

        if(_currentEnemyNum <= 0)
        {
            GameClear();
        }
    }

    private void GameClear()
    {
        var p = PlayerData.P_ABI_STR;
        var exp = StageData.GetExp(_stageNum) + _gainExp;
        exp = (int)(exp * (1 + (p * 0.1f) + Mathf.Max(0, p - 5) * 0.1f + Mathf.Max(0, p - 10) * 0.1f));
        var befLv = PlayerData.LV;
        var befStage = PlayerData.STAGE;
        PlayerData.GainEXP(exp);
        var aftLv = PlayerData.LV;
        var aftStage = PlayerData.STAGE;

        foreach (Item item in _gainItems)
        {
            PlayerData.GainItem(item.itemNum, item.itemLv);
        }

        _gccm.GameClear(exp, befLv, aftLv, _gainItems.Count, befStage, aftStage);
    }

    public void GameOver()
    {
        if (_gameover == false)
        {
            _gccm.GameOver();
            _gameover = true;
        }
    }

    public int GetStageNum()
    {
        return _stageNum;
    }
}
