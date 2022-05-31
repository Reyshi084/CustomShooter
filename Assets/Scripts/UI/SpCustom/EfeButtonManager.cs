using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EfeButtonManager : MonoBehaviour
{
    [SerializeField] Text _text;
    [SerializeField] Utils.AbilityType _abilityType;
    [SerializeField] CustomLvWindowManager _lvUpWindow;
    [SerializeField] GameObject _customWindowFrame;


    private void Start()
    {
        SetText();
    }

    public void OnEfePlusButtonDown()
    {
        switch (_abilityType)
        {
            case Utils.AbilityType.Duration:
                if (!PlayerData.AddEFEDUR())
                {
                    if (PlayerData.LV_EFE_DUR < Utils.ItemLvMax)
                    {
                        CreateWindow();
                    }
                    else
                    {
                        // レベルが最大のときの処理
                    }
                }
                else
                {
                    // ステータスを上げられた場合
                    SetText();
                }
                break;
            case Utils.AbilityType.Damage:
                if (!PlayerData.AddEFEDMG())
                {
                    if (PlayerData.LV_EFE_DMG < Utils.ItemLvMax)
                    {
                        CreateWindow();
                    }
                    else
                    {
                        // レベルが最大のときの処理
                    }
                }
                else
                {
                    SetText();
                }
                break;
            case Utils.AbilityType.Size:
                if (!PlayerData.AddEFERNG())
                {
                    if (PlayerData.LV_EFE_RNG < Utils.ItemLvMax)
                    {
                        CreateWindow();
                    }
                    else
                    {
                        // レベルが最大のときの処理
                    }
                }
                else
                {
                    SetText();
                }
                break;
            case Utils.AbilityType.Percent:
                if (!PlayerData.AddEFEPCNT())
                {
                    if (PlayerData.LV_EFE_PCNT < Utils.ItemLvMax)
                    {
                        CreateWindow();
                    }
                    else
                    {
                        // レベルが最大のときの処理
                    }
                }
                else
                {
                    SetText();
                }
                break;
        }


    }

    public void OnEfeMinusButtonDown()
    {
        switch (_abilityType)
        {
            case Utils.AbilityType.Duration:
                PlayerData.SubEFEDUR();
                SetText();
                break;
            case Utils.AbilityType.Damage:
                PlayerData.SubEFEDMG();
                SetText();
                break;
            case Utils.AbilityType.Size:
                PlayerData.SubEFERNG();
                SetText();
                break;
            case Utils.AbilityType.Percent:
                PlayerData.SubEFEPCNT();
                SetText();
                break;
        }
    }

    public void SetText()
    {
        switch (_abilityType)
        {
            case Utils.AbilityType.Duration:
                _text.text = "DURATION[" + PlayerData.P_EFE_DUR + "/" + PlayerData.LV_EFE_DUR + "]";
                break;
            case Utils.AbilityType.Damage:
                _text.text = "DAMAGE[" + PlayerData.P_EFE_DMG + "/" + PlayerData.LV_EFE_DMG + "]";
                break;
            case Utils.AbilityType.Size:
                _text.text = "RANGE[" + PlayerData.P_EFE_RNG + "/" + PlayerData.LV_EFE_RNG + "]";
                break;
            case Utils.AbilityType.Percent:
                _text.text = "PERCENT[" + PlayerData.P_EFE_PCNT + "/" + PlayerData.LV_EFE_PCNT + "]";
                break;
        }
    }

    private void CreateWindow()
    {
        var window = Instantiate(_lvUpWindow, _customWindowFrame.transform);
        window.SetEfeWindow(_abilityType, this);
        StartCoroutine(DisplayWindow(window.gameObject));
    }

    private IEnumerator DisplayWindow(GameObject window)
    {
        window.transform.localScale = new Vector3(0, 0);
        for (int i = 0; i < 20; i++)
        {
            window.transform.localScale += new Vector3(0.025f, 0.025f, 0);
            yield return new WaitForSecondsRealtime(0.01f);
        }
    }
}
