using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public static class PlayerData
{
    public static int MAX_hp { private set; get; } = 10; // 最大HP
    public static int MAX_attack { private set; get; } = 1; // 初期攻撃力
    public static int MAX_defense { private set; get; } = 0; // 初期防御力
    public static float BulletSpeed { private set; get; } = 10f; // 弾丸の速度
    public static int BulletNum { private set; get; } = 3; // 弾数
    public static float MoveSpeed { private set; get; } = 10f; // 移動速度
    public static Utils.Effect AddingEffect { private set; get; } = Utils.Effect.None; // 攻撃時の追加効果
    public static float Duration { private set; get; } = 1f; // 追加効果の持続時間(poison, fire, stream, lightning, cold, hypnoosis, chain)
    public static int Damage { private set; get; } = 1; // 追加効果のダメージ量（poison, fire, lightning）
    public static float Percent { private set; get; } = 0.3f; // 追加効果の割合（stream, hypnosis, slash）
    public static float Size { private set; get; } = 1f; // 追加効果の大きさ（poison, stream）
    public static Utils.PlayerBulletType PBT { private set; get; } = Utils.PlayerBulletType.Normal; // 弾道の種類



    // 以下、セーブデータ

    public static int LV {  set; get; } = 1;
    public static int EXP {  set; get; } = 0;
    public static int STAGE {  set; get; } = 1;
    public static int EFENUM {  set; get; } = 0;

    public static int EGY { set; get; } = 0;
    public static int E_HP {  set; get; } = 0;
    public static int E_ATK {  set; get; } = 0;
    public static int E_DFE {  set; get; } = 0;
    public static int E_BSPD {  set; get; } = 0;
    public static int E_BNUM {  set; get; } = 0;
    public static int E_MSPD {  set; get; } = 0;


    // エフェクトレベルによって振り分けられるポイントが変化する
    // ポイントは全ての性能共通のものである
    public static int EFELV { set; get; } = 0;
    public static int EFEP { set; get; } = 0;
    public static int LV_EFE_DUR { set; get; } = 0;
    public static int LV_EFE_DMG { set; get; } = 0;
    public static int LV_EFE_PCNT { set; get; } = 0;
    public static int LV_EFE_RNG { set; get; } = 0;
    public static int P_EFE_DUR {  set; get; } = 0;
    public static int P_EFE_DMG {  set; get; } = 0;
    public static int P_EFE_PCNT {  set; get; } = 0;
    public static int P_EFE_RNG {  set; get; } = 0;

    // それぞれのアビリティに対してレベルが存在し、そのレベルを上げることで振ることのできるポイントが変化する
    // ポイントは別々で管理される
    public static int LV_ABI_TRI {  set; get; } = 0;
    public static int LV_ABI_SQU {  set; get; } = 0;
    public static int LV_ABI_PNT {  set; get; } = 0;
    public static int LV_ABI_HEX {  set; get; } = 0;
    public static int LV_ABI_STR {  set; get; } = 0;

    public static int P_ABI_TRI {  set; get; } = 0;
    public static int P_ABI_SQU {  set; get; } = 0;
    public static int P_ABI_PNT {  set; get; } = 0;
    public static int P_ABI_HEX {  set; get; } = 0;
    public static int P_ABI_STR {  set; get; } = 0;


    public static int[,] FRAGNUM { set; get; } = new int[Utils.ItemTypeNum, Utils.ItemLvMax];

    public static int CLEARCNT { set; get; } = 0;

    // セーブデータおわり



    public static int UsedEnergy { private set; get; } = 0;
    public static int UsedEffectPoint { private set; get; } = 0;



    private static SaveData _nowData;
    private static string _filePath = Application.persistentDataPath + "/" + "savedata1.json";



    private const int EFERATE = 4;



    public static void LoadPlayerData()
    {
        Debug.Log("Loading...");

        if (File.Exists(_filePath))
        {
            Debug.Log("Exist!");
            StreamReader streamReader = new StreamReader(_filePath);
            string data = streamReader.ReadToEnd();
            streamReader.Close();
            _nowData = JsonUtility.FromJson<SaveData>(data);
            
        }
        else
        {
            // 一番最初にゲームをプレイした時
            CreateNewSaveData();
        }

        SaveData.ConvertSaveDataToPlayerData(_nowData);
        ConvertEnergyToParam();


        UsedEnergy = AddedEGY();
        UsedEffectPoint = AddedEFEP();
    }

    public static void SavePlayerData()
    {
        Debug.Log("Saving...");

        _nowData = SaveData.ConvertPlayerDataToSaveData();

        string json = JsonUtility.ToJson(_nowData);
        StreamWriter streamWriter = new StreamWriter(_filePath);
        streamWriter.Write(json);
        streamWriter.Flush();
        streamWriter.Close();
    }

    public static void Reset()
    {
        CreateNewSaveData();
        SaveData.ConvertSaveDataToPlayerData(_nowData);
    }

    private static void CreateNewSaveData()
    {
        // 初期化されたデータの生成
        Debug.Log("Create...");
        _nowData = new SaveData();

        string json = JsonUtility.ToJson(_nowData);
        StreamWriter streamWriter = new StreamWriter(_filePath);
        streamWriter.Write(json);
        streamWriter.Flush();
        streamWriter.Close();
    }

    public static void ConvertEnergyToParam()
    {
        MAX_hp = 10 + E_HP + (int)(E_HP * LV / 10.0f);
        MAX_attack = 1 + E_ATK + (int)(E_ATK * LV / 10.0f);
        MAX_defense = 0 + E_DFE + (int)(E_DFE * LV / 10.0f);
        BulletSpeed = 10f + E_BSPD + E_BSPD / 5;
        BulletNum = 3 + E_BNUM;
        MoveSpeed = 10f + E_MSPD + E_MSPD / 5;
        AddingEffect = (Utils.Effect)EFENUM;
        Duration = 1f + P_EFE_DUR; // 1, 2, 3, ..., 16
        Damage = (1 + P_EFE_DMG + (int)(P_EFE_DMG * LV / 10.0f)) * 2; // attack * 2 とおなじ
        Percent = 0.1f + P_EFE_PCNT * 0.02f; // 0.10, 0.12, 0.14, ..., 0.4
        Size = 1f + (P_EFE_RNG / 2.0f); // 1, 1.5, 2, 2.5, ..., 8.5
    }

    public static int AddedEGY()
    {
        int cnt = 0;
        cnt += E_HP;
        cnt += E_ATK;
        cnt += E_DFE;
        cnt += E_BSPD;
        cnt += E_BNUM;
        cnt += E_MSPD;

        return cnt;
    }

    public static int AddedEFEP()
    {
        int cnt = 0;
        cnt += P_EFE_DMG;
        cnt += P_EFE_DUR;
        cnt += P_EFE_PCNT;
        cnt += P_EFE_RNG;

        return cnt;
    }


    public static bool AddHP()
    {
        if(UsedEnergy < EGY)
        {
            E_HP++;
            UsedEnergy++;
            return true;
        }
        return false;
    }

    public static bool AddATK()
    {
        if (UsedEnergy < EGY)
        {
            E_ATK++;
            UsedEnergy++;
            return true;
        }
        return false;
    }

    public static bool AddDFE()
    {
        if (UsedEnergy < EGY)
        {
            E_DFE++;
            UsedEnergy++;
            return true;
        }
        return false;
    }

    public static bool AddBSPD()
    {
        if (UsedEnergy < EGY)
        {
            E_BSPD++;
            UsedEnergy++;
            return true;
        }
        return false;
    }

    public static bool AddBNUM()
    {
        if (UsedEnergy < EGY)
        {
            E_BNUM++;
            UsedEnergy++;
            return true;
        }
        return false;
    }

    public static bool AddMSPD()
    {
        if (UsedEnergy < EGY)
        {
            E_MSPD++;
            UsedEnergy++;
            return true;
        }
        return false;
    }

  
    public static bool AddEFEDUR()
    {
        if (P_EFE_DUR < LV_EFE_DUR)
        {
            P_EFE_DUR++;
            return true;
        }
        return false;
    }

    public static bool AddEFEDMG()
    {
        if (P_EFE_DMG < LV_EFE_DMG)
        {
            P_EFE_DMG++;
            return true;
        }
        return false;
    }

    public static bool AddEFEPCNT()
    {
        if (P_EFE_PCNT < LV_EFE_PCNT)
        {
            P_EFE_PCNT++;
            return true;
        }
        return false;
    }

    public static bool AddEFERNG()
    {
        if (P_EFE_RNG < LV_EFE_RNG)
        {
            P_EFE_RNG++;
            return true;
        }
        return false;
    }


    public static bool AddABITRI()
    {
        if (P_ABI_TRI < LV_ABI_TRI)
        {
            P_ABI_TRI++;
            return true;
        }
        return false;
    }

    public static bool AddABISQU()
    {
        if (P_ABI_SQU < LV_ABI_SQU)
        {
            P_ABI_SQU++;
            return true;
        }
        return false;
    }

    public static bool AddABIPNT()
    {
        if (P_ABI_PNT < LV_ABI_PNT)
        {
            P_ABI_PNT++;
            return true;
        }
        return false;
    }

    public static bool AddABIHEX()
    {
        if (P_ABI_HEX < LV_ABI_HEX)
        {
            P_ABI_HEX++;
            return true;
        }
        return false;
    }

    public static bool AddABISTR()
    {
        if (P_ABI_STR < LV_ABI_STR)
        {
            P_ABI_STR++;
            return true;
        }
        return false;
    }
    

    public static bool SubHP()
    {
        if (E_HP != 0)
        {
            E_HP--;
            UsedEnergy--;
            return true;
        }
        return false;
    }

    public static bool SubATK()
    {
        if(E_ATK != 0)
        {
            E_ATK--;
            UsedEnergy--;
            return true;
        }
        return false;
    }

    public static bool SubDFE()
    {
        if (E_DFE != 0)
        {
            E_DFE--;
            UsedEnergy--;
            return true;
        }
        return false;
    }

    public static bool SubBSPD()
    {
        if (E_BSPD != 0)
        {
            E_BSPD--;
            UsedEnergy--;
            return true;
        }
        return false;
    }

    public static bool SubBNUM()
    {
        if (E_BNUM != 0)
        {
            E_BNUM--;
            UsedEnergy--;
            return true;
        }
        return false;
    }

    public static bool SubMSPD()
    {
        if (E_MSPD != 0)
        {
            E_MSPD--;
            UsedEnergy--;
            return true;
        }
        return false;
    }

    public static bool SubEFE()
    {
        if (EFELV != 0)
        {
            EFELV--;
            UsedEnergy--;
            return true;
        }
        return false;
    }

    public static bool SubEFEDUR()
    {
        if (P_EFE_DUR > 0)
        {
            P_EFE_DUR--;
            return true;
        }
        return false;
    }

    public static bool SubEFEDMG()
    {
        if (P_EFE_DMG > 0)
        {
            P_EFE_DMG--;
            return true;
        }
        return false;
    }

    public static bool SubEFEPCNT()
    {
        if (P_EFE_PCNT > 0)
        {
            P_EFE_PCNT--;
            return true;
        }
        return false;
    }

    public static bool SubEFERNG()
    {
        if (P_EFE_RNG > 0)
        {
            P_EFE_RNG--;
            return true;
        }
        return false;
    }


    public static bool SubABITRI()
    {
        if (P_ABI_TRI > 0)
        {
            P_ABI_TRI--;
            return true;
        }
        return false;
    }

    public static bool SubABISQU()
    {
        if (P_ABI_SQU > 0)
        {
            P_ABI_SQU--;
            return true;
        }
        return false;
    }

    public static bool SubABIPNT()
    {
        if (P_ABI_PNT > 0)
        {
            P_ABI_PNT--;
            return true;
        }
        return false;
    }

    public static bool SubABIHEX()
    {
        if (P_ABI_HEX > 0)
        {
            P_ABI_HEX--;
            return true;
        }
        return false;
    }

    public static bool SubABISTR()
    {
        if (P_ABI_STR > 0)
        {
            P_ABI_STR--;
            return true;
        }
        return false;
    }


    public static void SetEffect(Utils.Effect effect)
    {
        AddingEffect = effect;
        EFENUM = (int)effect;
    }


    public static void GainEXP(int exp)
    {
        if(STAGE == SelectSceneManager.SelectStageNum)
        {
            STAGE++;
        }
        EXP += exp;
        if(EXP >= 9999999)
        {
            EXP = 9999999;
        }
        CheckLevelUp();
        SavePlayerData();
    }

    public static void GainItem(int itemNum, int itemLv)
    {
        if (FRAGNUM[itemNum, itemLv-1] < 1000)
        {
            FRAGNUM[itemNum, itemLv-1]++;
        }
    }

    private static void CheckLevelUp()
    {
        if(LV >= 99)
        {
            return;
        }

        if(((STAGE - 1) / 5) * 5 + 5 <= LV)
        {
            return;
        }

        if((LV + 1) * (LV + 1) * (LV + 1) * ((LV + 1) / 5 + 1) <= EXP)
        {
            LV++;
            EGY++;
            CheckLevelUp();
        }
    }

    public static string ToNextLevelExp()
    {
        var next = ((LV + 1) * (LV + 1) * (LV + 1) * ((LV + 1) / 5 + 1) - EXP);
        if(next <= 0)
        {
            return "未解禁";
        }
        else
        {
            return next.ToString();
        }
    }
}
