using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatText : MonoBehaviour
{
    public Text text_lv;    // ����
    public Text text_hp;    // ü��
    public Text text_sp;    // ���¹̳�
    //public Text text_atk;   // ���ݷ�
    //public Text text_def;   // ����
    //public Text text_cri;   // ġ��Ÿ��
    //public Text text_agi;   // ȸ����
    //public Text text_as;    // ���ݼӵ�
    //public Text text_ms;    // �̵��ӵ�
    public Text text_gold;  // ��

    void Update()
    {
        //text_lv.text = PlayerState.Instance.Level.ToString("00");
        //text_hp.text = PlayerState.Instance.Hp.ToString("00000");
        //text_sp.text = PlayerState.Instance.Stamina.ToString("00000");
        //text_gold = PlayerState.Instance.Money.ToString("00000") + " G";
    }
}
