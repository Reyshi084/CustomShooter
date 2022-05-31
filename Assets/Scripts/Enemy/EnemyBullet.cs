using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private EffectManager _effectPrefab;

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

    private int _attack;
    private float _bulletSpeed;
    private Utils.Effect _effect;
    private float _duration;
    private int _damage;
    private float _percent;


    public void MoveBullet(Utils.EnemyBulletType enemyBulletType, int attack, float bulletSpeed,
        Utils.Effect effect, float duration, int damage, float percent)
    {
        _tf = this.transform;
        _rb = this.gameObject.GetComponent<Rigidbody2D>();
        _sr = this.gameObject.GetComponent<SpriteRenderer>();

        _attack = attack;
        _bulletSpeed = bulletSpeed;
        _effect = effect;
        _duration = duration;
        _damage = damage;
        _percent = percent;

        _isFirstSprite = true;
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

        switch (enemyBulletType)
        {
            case Utils.EnemyBulletType.Normal:
                _rb.velocity = new Vector3(0, -bulletSpeed * 10.0f, 0);
                return;
        }
    }

    private void Update()
    {
        if(Utils.BulletUpperLimit < _tf.position.y || _tf.position.y < Utils.BulletLowerLimit)
        {
            CreateEffect();
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
        if (collision.transform.CompareTag("Player"))
        {
            var player = collision.GetComponentInParent<PlayerBehaviour>();
            player.OnBulletDamage(_attack, _effect, _duration, _damage, _percent);

            CreateEffect();
            Destroy(this.gameObject);
        }
        else if (collision.transform.CompareTag("P_Shield"))
        {
            var player = collision.GetComponentInParent<PlayerBehaviour>();
            player.OnAttackShield(_attack, _effect, _damage, _percent);

            CreateEffect();
            Destroy(gameObject);
        }
        else if (collision.transform.CompareTag("PlayerBullet"))
        {
            CreateEffect();
            Destroy(this.gameObject);
        }

    }

    private void CreateEffect()
    {
        if (_effect == Utils.Effect.Stream || _effect == Utils.Effect.Poison)
        {
            var efe = Instantiate(_effectPrefab, this.transform.localPosition, Quaternion.identity, transform.parent);
            efe.SetEffect(_effect, _duration, _damage, _percent, 1, true);
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
}
