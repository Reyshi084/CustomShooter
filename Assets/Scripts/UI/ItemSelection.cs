using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelection : MonoBehaviour
{
    [SerializeField] Text _text;
    [SerializeField] Image _gem;

    [SerializeField] Sprite[] _frags;

    public int ItemNum { protected set; get; }
    public int ItemLv { protected set; get; }
    public string ItemName { protected set; get; }


    public int StackNum { protected set; get; }


    private CustomLvWindowManager _clwm;


    public void SetInfo(int itemNum, int itemLv, CustomLvWindowManager customLvWindowManager)
    {
        ItemNum = itemNum;
        ItemLv = itemLv;
        ItemName = Enemy.GetItemName(itemNum, itemLv);

        _clwm = customLvWindowManager;

        StackNum = 0;
        SetText();
        SetGemColor();
        SetGemShape();
    }

    public void UseItems()
    {
        PlayerData.FRAGNUM[ItemNum, ItemLv - 1] -= StackNum;
    }

    private void SetText()
    {
        _text.text = ItemName + " x " + PlayerData.FRAGNUM[ItemNum, ItemLv - 1] + "   : " + StackNum;

        if(PlayerData.FRAGNUM[ItemNum, ItemLv - 1] == 0)
        {
            _text.color -= new Color(0, 0, 0, 0.5f);
        }
    }

    private void SetGemColor()
    {
        Color color = Utils.GetImageColor((Utils.Effect)(ItemNum / Utils.EnemyTypeNum));
        _gem.color = color;
    }

    private void SetGemShape()
    {
        var shapeNum = ItemNum % Utils.EnemyTypeNum;
        _gem.sprite = _frags[shapeNum];
    }

    public void OnPlusButtonDown()
    {
        if(PlayerData.FRAGNUM[ItemNum, ItemLv - 1] > StackNum && _clwm.AddItem())
        {
            StackNum++;
            SetText();
        }
    }

    public void OnMinusButtonDown()
    {
        if(StackNum > 0 && _clwm.SubItem())
        {
            StackNum--;
            SetText();
        }
    }
}
