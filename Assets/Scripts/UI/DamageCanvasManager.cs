using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageCanvasManager : MonoBehaviour
{
    [SerializeField] Text _text;
    [SerializeField] Outline _outline;

    // ダメージ量と位置を受け取り、その位置に表示
    public void DisplayDamage(int damage, bool toEnemyDamage)
    {
        _text.text = damage.ToString();

        if (toEnemyDamage)
        {
            _text.color = new Color(0.8f, 0.8f, 0.8f);
            _outline.effectColor = new Color(0, 0, 0);
        }
        else
        {
            _text.color = Color.red; 
            _outline.effectColor = new Color(0, 0, 0);
        }

        StartCoroutine(TextMoveCoroutine());

    }

    public void DisplayHeal(int damage)
    {
        _text.text = damage.ToString();
        _text.color = Color.green;
        _outline.effectColor = new Color(0, 0, 0);

        StartCoroutine(TextMoveCoroutine());
    }

    private IEnumerator TextMoveCoroutine()
    {
        transform.Translate(new Vector3(Random.Range(-9f, 9f), 9f));

        for(int i = 0; i < 10; i++)
        {
            transform.Translate(new Vector3(0, 2f));
            /*
            _text.color -= new Color(0, 0, 0, 0.1f);
            _outline.effectColor -= new Color(0, 0, 0, 0.05f);
            */
            yield return new WaitForSeconds(0.05f);
        }

        Destroy(this.gameObject);
    }
}
