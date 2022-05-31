using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] PlayerBullet _pbPrefab;
    [SerializeField] PlayerBullet _pbCounterPrefab;

    [SerializeField] Text _bulletNumText;
    [SerializeField] StageManager _stageManager;
    [SerializeField] DamageCanvasManager _dmgCanvas;
    [SerializeField] Text _hpText;
    [SerializeField] Slider _hpBar;

    [SerializeField] Image[] _effectIcon;
    [SerializeField] Image _lockBulletIcon;

    [SerializeField] Sprite[] _SPR_bullet;
    [SerializeField] Image _IMG_bullet;

    [SerializeField] GameObject _shield;
    [SerializeField] GameObject _counter;
    [SerializeField] GameObject _heal;
    [SerializeField] GuardianManager _guardianR;
    [SerializeField] GuardianManager _guardianL;

    [SerializeField] Sprite _SPR_shield;
    [SerializeField] Sprite _SPR_shieldBreak;

    private Transform _tf;
    private bool _leftButtonDown;
    private bool _rightButtonDown;

    private int _hp; // 現在のHP
    private int _attack; // 現在の攻撃力
    private int _defense; // 現在の防御力
    private Utils.Effect _effect; // 現在の追加効果


    private Coroutine _coroutineFire;
    private Coroutine _coroutineCold;
    private Coroutine _coroutineStream;
    private Coroutine _coroutineLightning;
    private Coroutine _coroutineHypnosis;
    private Coroutine _coroutinePoison;
    private Coroutine _coroutineSlash;
    private Coroutine _coroutineChain;
    private float _delayState1;
    private float _delayState2;
    private int _freeze;
    private bool _outage;
    private int _reverse;
    private float _hypPcnt;
    private float _burnTime;
    private float _reliefDelayPcnt;

    private int _bulletNum;
    private int _shieldDur;
    private int _MaxShieldDur;
    private SpriteRenderer _SR_shield;

    public int BulletNum 
    {
        set 
        {
            this._bulletNum = value;
            UpdateBulletNumText();
        }

        get 
        {
            return this._bulletNum;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SetPlayer();
    }

    private void SetPlayer()
    {
        _tf = this.transform;
        _tf.position = new Vector3(0, Utils.PlayerPosY);

        BulletNum = 0;
        _bulletNumText.text = PlayerData.BulletNum.ToString();
        SetCounter();
        _SR_shield = _shield.GetComponent<SpriteRenderer>();
        SetShield();
        SetHeal();
        SetGuardian();

        _hp = PlayerData.MAX_hp;
        _attack = PlayerData.MAX_attack;
        _defense = PlayerData.MAX_defense;
        _effect = PlayerData.AddingEffect;

        foreach(Image image in _effectIcon)
        {
            image.enabled = false;
        }
        _lockBulletIcon.gameObject.SetActive(false);

        _IMG_bullet.sprite = _SPR_bullet[(int)PlayerData.AddingEffect];

        _coroutineFire = null;
        _coroutineCold = null;
        _coroutineStream = null;
        _coroutineLightning = null;
        _coroutineHypnosis = null;
        _coroutinePoison = null;
        _coroutineSlash = null;
        _coroutineChain = null;
        _delayState1 = 1.0f;
        _delayState2 = 1.0f;
        _freeze = 1;
        _outage = false;
        _reverse = 1;
        _hypPcnt = 1f;
        _burnTime = 0;
        _reliefDelayPcnt = (20 - Mathf.Clamp(PlayerData.E_MSPD, 0, 20)) / 20.0f;

        _hpText.text = _hp.ToString();
        _hpBar.value = 1f;
    }

    // Update is called once per frame
    void Update()
    {
       // プレイヤーの移動
        // pcでの動作確認用
        var h = Input.GetAxisRaw("Horizontal");

        // スマートフォン用
        if (_rightButtonDown)
        {
            h += 1;
        }
        if (_leftButtonDown)
        {
            h -= 1;
        }

        h = Mathf.Clamp(h, -1, 1);

        _tf.Translate(h * PlayerData.MoveSpeed * Time.deltaTime * 4 * _delayState1 * _delayState2 * _freeze * _reverse, 0, 0);


        _tf.localPosition = new Vector3(Mathf.Clamp(_tf.localPosition.x, -Utils.MoveLimit, Utils.MoveLimit), Utils.PlayerPosY);


    }

    // 外部からのダメージはこのメソッドのみ
    public void OnBulletDamage(int attack, Utils.Effect effect, float duration, int damage, float percent)
    {
        if (effect != Utils.Effect.Slash)
        {
            // 弾の通常ダメージ
            OnDamage(attack, false);
        }
        // 追加効果付与
        AddEffect(effect, duration, damage, percent);
        Counter();
    }

    private void AddEffect(Utils.Effect effect, float duration, int damage, float percent)
    {
        switch (effect)
        {
            case Utils.Effect.Fire:
                if (_coroutineFire != null)
                {
                    StopCoroutine(_coroutineFire);
                }
                _coroutineFire = StartCoroutine(BurnCoroutine(duration, damage));
                break;
            case Utils.Effect.Cold:
                if (_coroutineCold != null)
                {
                    StopCoroutine(_coroutineCold);
                }
                _coroutineCold = StartCoroutine(FreezeCoroutine(duration));
                break;
            case Utils.Effect.Lightning:
                if (_coroutineLightning != null)
                {
                    StopCoroutine(_coroutineLightning);
                }
                _coroutineLightning = StartCoroutine(ConfuseCoroutine(duration));
                break;
            case Utils.Effect.Slash:
                OnDamagePercent(percent);
                if(_coroutineSlash != null)
                {
                    StopCoroutine(_coroutineSlash);
                }
                _coroutineSlash = StartCoroutine(ShockCoroutine());
                break;
            case Utils.Effect.Hypnosis:
                if (_coroutineHypnosis != null)
                {
                    StopCoroutine(_coroutineHypnosis);
                }
                _coroutineHypnosis = StartCoroutine(SleepCoroutine(duration, percent));
                break;
            case Utils.Effect.Chain:
                if (_coroutineChain != null)
                {
                    StopCoroutine(_coroutineChain);
                }
                _coroutineChain = StartCoroutine(OutageCoroutine(duration));
                break;
        }
    }

    private void OnDamage(int attack, bool isEffect)
    {

        int dmg;

        if(isEffect)
        {
            dmg = attack;
        }
        else
        {
            // 弾の場合
            dmg = attack - _defense;
        }

        DecHP(dmg);
    }

    private void OnDamagePercent(float percent)
    {
        int damage = (int)(_hp * percent);
        if(damage <= 0)
        {
            damage = 1;
        }

        OnDamage(damage, true);
    }


    private void DecHP(int damage)
    {
        // 0以下の値を取らない
        if(damage <= 0)
        {
            damage = 1;
        }

        // hpの減少とバーの更新
        _hp -= damage;
        if(_hp < 0)
        {
            _hp = 0;
        }
        _hpText.text = _hp.ToString();
        _hpBar.value = (float)_hp / PlayerData.MAX_hp;

        // ダメージテキストの表示
        var _dmgCanvasIns = Instantiate(_dmgCanvas, this.transform);
        _dmgCanvasIns.DisplayDamage(damage, false);
        
        // ゲームオーバー処理
        if (_hp <= 0)
        {
            StartCoroutine(BreakPlayer());
        }
    }

    private IEnumerator BreakPlayer()
    {
        yield return null;
        _stageManager.GameOver();
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Effect"))
        {
            var efe = collision.GetComponent<EffectManager>();
            if (!efe.IsEnemyEffect)
                return;

            switch (efe.Effect)
            {
                case Utils.Effect.Poison:
                    if (_coroutinePoison == null)
                    {
                        _coroutinePoison = StartCoroutine(PoisonCoroutine(efe.Damage));
                    }
                    break;
                case Utils.Effect.Stream:
                    if (_coroutineStream != null)
                    {
                        StopCoroutine(_coroutineStream);
                    }
                    _coroutineStream = StartCoroutine(StreamCoroutine(1 - ((1 - efe.Percent) * _reliefDelayPcnt)));
                    break;
            }
        }
    }

    private IEnumerator BurnCoroutine(float duration, int damage)
    {
        _effectIcon[1].enabled = true;
        _burnTime = duration;

        while (_burnTime > 0)
        {
            _burnTime -= 1.0f;
            yield return new WaitForSeconds(1.0f);
            OnDamage(damage, true);
        }
        _effectIcon[1].enabled = false;
        _coroutineFire = null;
    }

    private IEnumerator PoisonCoroutine(int damage)
    {
        _effectIcon[0].enabled = true;
        OnDamage(damage, true);
        yield return new WaitForSeconds(1.0f);

        _effectIcon[0].enabled = false;
        _coroutinePoison = null;
    }

    private IEnumerator StreamCoroutine(float percent)
    {
        _effectIcon[2].enabled = true;
        _delayState1 = percent;
        yield return new WaitForSeconds(0.2f);
        _effectIcon[2].enabled = false;
        _delayState1 = 1;
        _coroutineStream = null;
    }

    private IEnumerator FreezeCoroutine(float duration)
    {
        _effectIcon[4].enabled = true;
        _freeze = 0;
        yield return new WaitForSeconds(duration);
        _effectIcon[4].enabled = false;
        _freeze = 1;

        _coroutineCold = null;
    }

    private IEnumerator ConfuseCoroutine(float duration)
    {
        _effectIcon[3].enabled = true;
        _reverse = -1;
        yield return new WaitForSeconds(duration);
        _effectIcon[3].enabled = false;
        _reverse = 1;

        _coroutineLightning = null;
    }

    private IEnumerator SleepCoroutine(float duration, float percent)
    {
        _effectIcon[5].enabled = true;
        _hypPcnt = percent;
        UpdateATK();
        UpdateDFE();
        _delayState2 = 1 - ((1 - percent) * _reliefDelayPcnt);
        yield return new WaitForSeconds(duration);
        _effectIcon[5].enabled = false;
        _hypPcnt = 1f;
        _delayState2 = 1f;
        UpdateATK();
        UpdateDFE();

        _coroutineHypnosis = null;
    }

    private IEnumerator ShockCoroutine()
    {
        _effectIcon[6].enabled = true;
        yield return new WaitForSeconds(1.0f);
        _effectIcon[6].enabled = false;

        _coroutineSlash = null;
    }

    private IEnumerator OutageCoroutine(float duration)
    {
        _lockBulletIcon.gameObject.SetActive(true);
        _effectIcon[7].enabled = true;
        _outage = true;
        yield return new WaitForSeconds(duration);
        _outage = false;
        _lockBulletIcon.gameObject.SetActive(false);
        _effectIcon[7].enabled = false;

        _coroutineChain = null;
    }

    private void UpdateATK()
    {
        _attack = (int)(PlayerData.MAX_attack * _hypPcnt);
    }
    private void UpdateDFE()
    {
        _defense = (int)(PlayerData.MAX_defense * _hypPcnt);
    }


    private void SetCounter()
    {
        if(PlayerData.P_ABI_TRI == 0)
        {
            _counter.SetActive(false);
        }
        else
        {
            _counter.SetActive(true);
        }
    }

    private void Counter()
    {
        int bltNum = Mathf.Clamp(PlayerData.P_ABI_TRI, 0, 6);
        float bltSpd = 10f + PlayerData.P_ABI_TRI + PlayerData.E_BSPD;
        int bltAtk = Mathf.FloorToInt(_attack * ((PlayerData.P_ABI_TRI + 5) / 10.0f));

        for (int i = 0; i < bltNum; i++)
        {
            var bullet1 = Instantiate(_pbCounterPrefab, this.transform.localPosition + new Vector3(0, 16), Quaternion.identity, transform.parent);
            bullet1.MoveBullet(this, bltAtk, bltSpd, Utils.Effect.None, Utils.PlayerBulletType.Counter, 90f - (60f / (bltNum + 1f)) * (i + 1));
            var bullet2 = Instantiate(_pbCounterPrefab, this.transform.localPosition + new Vector3(0, 16), Quaternion.identity, transform.parent);
            bullet2.MoveBullet(this, bltAtk, bltSpd, Utils.Effect.None, Utils.PlayerBulletType.Counter, 90f + (60f / (bltNum + 1f)) * (i + 1));
        }
        
    }

    private void SetShield()
    {
        if(PlayerData.P_ABI_SQU == 0)
        {
            _shield.SetActive(false);
        }
        else
        {
            _MaxShieldDur = _shieldDur = (int)(PlayerData.MAX_hp * (PlayerData.P_ABI_SQU * 0.06f + 0.2f));
        }
        _SR_shield.color = new Color(0.5f, 1, 1);
    }

    public void OnAttackShield(int attack, Utils.Effect effect, int damage, float percent)
    {
        if (effect == Utils.Effect.Slash)
        {
            _shieldDur -= Mathf.Max((int)(_shieldDur * percent), damage);
        }
        else
        {
            _shieldDur -= Mathf.Clamp(attack - _defense, 1, attack + 1);
        }

        if(_shieldDur <= 0)
        {
            _shieldDur = 0;
            StartCoroutine(BreakShield());
        }

        _SR_shield.color = new Color(0.5f, 1, 1, _shieldDur/(_MaxShieldDur * 2f) + 0.5f);
    }

    private IEnumerator BreakShield()
    {
        _shield.GetComponent<SpriteRenderer>().sprite = _SPR_shieldBreak;
        _shield.GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(0.5f);

        _shield.SetActive(false);
    }

    private void SetHeal()
    {
        if(PlayerData.P_ABI_PNT == 0)
        {
            _heal.SetActive(false);
        }
        else
        {
            _heal.SetActive(true);
        }
    }

    public void Heal()
    {
        if(PlayerData.P_ABI_PNT == 0)
        {
            return;
        }

        // hpの増加とバーの更新
        int heal = Mathf.FloorToInt(PlayerData.MAX_hp * (PlayerData.P_ABI_PNT * 0.02f + 0.1f));

        _hp = Mathf.Clamp(_hp + heal, 0, PlayerData.MAX_hp);
        _hpText.text = _hp.ToString();
        _hpBar.value = (float)_hp / PlayerData.MAX_hp;

        // ダメージテキストの表示
        var _dmgCanvasIns = Instantiate(_dmgCanvas, this.transform);
        _dmgCanvasIns.DisplayHeal(heal);
    }

    public void SetGuardian()
    {
        if(PlayerData.P_ABI_HEX == 0)
        {
            _guardianL.gameObject.SetActive(false);
            _guardianR.gameObject.SetActive(false);
        }
        else
        {
            _guardianL.gameObject.SetActive(true);
            _guardianR.gameObject.SetActive(true);
        }
    }

    public void AttackGuardian(Vector3 enemyPos)
    {
        if(PlayerData.P_ABI_HEX == 0)
        {
            return;
        }

        int bltAtk = Mathf.FloorToInt(_attack * (PlayerData.P_ABI_TRI * 0.04f + 0.4f));
        float bltSpd = 30.0f + PlayerData.P_ABI_HEX * 2;

        _guardianL.LaunchBullet(enemyPos, bltAtk, bltSpd);
        _guardianR.LaunchBullet(enemyPos, bltAtk, bltSpd);
    }

    private void UpdateBulletNumText()
    {
        var bnum = PlayerData.BulletNum - BulletNum;
        _bulletNumText.text = bnum.ToString();
    }

    // ボタンの処理
    public void OnAttackButtonDown()
    {
        if (PlayerData.BulletNum > BulletNum && !_outage)
        {
            var bullet = Instantiate(_pbPrefab, this.transform.localPosition + new Vector3(0, 16), Quaternion.identity, transform.parent);
            bullet.MoveBullet(this, _attack, PlayerData.BulletSpeed, _effect, PlayerData.PBT, 90);
            BulletNum++;
            UpdateBulletNumText();
        }
    }

    public void OnRightButtonDown()
    {
        _rightButtonDown = true;
    }

    public void OnRightButtonUp()
    {
        _rightButtonDown = false;
    }

    public void OnLeftButtonDown()
    {
        _leftButtonDown = true;
    }

    public void OnLeftButtonUp()
    {
        _leftButtonDown = false;
    }





}
