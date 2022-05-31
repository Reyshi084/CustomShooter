using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// アビリティポイントの振り分け、レベルの情報の処理を行う
public class AbiButtonManager : MonoBehaviour
{
    [SerializeField] Text _text;
    [SerializeField] Utils.EnemyType _enemyType;
    [SerializeField] CustomLvWindowManager _lvUpWindow;
    [SerializeField] GameObject _customWindowFrame;


    private void Start()
    {
        SetText();
    }

    public void OnAbiPlusButtonDown()
    {
        switch (_enemyType)
        {
            case Utils.EnemyType.Triangle:
                if (!PlayerData.AddABITRI())
                {
                    if (PlayerData.LV_ABI_TRI < 15)
                    {
                        var window = Instantiate(_lvUpWindow, _customWindowFrame.transform);
                        window.SetAbiWindow(_enemyType, this);
                        StartCoroutine(DisplayWindow(window.gameObject));
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
            case Utils.EnemyType.Square:
                if (!PlayerData.AddABISQU())
                {
                    if (PlayerData.LV_ABI_SQU < 15)
                    {
                        var window = Instantiate(_lvUpWindow, _customWindowFrame.transform);
                        window.SetAbiWindow(_enemyType, this);
                        StartCoroutine(DisplayWindow(window.gameObject));
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
            case Utils.EnemyType.Pentagon:
                if (!PlayerData.AddABIPNT())
                {
                    if (PlayerData.LV_ABI_PNT < 15)
                    {
                        var window = Instantiate(_lvUpWindow, _customWindowFrame.transform);
                        window.SetAbiWindow(_enemyType, this);
                        StartCoroutine(DisplayWindow(window.gameObject));
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
            case Utils.EnemyType.Hexagon:
                if (!PlayerData.AddABIHEX())
                {
                    if (PlayerData.LV_ABI_HEX < 15)
                    {
                        var window = Instantiate(_lvUpWindow, _customWindowFrame.transform);
                        window.SetAbiWindow(_enemyType, this);
                        StartCoroutine(DisplayWindow(window.gameObject));
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
            case Utils.EnemyType.Star:
                if (!PlayerData.AddABISTR())
                {
                    if (PlayerData.LV_ABI_STR < 15)
                    {
                        var window = Instantiate(_lvUpWindow, _customWindowFrame.transform);
                        window.SetAbiWindow(_enemyType, this);
                        StartCoroutine(DisplayWindow(window.gameObject));
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
        }


    }

    public void OnAbiMinusButtonDown()
    {
        switch (_enemyType)
        {
            case Utils.EnemyType.Triangle:
                PlayerData.SubABITRI();
                SetText();
                break;
            case Utils.EnemyType.Square:
                PlayerData.SubABISQU();
                SetText();
                break;
            case Utils.EnemyType.Pentagon:
                PlayerData.SubABIPNT();
                SetText();
                break;
            case Utils.EnemyType.Hexagon:
                PlayerData.SubABIHEX();
                SetText();
                break;
            case Utils.EnemyType.Star:
                PlayerData.SubABISTR();
                SetText();
                break;
        }
    }

    public void SetText()
    {
        switch (_enemyType)
        {
            case Utils.EnemyType.Triangle:
                _text.text = "COUNTER[" + PlayerData.P_ABI_TRI + "/" + PlayerData.LV_ABI_TRI + "]";
                break;
            case Utils.EnemyType.Square:
                _text.text = "SHIELD[" + PlayerData.P_ABI_SQU + "/" + PlayerData.LV_ABI_SQU + "]";
                break;
            case Utils.EnemyType.Pentagon:
                _text.text = "HEAL[" + PlayerData.P_ABI_PNT + "/" + PlayerData.LV_ABI_PNT + "]";
                break;
            case Utils.EnemyType.Hexagon:
                _text.text = "GUARDIAN[" + PlayerData.P_ABI_HEX + "/" + PlayerData.LV_ABI_HEX + "]";
                break;
            case Utils.EnemyType.Star:
                _text.text = "BONUS[" + PlayerData.P_ABI_STR + "/" + PlayerData.LV_ABI_STR + "]";
                break;
        }
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
