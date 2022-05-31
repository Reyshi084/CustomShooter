using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    [SerializeField]
    private int LV = 1;
    [SerializeField]
    private int EXP;
    [SerializeField]
    private int STAGE = 1;
    [SerializeField]
    private int EFENUM;

    [SerializeField]
    private int EGY;
    [SerializeField]
    private int E_HP;
    [SerializeField]
    private int E_ATK;
    [SerializeField]
    private int E_DFE;
    [SerializeField]
    private int E_BSPD;
    [SerializeField]
    private int E_BNUM;
    [SerializeField]
    private int E_MSPD;

    [SerializeField]
    private int EFEP;
    [SerializeField]
    private int EFELV;
    [SerializeField]
    private int LV_EFE_DUR;
    [SerializeField]
    private int LV_EFE_DMG;
    [SerializeField]
    private int LV_EFE_PCNT;
    [SerializeField]
    private int LV_EFE_RNG;
    [SerializeField]
    private int P_EFE_DUR;
    [SerializeField]
    private int P_EFE_DMG;
    [SerializeField]
    private int P_EFE_PCNT;
    [SerializeField]
    private int P_EFE_RNG;

    [SerializeField]
    private int LV_ABI_TRI;
    [SerializeField]
    private int LV_ABI_SQU;
    [SerializeField]
    private int LV_ABI_PNT;
    [SerializeField]
    private int LV_ABI_HEX;
    [SerializeField]
    private int LV_ABI_STR;
    [SerializeField]
    private int P_ABI_TRI;
    [SerializeField]
    private int P_ABI_SQU;
    [SerializeField]
    private int P_ABI_PNT;
    [SerializeField]
    private int P_ABI_HEX;
    [SerializeField]
    private int P_ABI_STR;

    [SerializeField]
    private int[] FRAGNUM = new int[Utils.ItemTypeNum * Utils.ItemLvMax];

    [SerializeField]
    private int CLEARCNT;


    public static void ConvertSaveDataToPlayerData(SaveData nowData)
    {
        PlayerData.LV = nowData.LV;
        PlayerData.EXP = nowData.EXP;
        PlayerData.STAGE = nowData.STAGE;
        PlayerData.EFENUM = nowData.EFENUM;

        PlayerData.EGY = nowData.EGY;
        PlayerData.E_HP = nowData.E_HP;
        PlayerData.E_ATK = nowData.E_ATK;
        PlayerData.E_DFE = nowData.E_DFE;
        PlayerData.E_BSPD = nowData.E_BSPD;
        PlayerData.E_BNUM = nowData.E_BNUM;
        PlayerData.E_MSPD = nowData.E_MSPD;

        PlayerData.EFEP = nowData.EFEP;
        PlayerData.EFELV = nowData.EFELV;
        PlayerData.LV_EFE_DUR = nowData.LV_EFE_DUR;
        PlayerData.LV_EFE_DMG = nowData.LV_EFE_DMG;
        PlayerData.LV_EFE_PCNT = nowData.LV_EFE_PCNT;
        PlayerData.LV_EFE_RNG = nowData.LV_EFE_RNG;
        PlayerData.P_EFE_DUR = nowData.P_EFE_DUR;
        PlayerData.P_EFE_DMG = nowData.P_EFE_DMG;
        PlayerData.P_EFE_PCNT = nowData.P_EFE_PCNT;
        PlayerData.P_EFE_RNG = nowData.P_EFE_RNG;

        PlayerData.LV_ABI_TRI = nowData.LV_ABI_TRI;
        PlayerData.LV_ABI_SQU = nowData.LV_ABI_SQU;
        PlayerData.LV_ABI_PNT = nowData.LV_ABI_PNT;
        PlayerData.LV_ABI_HEX = nowData.LV_ABI_HEX;
        PlayerData.LV_ABI_STR = nowData.LV_ABI_STR;

        PlayerData.P_ABI_TRI = nowData.P_ABI_TRI;
        PlayerData.P_ABI_SQU = nowData.P_ABI_SQU;
        PlayerData.P_ABI_PNT = nowData.P_ABI_PNT;
        PlayerData.P_ABI_HEX = nowData.P_ABI_HEX;
        PlayerData.P_ABI_STR = nowData.P_ABI_STR;

        LoadFragData(nowData.FRAGNUM);

        PlayerData.CLEARCNT = nowData.CLEARCNT;

    }   

    public static SaveData ConvertPlayerDataToSaveData()
    {
        SaveData nowData = new SaveData();

        nowData.LV = PlayerData.LV;
        nowData.EXP = PlayerData.EXP;
        nowData.STAGE = PlayerData.STAGE;
        nowData.EFENUM = PlayerData.EFENUM;

        nowData.EGY = PlayerData.EGY;
        nowData.E_HP = PlayerData.E_HP;
        nowData.E_ATK = PlayerData.E_ATK;
        nowData.E_DFE = PlayerData.E_DFE;
        nowData.E_BSPD = PlayerData.E_BSPD;
        nowData.E_BNUM = PlayerData.E_BNUM;
        nowData.E_MSPD = PlayerData.E_MSPD;

        nowData.EFEP = PlayerData.EFEP;
        nowData.EFELV = PlayerData.EFELV;
        nowData.LV_EFE_DUR = PlayerData.LV_EFE_DUR;
        nowData.LV_EFE_DMG = PlayerData.LV_EFE_DMG;
        nowData.LV_EFE_PCNT = PlayerData.LV_EFE_PCNT;
        nowData.LV_EFE_RNG = PlayerData.LV_EFE_RNG;
        nowData.P_EFE_DUR = PlayerData.P_EFE_DUR;
        nowData.P_EFE_DMG = PlayerData.P_EFE_DMG;
        nowData.P_EFE_PCNT = PlayerData.P_EFE_PCNT;
        nowData.P_EFE_RNG = PlayerData.P_EFE_RNG;

        nowData.LV_ABI_TRI = PlayerData.LV_ABI_TRI;
        nowData.LV_ABI_SQU = PlayerData.LV_ABI_SQU;
        nowData.LV_ABI_PNT = PlayerData.LV_ABI_PNT;
        nowData.LV_ABI_HEX = PlayerData.LV_ABI_HEX;
        nowData.LV_ABI_STR = PlayerData.LV_ABI_STR;
            
        nowData.P_ABI_TRI = PlayerData.P_ABI_TRI;
        nowData.P_ABI_SQU = PlayerData.P_ABI_SQU;
        nowData.P_ABI_PNT = PlayerData.P_ABI_PNT;
        nowData.P_ABI_HEX = PlayerData.P_ABI_HEX;
        nowData.P_ABI_STR = PlayerData.P_ABI_STR;

        SaveFragData(nowData.FRAGNUM);

        nowData.CLEARCNT = PlayerData.CLEARCNT;

        return nowData;

    }

    private static void LoadFragData(int[] frags)
    {
        if(frags == null)
        {
            PlayerData.FRAGNUM = new int[Utils.ItemTypeNum, Utils.ItemLvMax];
            return;
        }

        for(int y = 0; y < Utils.ItemTypeNum; y++)
        {
            for(int x = 0; x < Utils.ItemLvMax; x++)
            {
                PlayerData.FRAGNUM[y, x] = frags[x + y * Utils.ItemLvMax];
            }
        }
    }

    private static void SaveFragData(int[] frags)
    {
        for (int y = 0; y < Utils.ItemTypeNum; y++)
        {
            for (int x = 0; x < Utils.ItemLvMax; x++)
            {
                frags[x + y * Utils.ItemLvMax] = PlayerData.FRAGNUM[y, x];
            }
        }
    }
}
