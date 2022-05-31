using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EffectSelectManager : MonoBehaviour
{
    [SerializeField] GameObject _selectWindow;
    [SerializeField] GameObject _filter;
    [SerializeField] Sprite[] _icon;
    [SerializeField] Sprite _none;
    [SerializeField] Image _selectButton;

    [SerializeField] Image _poison;
    [SerializeField] Image _fire;
    [SerializeField] Image _stream;
    [SerializeField] Image _lightning;
    [SerializeField] Image _cold;
    [SerializeField] Image _hypnosis;
    [SerializeField] Image _slash;
    [SerializeField] Image _chain;

    [SerializeField] Text _description;

    private Utils.Effect _nowSelectEffect;
    private Utils.Effect _prevSelectEffect;

    private GameObject _nowFilter;

    private readonly Color SelectColor = Color.red;

    private void Start()
    {
        _nowSelectEffect = PlayerData.AddingEffect;
        _prevSelectEffect = Utils.Effect.None;

        _selectWindow.SetActive(false);

        SetIcon();
        SetDescription();
    }

    private void SetIcon()
    {
        switch (_nowSelectEffect)
        {
            case Utils.Effect.Poison:
                _selectButton.sprite = _icon[0];
                break;
            case Utils.Effect.Fire:
                _selectButton.sprite = _icon[1];
                break;
            case Utils.Effect.Stream:
                _selectButton.sprite = _icon[2];
                break;
            case Utils.Effect.Lightning:
                _selectButton.sprite = _icon[3];
                break;
            case Utils.Effect.Cold:
                _selectButton.sprite = _icon[4];
                break;
            case Utils.Effect.Hypnosis:
                _selectButton.sprite = _icon[5];
                break;
            case Utils.Effect.Slash:
                _selectButton.sprite = _icon[6];
                break;
            case Utils.Effect.Chain:
                _selectButton.sprite = _icon[7];
                break;
            default:
                _selectButton.sprite = _none;
                break;
        }
    }

    private void SetCursor()
    {
        if (_nowSelectEffect == _prevSelectEffect)
        {
            _nowSelectEffect = Utils.Effect.None;
        }

        switch (_prevSelectEffect)
        {
            case Utils.Effect.None:
                break;
            case Utils.Effect.Poison:
                _poison.color = Color.white;
                break;
            case Utils.Effect.Fire:
                _fire.color = Color.white;
                break;
            case Utils.Effect.Stream:
                _stream.color = Color.white;
                break;
            case Utils.Effect.Lightning:
                _lightning.color = Color.white;
                break;
            case Utils.Effect.Cold:
                _cold.color = Color.white;
                break;
            case Utils.Effect.Hypnosis:
                _hypnosis.color = Color.white;
                break;
            case Utils.Effect.Slash:
                _slash.color = Color.white;
                break;
            case Utils.Effect.Chain:
                _chain.color = Color.white;
                break;
        }

        switch (_nowSelectEffect)
        {
            case Utils.Effect.None:
                break;
            case Utils.Effect.Poison:
                _poison.color = SelectColor;
                break;
            case Utils.Effect.Fire:
                _fire.color = SelectColor;
                break;
            case Utils.Effect.Stream:
                _stream.color = SelectColor;
                break;
            case Utils.Effect.Lightning:
                _lightning.color = SelectColor;
                break;
            case Utils.Effect.Cold:
                _cold.color = SelectColor;
                break;
            case Utils.Effect.Hypnosis:
                _hypnosis.color = SelectColor;
                break;
            case Utils.Effect.Slash:
                _slash.color = SelectColor;
                break;
            case Utils.Effect.Chain:
                _chain.color = SelectColor;
                break;
        }



        SetDescription();
    }

    public IEnumerator DisplayCoroutine()
    {
        _nowFilter = Instantiate(_filter);
        _selectWindow.transform.localScale = new Vector3(0, 0);
        for (int i = 0; i < 20; i++)
        {
            _selectWindow.transform.localScale += new Vector3(0.05f, 0.05f, 0);
            yield return new WaitForSecondsRealtime(0.01f);
        }
    }

    public IEnumerator DestroyCoroutine()
    {
        for (int i = 0; i < 20; i++)
        {
            _selectWindow.transform.localScale -= new Vector3(0.05f, 0.05f, 0);
            yield return new WaitForSecondsRealtime(0.01f);
        }
        Destroy(_nowFilter);
        _selectWindow.SetActive(false);
    }

    public void OnSelectButtonDown()
    {
        _selectWindow.SetActive(true);
        StartCoroutine(DisplayCoroutine());
        SetCursor();
    }

    public void OnUndoButtonDown()
    {
        SetIcon();
        StartCoroutine(DestroyCoroutine());
        PlayerData.SetEffect(_nowSelectEffect);
    }


    public void OnPoisonButtonDown()
    {
        _prevSelectEffect = _nowSelectEffect;
        _nowSelectEffect = Utils.Effect.Poison;
        SetCursor();
    }
    public void OnFireButtonDown()
    {
        _prevSelectEffect = _nowSelectEffect;
        _nowSelectEffect = Utils.Effect.Fire;
        SetCursor();
    }
    public void OnStreamButtonDown()
    {
        _prevSelectEffect = _nowSelectEffect;
        _nowSelectEffect = Utils.Effect.Stream;
        SetCursor();
    }
    public void OnLightningButtonDown()
    {
        _prevSelectEffect = _nowSelectEffect;
        _nowSelectEffect = Utils.Effect.Lightning;
        SetCursor();
    }
    public void OnColdButtonDown()
    {
        _prevSelectEffect = _nowSelectEffect;
        _nowSelectEffect = Utils.Effect.Cold;
        SetCursor();
    }
    public void OnHypnosisButtonDown()
    {
        _prevSelectEffect = _nowSelectEffect;
        _nowSelectEffect = Utils.Effect.Hypnosis;
        SetCursor();
    }
    public void OnSlashButtonDown()
    {
        _prevSelectEffect = _nowSelectEffect;
        _nowSelectEffect = Utils.Effect.Slash;
        SetCursor();
    }
    public void OnChainButtonDown()
    {
        _prevSelectEffect = _nowSelectEffect;
        _nowSelectEffect = Utils.Effect.Chain;
        SetCursor();
    }

    private void SetDescription()
    {
        _description.text = GetDescription(_nowSelectEffect);
    }

    private string GetDescription(Utils.Effect effect)
    {
        switch (effect)
        {
            case Utils.Effect.None:
                return "下のEFFECTから\n好きなものを選ぼう！";
            case Utils.Effect.Poison:
                return "猛毒弾\n\n着弾点に毒をまき\n敵が毒に触れると\nダメージを与える\n\n敵が群れている時に強い";
            case Utils.Effect.Fire:
                return "火炎弾\n\n当たった敵に一定時間\nダメージを与え続ける\n\n単体の敵に強い";
            case Utils.Effect.Stream:
                return "水流弾\n\n着弾点に水をまき\n敵が水に触れると\n移動速度を低下させる\n\n素早い敵の群れを足止めできる";
            case Utils.Effect.Lightning:
                return "雷撃弾\n\n当たった敵を感電させ\n弾を当てる毎に\n感電状態の敵に\nダメージを与える\n\n多くの敵に攻撃できる";
            case Utils.Effect.Cold:
                return "氷結弾\n\n当たった敵を一定時間\n移動できなくさせる\n＆解氷後ダメージを与える\n\n素早い敵を足止めできる";
            case Utils.Effect.Hypnosis:
                return "睡眠弾\n\n当たった敵の攻撃と\n防御と移動速度を\n一定期間低下させる\n\n強敵に対して強い";
            case Utils.Effect.Slash:
                return "斬撃弾\n\n当たった敵の残りHP\nに応じてダメージを与える\n\n敵の体力が多いほど強い";
            case Utils.Effect.Chain:
                return "鉄鎖弾\n\n当たった敵を拘束し\n攻撃不能にする\n& 拘束している敵の数\nに応じて自身のATKが上がる\n\n敵が多いほど強くなれる";
        }
        return "";
    }
}
