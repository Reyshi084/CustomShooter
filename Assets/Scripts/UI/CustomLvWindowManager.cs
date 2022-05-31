using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomLvWindowManager : MonoBehaviour
{
    [SerializeField] ItemSelection _itemSelectionPrefab;
    [SerializeField] GameObject _selectWindow;
    [SerializeField] GameObject _filter;
    [SerializeField] Button _lvupButton;
    [SerializeField] Image _lvupButtonFrame;
    [SerializeField] Text _lvupButtonText;
    [SerializeField] Text _needNumText;

    [SerializeField] GameObject _lvUpWindow; // アイテム使用後
    [SerializeField] Text _lvupTextHead;
    [SerializeField] Text _lvupTextBody;

    public const int EFESIZE = 9;
    public const int ABISIZE = 5;
    public const int ABIOFFSET = 400;
    public const int EFEOFFSET = 500;
    public const int SIZE = 110;


    private enum WindowType
    {
        Ability,
        Effect,
    }
    private WindowType _windowType;

    public int SelectNum { set; get; }

    public int NeedNum { set; get; } = Utils.NeedFragNum;
    public Utils.EnemyType EnemyType { set; get; }
    public Utils.AbilityType AbilityType { set; get; }

    private GameObject _nowFilter;

    private List<ItemSelection> _items = new List<ItemSelection>();

    private AbiButtonManager _abiButtonManager;
    private EfeButtonManager _efeButtonManager;

    public void SetAbiWindow(Utils.EnemyType enemyType, AbiButtonManager abm)
    {
        EnemyType = enemyType;
        _abiButtonManager = abm;
        SelectNum = 0;
        _windowType = WindowType.Ability;
        SetNeedNumText();

        _nowFilter = Instantiate(_filter);
        _lvupButton.enabled = false;
        _lvUpWindow.SetActive(false);

        _lvupButtonText.color = Color.gray;
        _lvupButtonFrame.color = Color.gray;

        int numAbi = 0;
        int lvAbi = 0;
        switch (enemyType)
        {
            case Utils.EnemyType.Triangle:
                numAbi = 0;
                lvAbi = PlayerData.LV_ABI_TRI;
                break;
            case Utils.EnemyType.Square:
                numAbi = 1;
                lvAbi = PlayerData.LV_ABI_SQU;
                break;
            case Utils.EnemyType.Pentagon:
                numAbi = 2;
                lvAbi = PlayerData.LV_ABI_PNT;
                break;
            case Utils.EnemyType.Hexagon:
                numAbi = 3;
                lvAbi = PlayerData.LV_ABI_HEX;
                break;
            case Utils.EnemyType.Star:
                numAbi = 4;
                lvAbi = PlayerData.LV_ABI_STR;
                break;
        }

        for (int i = 0; i < EFESIZE; i++)
        {
            var item = Instantiate(_itemSelectionPrefab, _selectWindow.transform);
            item.transform.localPosition = new Vector3(0, ABIOFFSET - i * SIZE);
            item.SetInfo(i * Utils.EnemyTypeNum + numAbi, lvAbi + 1, this);
            _items.Add(item);
        }
    }

    public void SetEfeWindow(Utils.AbilityType abilityType, EfeButtonManager ebm)
    {
        AbilityType = abilityType;
        _efeButtonManager = ebm;
        SelectNum = 0;
        _windowType = WindowType.Effect;
        SetNeedNumText();

        _nowFilter = Instantiate(_filter);
        _lvupButton.enabled = false;
        _lvUpWindow.SetActive(false);

        _lvupButtonText.color = Color.gray;
        _lvupButtonFrame.color = Color.gray;

        switch (abilityType)
        {
            // Duration
            case Utils.AbilityType.Duration:
                for (int i = 0; i < ABISIZE; i++)
                {
                    var item = Instantiate(_itemSelectionPrefab, _selectWindow.transform);
                    item.transform.localPosition = new Vector3(0, EFEOFFSET - i * SIZE);
                    item.SetInfo(i + Utils.EnemyTypeNum * (int)Utils.Effect.Cold, PlayerData.LV_EFE_DUR + 1, this);
                    _items.Add(item);
                }
                for (int i = 0; i < ABISIZE; i++)
                {
                    var item = Instantiate(_itemSelectionPrefab, _selectWindow.transform);
                    item.transform.localPosition = new Vector3(0, EFEOFFSET - i * SIZE - ABISIZE * SIZE);
                    item.SetInfo(i + Utils.EnemyTypeNum * (int)Utils.Effect.Chain, PlayerData.LV_EFE_DUR + 1, this);
                    _items.Add(item);
                }
                break;

            // Damage
            case Utils.AbilityType.Damage:
                for (int i = 0; i < ABISIZE; i++)
                {
                    var item = Instantiate(_itemSelectionPrefab, _selectWindow.transform);
                    item.transform.localPosition = new Vector3(0, EFEOFFSET - i * SIZE);
                    item.SetInfo(i + Utils.EnemyTypeNum * (int)Utils.Effect.Fire, PlayerData.LV_EFE_DMG + 1, this);
                    _items.Add(item);
                }
                for (int i = 0; i < ABISIZE; i++)
                {
                    var item = Instantiate(_itemSelectionPrefab, _selectWindow.transform);
                    item.transform.localPosition = new Vector3(0, EFEOFFSET - i * SIZE - ABISIZE * SIZE);
                    item.SetInfo(i + Utils.EnemyTypeNum * (int)Utils.Effect.Lightning, PlayerData.LV_EFE_DMG + 1, this);
                    _items.Add(item);
                }
                break;

            // Range
            case Utils.AbilityType.Size:
                for (int i = 0; i < ABISIZE; i++)
                {
                    var item = Instantiate(_itemSelectionPrefab, _selectWindow.transform);
                    item.transform.localPosition = new Vector3(0, EFEOFFSET - i * SIZE);
                    item.SetInfo(i + Utils.EnemyTypeNum * (int)Utils.Effect.Poison, PlayerData.LV_EFE_RNG + 1, this);
                    _items.Add(item);
                }
                for (int i = 0; i < ABISIZE; i++)
                {
                    var item = Instantiate(_itemSelectionPrefab, _selectWindow.transform);
                    item.transform.localPosition = new Vector3(0, EFEOFFSET - i * SIZE - ABISIZE * SIZE);
                    item.SetInfo(i + Utils.EnemyTypeNum * (int)Utils.Effect.Stream, PlayerData.LV_EFE_RNG + 1, this);
                    _items.Add(item);
                }
                break;
            case Utils.AbilityType.Percent:
                for (int i = 0; i < ABISIZE; i++)
                {
                    var item = Instantiate(_itemSelectionPrefab, _selectWindow.transform);
                    item.transform.localPosition = new Vector3(0, EFEOFFSET - i * SIZE);
                    item.SetInfo(i + Utils.EnemyTypeNum * (int)Utils.Effect.Slash, PlayerData.LV_EFE_PCNT + 1, this);
                    _items.Add(item);
                }
                for (int i = 0; i < ABISIZE; i++)
                {
                    var item = Instantiate(_itemSelectionPrefab, _selectWindow.transform);
                    item.transform.localPosition = new Vector3(0, EFEOFFSET - i * SIZE - ABISIZE * SIZE);
                    item.SetInfo(i + Utils.EnemyTypeNum * (int)Utils.Effect.Hypnosis, PlayerData.LV_EFE_PCNT + 1, this);
                    _items.Add(item);
                }
                break;
        }
    }

    public bool AddItem()
    {
        if(SelectNum == NeedNum)
        {
            return false;
        }
        else
        {
            SelectNum++;
            SetNeedNumText();
            CheckLvUp();
            return true;
        }
    }

    public bool SubItem()
    {
        if (SelectNum > 0)
        {
            SelectNum--;
            SetNeedNumText();
            CheckLvUp();
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SetNeedNumText()
    {
        _needNumText.text = "[" + SelectNum + "/" + NeedNum + "]";
        if(SelectNum == NeedNum)
        {
            _needNumText.color = Color.yellow;
        }
        else
        {
            _needNumText.color = Color.red - new Color(0, 0, 0, 0.5f);
        }
    }

    private void CheckLvUp()
    {
        if(SelectNum == NeedNum)
        {
            _lvupButton.enabled = true;
            _lvupButtonText.color = Color.yellow;
            _lvupButtonFrame.color = Color.red;
        }
        else
        {
            _lvupButton.enabled = false;
            _lvupButtonText.color = Color.gray;
            _lvupButtonFrame.color = Color.gray;
        }
    }

    public void OnBackButtonDown()
    {
        StartCoroutine(DestroyCoroutine());
    }

    public void OnLvUpButtonDown()
    {
        foreach(ItemSelection item in _items)
        {
            item.UseItems();
        }

        _selectWindow.SetActive(false);
        _lvUpWindow.SetActive(true);
        _lvupTextHead.text = "LV UP!!";

        int nowLv;

        switch (_windowType)
        {
            case WindowType.Ability:
                switch (EnemyType)
                {
                    case Utils.EnemyType.Triangle:
                        nowLv = ++PlayerData.LV_ABI_TRI;
                        PlayerData.P_ABI_TRI++;
                        _lvupTextBody.text = "COUNTER LV." + (nowLv - 1) + "->" + nowLv;
                        break;
                    case Utils.EnemyType.Square:
                        nowLv = ++PlayerData.LV_ABI_SQU;
                        PlayerData.P_ABI_SQU++;
                        _lvupTextBody.text = "SHIELD LV." + (nowLv - 1) + "->" + nowLv;
                        break;
                    case Utils.EnemyType.Pentagon:
                        nowLv = ++PlayerData.LV_ABI_PNT;
                        PlayerData.P_ABI_PNT++;
                        _lvupTextBody.text = "HEAL LV." + (nowLv - 1) + "->" + nowLv;
                        break;
                    case Utils.EnemyType.Hexagon:
                        nowLv = ++PlayerData.LV_ABI_HEX;
                        PlayerData.P_ABI_HEX++;
                        _lvupTextBody.text = "GUARDIAN LV." + (nowLv - 1) + "->" + nowLv;
                        break;
                    case Utils.EnemyType.Star:
                        nowLv = ++PlayerData.LV_ABI_STR;
                        PlayerData.P_ABI_STR++;
                        _lvupTextBody.text = "BONUS LV." + (nowLv - 1) + "->" + nowLv;
                        break;
                }
                break;
            case WindowType.Effect:
                switch (AbilityType)
                {
                    case Utils.AbilityType.Duration:
                        nowLv = ++PlayerData.LV_EFE_DUR;
                        PlayerData.P_EFE_DUR++;
                        _lvupTextBody.text = "DURATION LV." + (nowLv - 1) + "->" + nowLv;
                        break;
                    case Utils.AbilityType.Damage:
                        nowLv = ++PlayerData.LV_EFE_DMG;
                        PlayerData.P_EFE_DMG++;
                        _lvupTextBody.text = "DAMAGE LV." + (nowLv - 1) + "->" + nowLv;
                        break;
                    case Utils.AbilityType.Size:
                        nowLv = ++PlayerData.LV_EFE_RNG;
                        PlayerData.P_EFE_RNG++;
                        _lvupTextBody.text = "RANGE LV." + (nowLv - 1) + "->" + nowLv;
                        break;
                    case Utils.AbilityType.Percent:
                        nowLv = ++PlayerData.LV_EFE_PCNT;
                        PlayerData.P_EFE_PCNT++;
                        _lvupTextBody.text = "PERCENT LV." + (nowLv - 1) + "->" + nowLv;
                        break;
                }
                break;
        }


        switch (_windowType)
        {
            case WindowType.Ability:
                _abiButtonManager.SetText();
                break;
            case WindowType.Effect:
                _efeButtonManager.SetText();
                break;
        }
    }

    public void OnOKButtonDown()
    {
        StartCoroutine(DestroyCoroutine());
    }

    public IEnumerator DestroyCoroutine()
    {
        for (int i = 0; i < 20; i++)
        {
            transform.localScale -= new Vector3(0.025f, 0.025f, 0);
            yield return new WaitForSecondsRealtime(0.01f);
        }
        Destroy(_nowFilter);
        Destroy(gameObject);
    }
}
