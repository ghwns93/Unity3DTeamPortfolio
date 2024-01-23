using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatText : MonoBehaviour
{
    public Text text_lv;    // 레벨
    public Text text_hp;    // 체력
    public Text text_sp;    // 스태미나
    //public Text text_atk;   // 공격력
    //public Text text_def;   // 방어력
    //public Text text_cri;   // 치명타율
    //public Text text_agi;   // 회피율
    //public Text text_as;    // 공격속도
    //public Text text_ms;    // 이동속도
    public Text text_gold;  // 돈

    void Update()
    {
        //text_lv.text = PlayerState.Instance.Level.ToString("00");
        //text_hp.text = PlayerState.Instance.Hp.ToString("00000");
        //text_sp.text = PlayerState.Instance.Stamina.ToString("00000");
        //text_gold = PlayerState.Instance.Money.ToString("00000") + " G";
    }
}
