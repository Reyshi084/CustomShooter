using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//GameManagerにアタッチする
//EnemyDataで作成されたデータを元に、敵インスタンス（このクラスのインスタンス）が生成される
//EnemySpriteDataからスプライト情報を取得する
public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] EnemyBullet _ebPrefab;
    [SerializeField] DamageCanvasManager _dmgCanvas;
    [SerializeField] SpriteRenderer _SR_aura;
    [SerializeField] SpriteRenderer _SR_shine;

    public bool IsBossEnemy { protected set; get; }

    private Enemy _enemy;
    private StageManager _stageManager;

    private Sprite _SPR_enemyIdle1;
    private Sprite _SPR_enemyIdle2;
    private Sprite _SPR_enemyBang;


    private int _hp; // 現在のHP
    private int _attack; // 現在の攻撃力
    private int _defense; // 現在の防御力


    private List<EffectManager> _touchedEffect = new List<EffectManager>();
    private Coroutine _coroutineFire;
    private Coroutine _coroutineStream;
    private Coroutine _coroutineCold;
    private Coroutine _coroutineLightning;
    private Coroutine _coroutineHypnosis;
    private Coroutine _coroutinePoison;
    private Coroutine _coroutineChain;
    private float _delayStream;
    private float _delaySleep;
    private int _freeze;
    private bool _outage;
    private float _hypPcnt;
    private float _burnTime;
    private static List<EnemyBehaviour> ChargedEnemy = new List<EnemyBehaviour>();
    private static int ChainNum;

    private SpriteRenderer _sr;
    private bool _isFirstSprite; // スプライト変更時に使用する
    private Coroutine _coroutineAura;

    private Rigidbody2D _rb;
    private Transform _tf;

    private bool _break; // 破壊されたかどうか
    private bool _shine; // アイテムを落としたかどうか


    private void Update()
    {
        if(transform.position.y < Utils.EnemyLowerLimit)
        {
            _stageManager.GameOver();
        }
    }

    public void SetEnemy(Enemy enemy, Sprite[] sprites, StageManager stageManager)
    {
        IsBossEnemy = false;

        _enemy = enemy;
        _stageManager = stageManager;

        _SPR_enemyIdle1 = sprites[0];
        _SPR_enemyIdle2 = sprites[1];
        _SPR_enemyBang = sprites[2];

        _hp = enemy.MAX_hp;
        _attack = enemy.MAX_attack;
        _defense = enemy.MAX_defense;

        _sr = GetComponent<SpriteRenderer>();
        _sr.sprite = _SPR_enemyIdle1;
        _isFirstSprite = true;

        _rb = GetComponent<Rigidbody2D>();
        _tf = this.transform;

        transform.localScale = new Vector3(_enemy.Size, _enemy.Size, 1);

        _break = false;
        _shine = false;

        _touchedEffect.Clear();
        _coroutineFire = null;
        _coroutineCold = null;
        _coroutineLightning = null;
        _coroutineHypnosis = null;
        _coroutinePoison = null;
        _coroutineChain = null;
        _delayStream = 1.0f;
        _delaySleep = 1.0f;
        _freeze = 1;
        _outage = false;
        _hypPcnt = 1f;
        _burnTime = 0;

        StartCoroutine(StartMove());
        StartCoroutine(ChangeSpriteCoroutine());
        StartCoroutine(AuraCoroutine());

        _SR_shine.enabled = false;

        if(_enemy.IsBoss)
        {
            IsBossEnemy = true;
            StartCoroutine(ConstantBulletCoroutine());
        }
    }

    private IEnumerator StartMove()
    {
        var spd = _enemy.RegularMoveSpeed * _stageManager.EnemyStandardSpeed;
        var cellSize = Utils.CellSize;
        var startPosX = _tf.position.x;
        int dir = 1;

        switch (_enemy.EMT)
        {
            case Utils.EnemyMoveType.Stay:
                while (!_break)
                {
                    _tf.Translate(0, -spd * Time.deltaTime * _delayStream * _delaySleep * _freeze, 0); // 縦移動
                    yield return null;
                }
                yield break;
            case Utils.EnemyMoveType.Slide_s:
                while (!_break)
                {
                    _tf.Translate(0, -spd * Time.deltaTime * _delayStream * _delaySleep * _freeze, 0); // 縦移動
                    _tf.Translate(dir * _enemy.MoveSpeed * Time.deltaTime * _delayStream * _delaySleep * _freeze, 0, 0); // 横移動

                    if (dir == 1 && _tf.position.x - startPosX > cellSize)
                    {
                        dir = -1;
                    }
                    else if(dir == -1 && startPosX - _tf.position.x > cellSize)
                    {
                        dir = 1;
                    }

                    yield return null;
                }
                yield break;
            case Utils.EnemyMoveType.Star:
                if(_tf.localPosition.x > 0)
                {
                    dir = -1;
                }
                else
                {
                    dir = 1;
                }
                while (!_break && _tf.localPosition.x <= Utils.EnemySideLimit && -Utils.EnemySideLimit <= _tf.localPosition.x)
                {
                    _tf.Translate(dir * _enemy.MoveSpeed * Time.deltaTime * _delayStream * _delaySleep * _freeze * 7f, 0, 0); // 横移動
                    yield return null;
                }
                if (!_break)
                {
                    StartCoroutine(AwayEnemy());
                }
                yield break;

        }


    }

    private IEnumerator ConstantBulletCoroutine()
    {
        if(_enemy.EnemyType == Utils.EnemyType.Star)
        {
            yield break;
        }

        yield return new WaitForSeconds(Random.Range(0f, _enemy.BulletInterval));

        while (!_break)
        {
            var randomTime = Random.Range(0, 1.0f);
            LaunchConstantBullet();
            yield return new WaitForSeconds(_enemy.BulletInterval + randomTime);
        }
    }

    public void LaunchConstantBullet()
    {
        if (_break || _outage)
        {
            return;
        }
        var bullet = Instantiate(_ebPrefab, this.transform.localPosition, Quaternion.identity, transform.parent);
        bullet.MoveBullet(_enemy.EBT, _attack, _enemy.BulletSpeed, _enemy.AddingEffect, _enemy.Duration, _enemy.Damage, _enemy.Percent);
    }

    // 敵に弾のダメージを与える
    public void OnBulletDamage(int attack, Utils.Effect effect, float duration, int damage, float percent)
    {
        if (_break)
        {
            return;
        }

        if (effect == Utils.Effect.Slash && _enemy.EnemyType != Utils.EnemyType.Hexagon)
        {
            OnDamagePercent(attack, CalcSlashPcnt(percent));
        }
        else
        {
            // 弾の通常ダメージ
            OnDamage(attack, false);
            // 追加効果付与
            AddEffect(effect, duration, damage, percent);
        }


    }

    // ダメージ計算とHP管理
    private void OnDamage(int attack, bool isEffect)
    {

        int dmg;

        if(ChainNum > 1)
        {
            //*
            attack = (int)(attack * (1 + CalcChainDmg(PlayerData.P_EFE_DMG) * (ChainNum - 1) + 0.1f));
        }

        if (isEffect)
        {
            if(_enemy.EnemyType == Utils.EnemyType.Hexagon)
            {
                dmg = 1;
            }
            else
            {
                dmg = attack;
            }
        }
        else
        {
            // 弾の場合
            dmg = attack - _defense;
        }

        DecHP(dmg);
    }

    private void OnDamagePercent(int damage, float percent)
    {
        var pcntDmg = (int)(_hp * percent);
        
        if((damage - _defense).CompareTo(pcntDmg) < 0)
        {
            OnDamage(pcntDmg, true);
        }
        else
        {
            OnDamage(damage, false);
        }

    }

    private void DecHP(int damage)
    {
        // 0以下を取らない
        if (damage <= 0)
        {
            damage = 1;
        }

        if(_hp <= 0)
        {
            return;
        }

        _hp -= damage;
        _stageManager.SetHpBar(_enemy.MAX_hp, _hp);

        var _dmgCanvasIns = Instantiate(_dmgCanvas, this.transform);
        _dmgCanvasIns.DisplayDamage(damage, true);

        if (_hp <= 0)
        {
            StartCoroutine(BreakEnemy());
        }
    }

    private void AddEffect(Utils.Effect effect, float duration, int damage, float percent)
    {
        switch (effect)
        {
            case Utils.Effect.Fire:
                _burnTime = CalcFireDur(duration);
                if (_coroutineFire == null)
                {
                    _coroutineFire = StartCoroutine(BurnCoroutine(CalcFireDmg(damage), CalcFirePcnt(percent)));
                }
                break;
            case Utils.Effect.Cold:
                if (_coroutineCold != null)
                {
                    StopCoroutine(_coroutineCold);
                }
                _coroutineCold = StartCoroutine(FreezeCoroutine(CalcFreezeDur(duration), CalcFreezeDmg(damage)));
                break;
            case Utils.Effect.Lightning:
                if (_coroutineLightning != null)
                {
                    StopCoroutine(_coroutineLightning);
                }
                _coroutineLightning = StartCoroutine(ConfuseCoroutine(CalcLightningDur(duration), CalcLightningDmg(damage)));
                break;
            case Utils.Effect.Hypnosis:
                if (_coroutineHypnosis != null)
                {
                    StopCoroutine(_coroutineHypnosis);
                }
                _coroutineHypnosis = StartCoroutine(SleepCoroutine(CalcHypnosisDur(duration), CalcHypnosisPcntAD(percent), CalcHypnosisPcntS(percent)));
                break;
            case Utils.Effect.Chain:
                if (_coroutineChain != null)
                {
                    StopCoroutine(_coroutineChain);
                }
                else
                {
                    ChainNum++;
                }
                _coroutineChain = StartCoroutine(OutageCoroutine(CalcChainDur(duration)));
                break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Effect"))
        {
            var efe = collision.GetComponent<EffectManager>();
            if (efe.IsEnemyEffect)
                return;


            switch (efe.Effect)
            {
                case Utils.Effect.Poison:
                    if(_enemy.EnemyType == Utils.EnemyType.Star)
                    {
                        break;
                    }
                    if (_coroutinePoison == null)
                    {
                        _coroutinePoison = StartCoroutine(PoisonCoroutine(CalcPoisonDmg(efe.Damage), CalcPoisonPcnt(efe.Percent)));
                    }
                    break;
                case Utils.Effect.Stream:
                    if (_coroutineStream != null)
                    {
                        StopCoroutine(_coroutineStream);
                    }
                    _coroutineStream = StartCoroutine(StreamCoroutine(CalcStreamPcnt(efe.Percent)));
                    break;
            }
        }
    }


    private IEnumerator BurnCoroutine(int damage, float percent)
    {
        _sr.color = Color.red - new Color(0,0,0,0.2f);

        while (_burnTime > 0)
        {
            _burnTime -= percent;
            yield return new WaitForSeconds(percent);
            OnDamage(damage, true);
        }

        _burnTime = 0;
        _coroutineFire = null;
        _sr.color = Color.white;

    }

    private IEnumerator PoisonCoroutine(int damage, float percent)
    {
        _sr.color = Color.green - new Color(0, 0, 0, 0.4f);

        OnDamage(damage, true);
        yield return new WaitForSeconds(percent);
        

        _coroutinePoison = null;
        _sr.color = Color.white;
    }

    private IEnumerator StreamCoroutine(float percent)
    {
        _sr.color = Color.blue - new Color(0, 0, 0, 0.4f);

        _delayStream = percent;
        yield return new WaitForSeconds(0.1f);
        _delayStream = 1;
        _coroutineStream = null;
        _sr.color = Color.white;
    }

    private IEnumerator FreezeCoroutine(float duration, int damage)
    {
        _freeze = 0;
        _sr.color = Color.cyan - new Color(0, 0, 0, 0.2f);
        yield return new WaitForSeconds(duration);
        _freeze = 1;
        _sr.color = Color.white;
        OnDamage(damage, true);

        _coroutineCold = null;
    }

    private IEnumerator OutageCoroutine(float duration)
    {
        _outage = true;
        _sr.color = Color.gray - new Color(0, 0, 0, 0.2f);
        yield return new WaitForSeconds(duration);
        _outage = false;
        _sr.color = Color.white;
        ChainNum--;

        _coroutineChain = null;
    }

    private IEnumerator ConfuseCoroutine(float duration, int damage)
    {
        foreach (EnemyBehaviour enemy in ChargedEnemy)
        {
            if (enemy.gameObject.activeInHierarchy)
            {
                enemy.OnDamage(damage, true);
            }
        }

        if (!ChargedEnemy.Contains(this))
        {
            ChargedEnemy.Add(this);
            _sr.color = Color.yellow - new Color(0, 0, 0, 0.2f);
        }
        yield return new WaitForSeconds(duration);
        ChargedEnemy.Remove(this);
        _sr.color = Color.white;

        _coroutineLightning = null;
    }

    private IEnumerator SleepCoroutine(float duration, float percentAD, float percentS)
    {
        _hypPcnt = percentAD;
        _sr.color = Color.magenta - new Color(0, 0, 0, 0.2f);
        UpdateATK();
        UpdateDFE();
        _delaySleep = percentS;
        yield return new WaitForSeconds(duration);
        _hypPcnt = 1;
        _sr.color = Color.white;
        UpdateATK();
        UpdateDFE();
        _delaySleep = 1;

        _coroutineHypnosis = null;
    }

    private void UpdateATK()
    {
        _attack = (int)(_enemy.MAX_attack * _hypPcnt);
    }

    private void UpdateDFE()
    {
        _defense = (int)(_enemy.MAX_defense * _hypPcnt);
    }


    // (enemy)BreakEnemy -> (stage)DestroyEnemy -> (stage)GameClear
    private IEnumerator BreakEnemy()
    {
        this.GetComponent<BoxCollider2D>().enabled = false;
        _break = true;
        ChangeSprite();
        if (_coroutineAura != null)
        {
            StopCoroutine(_coroutineAura);
        }
        _SR_aura.enabled = false;
        yield return new WaitForSeconds(0.5f);

        // アイテムの処理
        if (HaveFragment())
        {
            _shine = true;
            ChangeSprite();
            yield return StartCoroutine(ShineCoroutine());
            _stageManager.StackGainItem(Enemy.GetItemNum(_enemy.EnemyType, _enemy.AddingEffect), _enemy.ItemLv);
        }

        // チェインの削除
        if (_coroutineChain != null)
        {
            StopCoroutine(_coroutineChain);
            _coroutineChain = null;
            ChainNum--;
        }

        _stageManager.DestroyEnemy();
        if(_enemy.EnemyType == Utils.EnemyType.Star)
        {
            _stageManager.GainExp(_enemy.EXP);
        }
        this.gameObject.SetActive(false);
    }

    // 画面外のスターの処理
    private IEnumerator AwayEnemy()
    {
        this.GetComponent<BoxCollider2D>().enabled = false;
        _break = true;

        // チェインの削除
        if (_coroutineChain != null)
        {
            StopCoroutine(_coroutineChain);
            _coroutineChain = null;
            ChainNum--;
        }

        _stageManager.AwayEnemy();
        this.gameObject.SetActive(false);

        yield return null;
    }

    // DropRateが設定されている場合はその値を使用する
    // 設定されていない場合はSizeによって確率を分ける
    private bool HaveFragment()
    {
        float rand = Random.Range(0, 1f);
        float rate;

        if (_enemy.DropRate >= 0)
        {
            rate = _enemy.DropRate;
        }
        else
        {
            rate = _enemy.Size * 0.1f;
        }

        rate *= (1 + (PlayerData.P_ABI_STR * 0.05f));

        if(rate >= rand)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void InitChainNum()
    {
        ChainNum = 0;
    }

    public static void InitChargedEnemy()
    {
        ChargedEnemy.Clear();
    }


    private IEnumerator ChangeSpriteCoroutine()
    {
        while (!_break)
        {
            if(_freeze != 0)
                ChangeSprite();


            yield return new WaitForSeconds(Utils.TickRate);
        }
    }

    private IEnumerator AuraCoroutine()
    {
        if(_enemy.AddingEffect == Utils.Effect.None)
        {
            _SR_aura.gameObject.SetActive(false);
            yield break;
        }

        _SR_aura.color = Utils.GetImageColor(_enemy.AddingEffect);

        /*
        switch (_enemy.AddingEffect)
        {
            case Utils.Effect.Cold:
                _SR_aura.color = Color.cyan;
                break;
            case Utils.Effect.Fire:
                _SR_aura.color = Color.red;
                break;
            case Utils.Effect.Hypnosis:
                _SR_aura.color = Color.magenta;
                break;
            case Utils.Effect.Lightning:
                _SR_aura.color = Color.yellow;
                break;
            case Utils.Effect.Poison:
                _SR_aura.color = new Color(0, 1, 0);
                break;
            case Utils.Effect.Slash:
                _SR_aura.color = new Color(1, 0, 1);
                break;
            case Utils.Effect.Stream:
                _SR_aura.color = Color.blue;
                break;
            default:
                _SR_aura.gameObject.SetActive(false);
                yield break;
        }
        */
        
        _coroutineAura = StartCoroutine(FlashAuraCoroutine());

    }

    private IEnumerator FlashAuraCoroutine()
    {
        while (!_break)
        {
            for(int i = 0; i < 15; i++)
            {
                _SR_aura.color -= new Color(0, 0, 0, 0.05f);
                yield return new WaitForSeconds(0.06f);
            }

            for (int i = 0; i < 15; i++)
            {
                _SR_aura.color += new Color(0, 0, 0, 0.05f);
                yield return new WaitForSeconds(0.06f);
            }
        }

        _SR_aura.enabled = false;
    }

    private IEnumerator ShineCoroutine()
    {
        var tick = 20;
        var scaleRate = _enemy.Size / (float)tick;
        var colorRate = 1.0f / tick;
        var waitTime = 0.02f;

        _SR_shine.color = Utils.GetImageColor(_enemy.AddingEffect);

        for (int i = 0; i < tick; i++)
        {
            _tf.localScale -= new Vector3(scaleRate, scaleRate);
            _tf.Rotate(0, 0, 2);
            _SR_shine.color -= new Color(0, 0, 0, colorRate);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void ChangeSprite()
    {
        if (_shine)
        {
            _SR_shine.enabled = true;
            _sr.enabled = false;
        }
        else if (_break)
        {
            _sr.sprite = _SPR_enemyBang;
        }
        else if (_isFirstSprite)
        {
            _sr.sprite = _SPR_enemyIdle2;
        }
        else
        {
            _sr.sprite = _SPR_enemyIdle1;
        }
        _isFirstSprite = !_isFirstSprite;
    }



    public static int CalcPoisonDmg(int dmgp)
    {
        return dmgp * 2;
    }

    public static float CalcPoisonPcnt(float pcntp)
    {
        return 1.0f - pcntp;
    }

    public static float CalcStreamPcnt(float pcntp)
    {
        return 1 - pcntp * 1.5f - 0.3f;
    }

    public static float CalcFireDur(float durp)
    {
        return durp;
    }

    public static int CalcFireDmg(int dmgp)
    {
        return dmgp * 3;
    }

    public static float CalcFirePcnt(float pcntp)
    {
        return 1.0f - pcntp;
    }

    public static int CalcFreezeDmg(int dmgp)
    {
        return dmgp * 2;
    }

    public static float CalcFreezeDur(float durp)
    {
        return durp / 4.0f + 0.5f;
    }

    public static float CalcLightningDur(float durp)
    {
        return durp / 2.0f + 2.5f;
    }

    public static int CalcLightningDmg(int dmgp)
    {
        return (int)(dmgp / 1.15f);
    }

    public static float CalcHypnosisDur(float durp)
    {
        return durp;
    }

    public static float CalcHypnosisPcntAD(float pcntp)
    {
        return 1.0f - (pcntp + 0.4f);
    }

    public static float CalcHypnosisPcntS(float pcntp)
    {
        return 1 - pcntp * 2f - 0.1f;
    }

    public static float CalcChainDur(float durp)
    {
        return durp + 6.0f;
    }

    public static float CalcChainDmg(int dmgp)
    {
        return (dmgp + 5) * 0.01f;
    }

    public static float CalcSlashPcnt(float pcntp)
    {
        return pcntp / 2.0f + 0.1f;
    }
}
