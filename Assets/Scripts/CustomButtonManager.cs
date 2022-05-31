using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomButtonManager : MonoBehaviour
{
    private enum Status
    {
        HP,
        ATK,
        DFE,
        BSPD,
        BNUM,
        MSPD,
    }

    [SerializeField] Text _statusText;
    [SerializeField] Status _status;

    private void Start()
    {
        SetStatusText();
    }

    private void SetStatusText()
    {
        PlayerData.ConvertEnergyToParam();

        switch (_status)
        {
            case Status.HP:
                _statusText.text = "HP" + "[" + PlayerData.E_HP + "]" + "(" + PlayerData.MAX_hp.ToString() + ")";
                break;
            case Status.ATK:
                _statusText.text = "ATK" + "[" + PlayerData.E_ATK + "]" + "(" + PlayerData.MAX_attack.ToString() + ")";
                break;
            case Status.DFE:
                _statusText.text = "DFE" + "[" + PlayerData.E_DFE + "]" + "(" + PlayerData.MAX_defense.ToString() + ")";
                break;
            case Status.BSPD:
                _statusText.text = "BSPD" + "[" + PlayerData.E_BSPD + "]" + "(" + PlayerData.BulletSpeed.ToString("f1") + ")";
                break;
            case Status.BNUM: 
                _statusText.text = "BNUM" + "[" + PlayerData.E_BNUM + "]" + "(" + PlayerData.BulletNum.ToString() + ")";
                break;
            case Status.MSPD:
                _statusText.text = "MSPD" + "[" + PlayerData.E_MSPD + "]" + "(" + PlayerData.MoveSpeed.ToString("f1") + ")";
                break;
        }
    }

    private void SetStatusText(string name, int energy)
    {
        PlayerData.ConvertEnergyToParam();

        string sta = "???";
        switch (_status)
        {
            case Status.HP:
                sta = PlayerData.MAX_hp.ToString();
                break;
            case Status.ATK:
                sta = PlayerData.MAX_attack.ToString();
                break;
            case Status.DFE:
                sta = PlayerData.MAX_defense.ToString();
                break;
            case Status.BSPD:
                sta = PlayerData.BulletSpeed.ToString("f1");
                break;
            case Status.BNUM:
                sta = PlayerData.BulletNum.ToString();
                break;
            case Status.MSPD:
                sta = PlayerData.MoveSpeed.ToString("f1");
                break;
            default:
                break;
        }

        _statusText.text = name + "[" + energy + "]" + "(" + sta + ")";
    }

    public void OnHPPulsButtonDown()
    {
        PlayerData.AddHP();
        SetStatusText("HP", PlayerData.E_HP);
    }

    public void OnHPMinusButtonDown()
    {
        PlayerData.SubHP();
        SetStatusText("HP", PlayerData.E_HP);
    }


    public void OnATKPulsButtonDown()
    {
        PlayerData.AddATK();
        SetStatusText("ATK", PlayerData.E_ATK);
    }

    public void OnATKMinusButtonDown()
    {
        PlayerData.SubATK();
        SetStatusText("ATK", PlayerData.E_ATK);
    }


    public void OnDFEPulsButtonDown()
    {
        PlayerData.AddDFE();
        SetStatusText("DFE", PlayerData.E_DFE);
    }

    public void OnDFEMinusButtonDown()
    {
        PlayerData.SubDFE();
        SetStatusText("DFE", PlayerData.E_DFE);
    }


    public void OnBSPDPulsButtonDown()
    {
        PlayerData.AddBSPD();
        SetStatusText("BSPD", PlayerData.E_BSPD);
    }

    public void OnBSPDMinusButtonDown()
    {
        PlayerData.SubBSPD();
        SetStatusText("BSPD", PlayerData.E_BSPD);
    }


    public void OnBNUMPulsButtonDown()
    {
        PlayerData.AddBNUM();
        SetStatusText("BNUM", PlayerData.E_BNUM);
    }

    public void OnBNUMMinusButtonDown()
    {
        PlayerData.SubBNUM();
        SetStatusText("BNUM", PlayerData.E_BNUM);
    }


    public void OnMSPDPulsButtonDown()
    {
        PlayerData.AddMSPD();
        SetStatusText("MSPD", PlayerData.E_MSPD);
    }

    public void OnMSPDMinusButtonDown()
    {
        PlayerData.SubMSPD();
        SetStatusText("MSPD", PlayerData.E_MSPD);
    }
}
