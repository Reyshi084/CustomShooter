using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] EffectManager _effectPrefab;

    [SerializeField] private Sprite[] _fire;
    [SerializeField] private Sprite _stream;
    [SerializeField] private Sprite _poison;
    [SerializeField] private Sprite[] _cold;
    [SerializeField] private Sprite[] _lightning;
    [SerializeField] private Sprite _slash;
    [SerializeField] private Sprite _hypnosis;
    [SerializeField] private Sprite _outage;

    private bool _isFirstSprite;
    private Sprite _SPR_bullet1;
    private Sprite _SPR_bullet2;

    private Transform _tf;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private PlayerBehaviour _pb;

    private int _attack;
    private float _bulletSpeed;
    private Utils.Effect _effect;
    private float _duration;
    private int _damage;
    private float _percent;
    private float _size;

    private Utils.PlayerBulletType _playerBT;

    // 撃ったプレイヤのインスタンス, 撃った直後の攻撃力, 撃った直後の追加効果
    public void MoveBullet(PlayerBehaviour player, int attack, float speed, Utils.Effect effect, Utils.PlayerBulletType pbt, float argdec)
    {
        _tf = this.transform;
        _tf.rotation = new Quaternion(0, 0, 180, 0);
        _rb = this.gameObject.GetComponent<Rigidbody2D>();
        _sr = this.gameObject.GetComponent<SpriteRenderer>();

        _pb = player;
        _attack = attack;
        _bulletSpeed = speed * 10.0f;
        _effect = effect;

        _duration = PlayerData.Duration;
        _damage = PlayerData.Damage;
        _percent = PlayerData.Percent;
        _size = PlayerData.Size;

        _playerBT = pbt;

        switch (effect)
        {
            case Utils.Effect.Fire:
                _SPR_bullet1 = _fire[0];
                _SPR_bullet2 = _fire[1];
                StartCoroutine(ChangeSpriteCoroutine());
                break;
            case Utils.Effect.Stream:
                _sr.sprite = _stream;
                break;
            case Utils.Effect.Poison:
                _sr.sprite = _poison;
                break;
            case Utils.Effect.Cold:
                _SPR_bullet1 = _cold[0];
                _SPR_bullet2 = _cold[1];
                StartCoroutine(ChangeSpriteCoroutine());
                break;
            case Utils.Effect.Lightning:
                _SPR_bullet1 = _lightning[0];
                _SPR_bullet2 = _lightning[1];
                StartCoroutine(ChangeSpriteCoroutine());
                break;
            case Utils.Effect.Slash:
                _sr.sprite = _slash;
                break;
            case Utils.Effect.Hypnosis:
                _sr.sprite = _hypnosis;
                break;
            case Utils.Effect.Chain:
                _sr.sprite = _outage;
                break;
        }

        switch (pbt)
        {
            case Utils.PlayerBulletType.Normal:
                _rb.velocity = new Vector3(0, _bulletSpeed, 0);
                break;
            case Utils.PlayerBulletType.Counter:
                _sr.color -= new Color(0, 0, 0, 0.5f);
                transform.Rotate(0, 0, argdec + 90f);
                _rb.velocity = new Vector3(_bulletSpeed * Mathf.Cos(argdec * Mathf.Deg2Rad), _bulletSpeed * Mathf.Sin(argdec * Mathf.Deg2Rad));
                break;
            case Utils.PlayerBulletType.Guardian:
                _sr.color -= new Color(0, 0, 0, 0.5f);
                transform.Rotate(0, 0, argdec + 90f);
                _rb.velocity = new Vector3(_bulletSpeed * Mathf.Cos(argdec * Mathf.Deg2Rad), _bulletSpeed * Mathf.Sin(argdec * Mathf.Deg2Rad));
                break;
        }
    }

    private void Update()
    {
        if (Utils.BulletUpperLimit < _tf.position.y ||_tf.position.y < Utils.BulletLowerLimit)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        else if (collision.transform.CompareTag())
        {

        }
        */
        if (collision.transform.CompareTag("Enemy"))
        {
            var enemy = collision.GetComponent<EnemyBehaviour>();
            enemy.OnBulletDamage(_attack, _effect, _duration, _damage, _percent);

            if(_playerBT == Utils.PlayerBulletType.Normal)
            {
                _pb.AttackGuardian(transform.position);
            }
            Attack();
        }
        else if (collision.transform.CompareTag("EnemyBullet"))
        {
            Attack();
        }

    }

    private void Attack()
    {
        CreateEffect();

        Destroy(gameObject);
    }

    private void CreateEffect()
    {
        if (_effect == Utils.Effect.Stream || _effect == Utils.Effect.Poison)
        {
            var efe = Instantiate(_effectPrefab, this.transform.localPosition, Quaternion.identity, transform.parent);
            efe.SetEffect(_effect, _duration + 1.0f, _damage, _percent, _size, false);
        }
    }

    private IEnumerator ChangeSpriteCoroutine()
    {
        while (true)
        {
            ChangeSprite();


            yield return new WaitForSeconds(Utils.TickRate / 4);
        }
    }

    private void ChangeSprite()
    {
        if (_isFirstSprite)
        {
            _sr.sprite = _SPR_bullet1;
        }
        else
        {
            _sr.sprite = _SPR_bullet2;
        }
        _isFirstSprite = !_isFirstSprite;
    }

    private void OnDestroy()
    {
        if (_playerBT == Utils.PlayerBulletType.Normal)
        {
            _pb.BulletNum--;
        }
    }
}
