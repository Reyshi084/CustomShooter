using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public Utils.Effect Effect { protected set; get; }
    public float Duration { protected set; get; }
    public int Damage { protected set; get; }
    public float Percent { protected set; get; }
    public bool IsEnemyEffect { protected set; get; }

    private SpriteRenderer _sr;

    public void SetEffect(Utils.Effect effect, float duration, int damage, float percent, float size, bool isEnemyEffect)
    {
        _sr = GetComponent<SpriteRenderer>();

        Effect = effect;
        Damage = damage;
        Percent = percent;
        IsEnemyEffect = isEnemyEffect;

        transform.localScale = new Vector3(size, size);

        switch (effect)
        {
            case Utils.Effect.Stream:
                Duration = duration + 2.0f;
                _sr.color = new Color(0, 0, 1, 0.5f);
                break;
            case Utils.Effect.Poison:
                Duration = duration;
                _sr.color = new Color(0, 1, 0, 0.5f);
                break;
            default:
                Destroy(gameObject);
                break;
        }

        StartCoroutine(StayEffect());
    }

    private IEnumerator StayEffect()
    {
        yield return new WaitForSeconds(Duration);
        Destroy(gameObject);
    }

}
