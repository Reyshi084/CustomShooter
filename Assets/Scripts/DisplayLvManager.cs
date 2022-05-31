using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayLvManager : MonoBehaviour
{
    private Text lvText;

    private void Start()
    {
        lvText = GetComponent<Text>();
    }

    public void Update()
    {
        lvText.text = "LV  .    " + PlayerData.LV + "\n"
                    + "EGY .    " + PlayerData.UsedEnergy + "/" + PlayerData.EGY + "\n"
                    + "EXP .    " + PlayerData.EXP + "\n"
                    + "NEXT.    " + PlayerData.ToNextLevelExp();
    }
}
